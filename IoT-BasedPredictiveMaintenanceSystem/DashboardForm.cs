using System;
using System.Windows.Forms;
using MongoDB.Bson;
using MongoDB.Driver;

namespace IoT_BasedPredictiveMaintenanceSystem
{
    public partial class DashboardForm: Form
    {
        private const string connectionString = "mongodb://127.0.0.1:27017";
        private const string databaseName = "IoTPredictiveMaintenance";
        private const string sensorCollectionName = "SensorData";
        private const string anomalyCollectionName = "Anomalies";

        private IMongoCollection<BsonDocument> sensorCollection;
        private IMongoCollection<BsonDocument> anomalyCollection;

        public DashboardForm()
        {
            InitializeComponent();
            InitializeDatabase();
            LoadDashboardStats();
        }
        private void InitializeDatabase()
        {
            var client = new MongoClient(connectionString);
            var database = client.GetDatabase(databaseName);
            sensorCollection = database.GetCollection<BsonDocument>(sensorCollectionName);
            anomalyCollection = database.GetCollection<BsonDocument>(anomalyCollectionName);
        }

        private async void LoadDashboardStats()
        {
            try
            {
                var totalReadings = await sensorCollection.CountDocumentsAsync(new BsonDocument());
                labelTotalReadings.Text = $"Total Readings: {totalReadings}";

                var totalFailures = await anomalyCollection.CountDocumentsAsync(new BsonDocument());
                labelFailuresDetected.Text = $"Failures Detected: {totalFailures}";

                var latestAnomaly = await anomalyCollection.Find(new BsonDocument())
                    .Sort(Builders<BsonDocument>.Sort.Descending("Timestamp"))
                    .Limit(1)
                    .FirstOrDefaultAsync();

                labelLastAnomaly.Text = latestAnomaly != null
                    ? $"Last Anomaly: {latestAnomaly["Timestamp"].ToUniversalTime()}"
                    : "Last Anomaly: None";

            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading dashboard stats: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void buttonRefreshStats_Click(object sender, EventArgs e)
        {
            LoadDashboardStats();
        }
    }
}
