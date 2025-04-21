using System;
using System.Globalization;
using CsvHelper;
using CsvHelper.Configuration;
using RainfallMonitor.Models;

//FileName: Processing.cs
//Date Created: 21-04-2025
//Author: Tien Hong TEOH
//Date Updated:  21-04-2025
//Updated By: Tien Hong TEOH
//
//Desc: Processing and Logic

namespace RainfallMonitor.Processing
{
    public class FileProcessing
    {
        private readonly string devicePath = "../Devices.csv";
        private readonly string[] dataPaths = Directory.GetFiles("../", "Data*.csv", SearchOption.TopDirectoryOnly);

        //Reads Devices.csv and returns in array of Device
        public List<Device> LoadDeviceCsv()
        {
            using var reader = new StreamReader(devicePath);
            using var csv = new CsvReader(reader, new CsvConfiguration(CultureInfo.InvariantCulture));
            return csv.GetRecords<Device>().ToList();
        }

        //Reads Data*.csv and returns in array of Device
        public List<Data> LoadDataCsv()
        {   
            var data = new List<Data>();
            foreach (var dpath in dataPaths)
            {
                using var reader = new StreamReader(dpath);
                using var csv = new CsvReader(reader, new CsvConfiguration(CultureInfo.InvariantCulture){HasHeaderRecord = true});

                data.AddRange(csv.GetRecords<Data>());
                
            }
            return data;
        }

        public List<RainfallReadings> ProcessingRainfall()
        {
            var status = new List<RainfallReadings>();
            return status;
        }
    }
}