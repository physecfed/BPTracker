using System;
using System.Collections.Generic;
using Windows.UI.Xaml.Data;

namespace BPTracker
{
    public class MainViewModel : BindingBase
    {
        public List<BloodPressure> BloodPressureData;
        public decimal SystolicMean, SystolicVariance;
        public decimal DiastolicMean, DiastolicVariance;
        public decimal HeartRateMean, HeartRateVariance;
        public string SystolicString, DiastolicString, HeartRateString;

        public MainViewModel()
        {
            BloodPressureData = new List<BloodPressure>();
        }

        public void UpdateStatistics()
        {
            int systolicSum = 0, diastolicSum = 0, heartRateSum = 0;
            foreach (var bloodPressureElement in BloodPressureData) {
                systolicSum += bloodPressureElement.SystolicPressure;
                diastolicSum += bloodPressureElement.DiastolicPressure;
                heartRateSum += bloodPressureElement.HeartRate;
            }
            SystolicMean = ((decimal) systolicSum) / ((decimal) BloodPressureData.Count);
            DiastolicMean = ((decimal) diastolicSum) / ((decimal) BloodPressureData.Count);
            HeartRateMean = ((decimal) heartRateSum) / ((decimal) BloodPressureData.Count);
        }
    }
}
