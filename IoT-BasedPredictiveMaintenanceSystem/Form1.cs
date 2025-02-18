using System;
using System.Collections.Generic;
using System.IO;
using LiveCharts;
using LiveCharts.WinForms;
using LiveCharts.Wpf;
using MongoDB.Bson;
using MongoDB.Driver;
using System.Windows.Forms;
using System.Threading.Tasks;
using System.Drawing;
using Microsoft.ML;
using Microsoft.ML.Data;

namespace IoT_BasedPredictiveMaintenanceSystem
{
    public partial class Form1: Form
    {
        private Random random = new Random();
        private List<SensorData> sensorDataList = new List<SensorData>();
        private IMongoCollection<BsonDocument> sensorCollection;
        private const string connectionString = "mongodb://localhost:27017";
        private const string databaseName = "IoTPredictiveMaintenance";
        private const string collectionName = "SensorData";
        private const double TemperatureThreshold = 28.0;  
        private const double VibrationThreshold = 1.8;    
        private const double CurrentThreshold = 4.5;
        private MLContext mlContext;
        private ITransformer trainedModel;
        private PredictionEngine<SensorDataML, FailurePrediction> predictionEngine;
        private const string modelPath = "failure_prediction_model.zip";
        private int sensorCountSinceLastTraining = 0;
        public class FailurePrediction
        {
            [ColumnName("PredictedLabel")]
            public bool PredictedFailure { get; set; }
        }
        public Form1()
        {
            InitializeComponent();
            LoadOrTrainModel();
            dataGridViewSensors.ColumnCount = 4;
            dataGridViewSensors.Columns[0].Name = "Timestamp";
            dataGridViewSensors.Columns[1].Name = "Temperature (°C)";
            dataGridViewSensors.Columns[2].Name = "Vibration (g)";
            dataGridViewSensors.Columns[3].Name = "Current (A)";

            cartesianChartSensors.Series = new SeriesCollection
    {
        new LineSeries { Title = "Temperature (°C)", Values = new ChartValues<double>() },
        new LineSeries { Title = "Vibration (g)", Values = new ChartValues<double>() },
        new LineSeries { Title = "Current (A)", Values = new ChartValues<double>() }
    };

            cartesianChartSensors.AxisX.Add(new Axis
            {
                Title = "Time",
                Labels = new List<string>()
            });

            cartesianChartSensors.AxisY.Add(new Axis
            {
                Title = "Sensor Values"
            });

            try
            {
                var client = new MongoClient(connectionString);
                var database = client.GetDatabase(databaseName);
                sensorCollection = database.GetCollection<BsonDocument>(collectionName);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"MongoDB Connection Error: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            LoadHistoricalData();
        }
        private async void LoadHistoricalData()
        {
            try
            {
                var count = await sensorCollection.CountDocumentsAsync(new BsonDocument());
                if (count == 0)
                {
                    MessageBox.Show("No historical data found.", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                var documents = await sensorCollection.Find(new BsonDocument())
                                                     .Sort(Builders<BsonDocument>.Sort.Descending("Timestamp"))
                                                     .ToListAsync();

                foreach (var doc in documents)
                {
                    var timestamp = doc.Contains("Timestamp") ? doc["Timestamp"].ToUniversalTime() : DateTime.MinValue;
                    var temperature = doc.Contains("Temperature") ? doc["Temperature"].ToDouble() : 0;
                    var vibration = doc.Contains("Vibration") ? doc["Vibration"].ToDouble() : 0;
                    var current = doc.Contains("Current") ? doc["Current"].ToDouble() : 0;

                    dataGridViewSensors.Rows.Add(timestamp, temperature, vibration, current);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading historical data: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private async Task SaveToDatabase(SensorData data)
        {
            var document = new BsonDocument
    {
        { "Timestamp", data.Timestamp },
        { "Temperature", data.Temperature },
        { "Vibration", data.Vibration },
        { "Current", data.Current }
    };

            await sensorCollection.InsertOneAsync(document);
        }

        private void buttonConnect_Click(object sender, EventArgs e)
        {

        }

        private void buttonStartMonitoring_Click(object sender, EventArgs e)
        {
            timerData.Start();
            labelStatus.Text = "Monitoring Started...";
        }

        private void timerData_Tick(object sender, EventArgs e)
        {
            SensorData data = new SensorData
            {
                Timestamp = DateTime.Now,
                Temperature = Math.Round(20 + random.NextDouble() * 10, 2), 
                Vibration = Math.Round(0.5 + random.NextDouble() * 1.5, 2), 
                Current = Math.Round(2 + random.NextDouble() * 3, 2) 
            };

            sensorDataList.Add(data);
            UpdateUI(data);
        }
        private async void UpdateUI(SensorData data)
        {
            if (InvokeRequired)
            {
                Invoke(new Action<SensorData>(UpdateUI), data);
                return;
            }

            dataGridViewSensors.Rows.Add(data.Timestamp, data.Temperature, data.Vibration, data.Current);

            await SaveToDatabase(data);

            if (data.Temperature > TemperatureThreshold || data.Vibration > VibrationThreshold || data.Current > CurrentThreshold)
            {
                TriggerAlert(data);
                await LogAnomaly(data);
            }

            PredictFailure(data);

            sensorCountSinceLastTraining++;

            if (sensorCountSinceLastTraining >= 100)
            {
                TrainModel();
                sensorCountSinceLastTraining = 0;
            }

            var series = cartesianChartSensors.Series;
            series[0].Values.Add(data.Temperature);
            series[1].Values.Add(data.Vibration);
            series[2].Values.Add(data.Current);

            if (series[0].Values.Count > 50) series[0].Values.RemoveAt(0);
            if (series[1].Values.Count > 50) series[1].Values.RemoveAt(0);
            if (series[2].Values.Count > 50) series[2].Values.RemoveAt(0);

            var labels = cartesianChartSensors.AxisX[0].Labels;
            labels.Add(data.Timestamp.ToString("HH:mm:ss"));
            if (labels.Count > 50) labels.RemoveAt(0);
        }
        private void TriggerAlert(SensorData data)
        {
            string message = $"⚠️ ALERT! Potential failure detected:\n" +
                             $"Temperature: {data.Temperature}°C (Threshold: {TemperatureThreshold}°C)\n" +
                             $"Vibration: {data.Vibration}g (Threshold: {VibrationThreshold}g)\n" +
                             $"Current: {data.Current}A (Threshold: {CurrentThreshold}A)";

            MessageBox.Show(message, "Failure Alert", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            labelStatus.Text = "⚠️ Anomaly Detected!";
            labelStatus.ForeColor = Color.Red;
        }
        private async Task LogAnomaly(SensorData data)
        {
            var anomalyDocument = new BsonDocument
    {
        { "Timestamp", data.Timestamp },
        { "Temperature", data.Temperature },
        { "Vibration", data.Vibration },
        { "Current", data.Current },
        { "Alert", "Failure Condition Met" }
    };

            var anomalyCollection = sensorCollection.Database.GetCollection<BsonDocument>("Anomalies");
            await anomalyCollection.InsertOneAsync(anomalyDocument);
        }

        private void buttonViewReports_Click(object sender, EventArgs e)
        {
            AnomalyReportForm reportForm = new AnomalyReportForm();
            reportForm.Show();
        }
        private void TrainModel()
        {
            mlContext = new MLContext();

            var trainingData = FetchTrainingData();

            if (trainingData.Count < 10)  // Ensure we have enough data
            {
                MessageBox.Show("Not enough data for training. At least 10 records required.", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            var dataView = mlContext.Data.LoadFromEnumerable(trainingData);

            var pipeline = mlContext.Transforms.Concatenate("Features", "Temperature", "Vibration", "Current")
                .Append(mlContext.BinaryClassification.Trainers.FastTree(labelColumnName: "Failure"));

            trainedModel = pipeline.Fit(dataView);
            predictionEngine = mlContext.Model.CreatePredictionEngine<SensorDataML, FailurePrediction>(trainedModel);

            mlContext.Model.Save(trainedModel, dataView.Schema, modelPath);

            MessageBox.Show("ML Model Retrained and Saved Successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void LoadOrTrainModel()
        {
            mlContext = new MLContext();

            if (File.Exists(modelPath))
            {
                // Load the saved model
                trainedModel = mlContext.Model.Load(modelPath, out var modelSchema);
                predictionEngine = mlContext.Model.CreatePredictionEngine<SensorDataML, FailurePrediction>(trainedModel);

                MessageBox.Show("ML Model Loaded Successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                TrainModel();
            }
        }

        private void PredictFailure(SensorData data)
        {
            var input = new SensorDataML
            {
                Temperature = (float)data.Temperature,
                Vibration = (float)data.Vibration,
                Current = (float)data.Current
            };

            var prediction = predictionEngine.Predict(input);

            if (prediction.PredictedFailure)
            {
                MessageBox.Show("⚠️ ML Prediction: Potential failure detected!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                labelStatus.Text = "⚠️ ML Prediction: Failure Expected!";
                labelStatus.ForeColor = Color.OrangeRed;
            }
        }
        private List<SensorDataML> FetchTrainingData()
        {
            var trainingData = new List<SensorDataML>();

            try
            {
                var sensorDocuments = sensorCollection.Find(new BsonDocument()).Sort(Builders<BsonDocument>.Sort.Descending("Timestamp")).ToList();

                foreach (var doc in sensorDocuments)
                {
                    var temperature = doc.Contains("Temperature") ? doc["Temperature"].ToDouble() : 0;
                    var vibration = doc.Contains("Vibration") ? doc["Vibration"].ToDouble() : 0;
                    var current = doc.Contains("Current") ? doc["Current"].ToDouble() : 0;
                    var failure = (temperature > TemperatureThreshold || vibration > VibrationThreshold || current > CurrentThreshold);

                    trainingData.Add(new SensorDataML
                    {
                        Temperature = (float)temperature,
                        Vibration = (float)vibration,
                        Current = (float)current,
                        Failure = failure
                    });
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error fetching training data: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            return trainingData;
        }

        private void buttonOpenDashboard_Click(object sender, EventArgs e)
        {
            DashboardForm dashboard = new DashboardForm();
            dashboard.Show();
        }
    }
}
