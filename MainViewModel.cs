using System;
using System.IO;
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

        /// <summary>
        /// 
        /// </summary>
        public MainViewModel()
        {
            BloodPressureData = new List<BloodPressure>();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="filePath"></param>
        public void LoadFromCsv(string filePath = @"..\..\..\..\bpdata.csv")
        {
            string fullPath = Path.GetFullPath(filePath);
            bool fileDoesExist = File.Exists(fullPath);
            Stream s = File.OpenRead(fullPath);
            if (File.Exists(fullPath)) {
                foreach (string line in File.ReadAllLines(fullPath)) {
                    string[] substrings = line.Split(',');
                    DateTime dateTime = DateTime.Parse(substrings[0]);
                    int systolic = int.Parse(substrings[1]);
                    int diastolic = int.Parse(substrings[2]);
                    int heartRate = int.Parse(substrings[3]);
                    BloodPressure bloodPressure = new BloodPressure { 
                        SystolicPressure = systolic, 
                        DiastolicPressure = diastolic, 
                        HeartRate = heartRate, 
                        MeasurementTime = dateTime 
                    };
                    BloodPressureData.Add(bloodPressure);
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
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
