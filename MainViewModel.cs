using System;
using System.Collections.Generic;
using System.IO;
using System.Data.SqlClient;
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
        /// <param name="connectionString"></param>
        public void LoadFromSql(string connectionString)
        {
            using (SqlConnection sqlConnection = new SqlConnection(connectionString)) {
                string queryString = "SELECT * FROM BpTable";
                SqlCommand query = new SqlCommand(queryString, sqlConnection);
                sqlConnection.Open();
                using (SqlDataReader reader = query.ExecuteReader()) {
                    while (reader.Read()) {
                        BloodPressure newEntry = new BloodPressure();
                        newEntry.MeasurementTime = DateTime.Parse(reader["Time"].ToString());
                        newEntry.SystolicPressure = int.Parse(reader["Systolic"].ToString());
                        newEntry.DiastolicPressure = int.Parse(reader["Diastolic"].ToString());
                        newEntry.HeartRate = int.Parse(reader["HR"].ToString());
                        BloodPressureData.Add(newEntry);
                    }
                }
            }
            this.UpdateStatistics();
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
