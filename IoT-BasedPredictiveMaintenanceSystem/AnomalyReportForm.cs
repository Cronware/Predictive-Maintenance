using System;
using System.Text;
using System.IO;
using System.Windows.Forms;
using MongoDB.Bson;
using MongoDB.Driver;

namespace IoT_BasedPredictiveMaintenanceSystem
{
    public partial class AnomalyReportForm : Form
    {
        private const string connectionString = "mongodb://127.0.0.1:27017";
        private const string databaseName = "IoTPredictiveMaintenance";
        private const string collectionName = "Anomalies";
        private IMongoCollection<BsonDocument> anomalyCollection;

        public AnomalyReportForm()
        {
            InitializeComponent();
            InitializeDatabase();
            LoadAnomalies();
        }
        private void InitializeDatabase()
        {
            var client = new MongoClient(connectionString);
            var database = client.GetDatabase(databaseName);
            anomalyCollection = database.GetCollection<BsonDocument>(collectionName);
        }

        private async void LoadAnomalies()
        {
            try
            {
                var anomalies = await anomalyCollection.Find(new BsonDocument()).Sort(Builders<BsonDocument>.Sort.Descending("Timestamp")).ToListAsync();

                dataGridViewAnomalies.ColumnCount = 4;
                dataGridViewAnomalies.Columns[0].Name = "Timestamp";
                dataGridViewAnomalies.Columns[1].Name = "Temperature (°C)";
                dataGridViewAnomalies.Columns[2].Name = "Vibration (g)";
                dataGridViewAnomalies.Columns[3].Name = "Current (A)";

                foreach (var doc in anomalies)
                {
                    var timestamp = doc.Contains("Timestamp") ? doc["Timestamp"].ToUniversalTime() : DateTime.MinValue;
                    var temperature = doc.Contains("Temperature") ? doc["Temperature"].ToDouble() : 0;
                    var vibration = doc.Contains("Vibration") ? doc["Vibration"].ToDouble() : 0;
                    var current = doc.Contains("Current") ? doc["Current"].ToDouble() : 0;

                    dataGridViewAnomalies.Rows.Add(timestamp, temperature, vibration, current);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading anomalies: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void buttonExportCSV_Click(object sender, EventArgs e)
        {
            if (dataGridViewAnomalies.Rows.Count == 0)
            {
                MessageBox.Show("No anomalies to export.", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            using (SaveFileDialog saveFileDialog = new SaveFileDialog())
            {
                saveFileDialog.Filter = "CSV files (*.csv)|*.csv";
                saveFileDialog.Title = "Save Anomaly Report";
                saveFileDialog.FileName = "AnomalyReport.csv";

                if (saveFileDialog.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                        StringBuilder csvContent = new StringBuilder();

                        csvContent.AppendLine("Timestamp,Temperature,Vibration,Current");

                        foreach (DataGridViewRow row in dataGridViewAnomalies.Rows)
                        {
                            if (!row.IsNewRow)
                            {
                                csvContent.AppendLine(
                                    $"{row.Cells[0].Value},{row.Cells[1].Value},{row.Cells[2].Value},{row.Cells[3].Value}");
                            }
                        }

                        File.WriteAllText(saveFileDialog.FileName, csvContent.ToString());

                        MessageBox.Show("Anomaly report exported successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Error exporting CSV: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }
    }
}
