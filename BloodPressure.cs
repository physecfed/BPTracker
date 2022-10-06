using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BPTracker
{
    public class BloodPressure
    {
        public DateTime MeasurementTime { get; set; }
        public int SystolicPressure { get; set; }
        public int DiastolicPressure { get; set; }
        public int HeartRate { get; set; }
    }
}
