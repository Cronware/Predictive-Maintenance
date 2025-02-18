using Microsoft.ML.Data;

namespace IoT_BasedPredictiveMaintenanceSystem
{
    public class SensorDataML
    {
        [LoadColumn(0)] public float Temperature { get; set; }
        [LoadColumn(1)] public float Vibration { get; set; }
        [LoadColumn(2)] public float Current { get; set; }
        [LoadColumn(3)] public bool Failure { get; set; }  // 1 = Failure, 0 = Normal
    }
}
