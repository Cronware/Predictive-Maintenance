using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IoT_BasedPredictiveMaintenanceSystem
{
    class SensorData
    {
        public DateTime Timestamp { get; set; }
        public double Temperature { get; set; }
        public double Vibration { get; set; }
        public double Current { get; set; }
    }
}
