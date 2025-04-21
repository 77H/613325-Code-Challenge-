//FileName: Models.cs
//Date Created: 21-04-2025
//Author: Tien Hong TEOH
//Date Updated:  21-04-2025
//Updated By: Tien Hong TEOH
//
//Desc: Data Structure of Program


namespace RainfallMonitor.Models
{
    public class Device
    {
        public string DeviceID{get;set;}
        public string DeviceName{get;set;}
        public string Location{get;set;}
    }

        public class Data
    {
        public string DeviceID{get;set;}
        public DateTime Time{get;set;}
        public Double Rainfall{get;set;}
    }

    public class RainfallReadings
    {
        public string DeviceID{get;set;}
        public string DeviceName{get;set;}
        public string Location{get;set;}
        public double AverageRainfall{get;set;}
        public string ColorStatus{get;set;}
        public string trend{get;set;}
    }
}