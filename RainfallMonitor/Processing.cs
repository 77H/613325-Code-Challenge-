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
        private readonly string devicePath =  "../../../Devices.csv";
        private readonly string[] dataPaths = Directory.GetFiles("../../../", "Data*.csv", SearchOption.TopDirectoryOnly);

        //Reads Devices.csv and returns in array of Device
        public List<Device> LoadDeviceCsv()
        {
            using var reader = new StreamReader(devicePath);
            using var csv = new CsvReader(reader, new CsvConfiguration(CultureInfo.InvariantCulture)
            {
                PrepareHeaderForMatch = args =>args.Header.Replace(" ", "")
            });
            return csv.GetRecords<Device>().ToList();
        }

        //Reads Data*.csv and returns in array of Device
        public List<Data> LoadDataCsv()
        {   
            var data = new List<Data>();
            foreach (var dpath in dataPaths)
            {
                using var reader = new StreamReader(dpath);
                using var csv = new CsvReader(reader, new CsvConfiguration(CultureInfo.InvariantCulture)
                {
                    HasHeaderRecord = true,
                    PrepareHeaderForMatch = args =>args.Header.Replace(" ", "")
                });

                data.AddRange(csv.GetRecords<Data>());
                
            }
            return data;
        }

        //Generates and processes rainfall readings
        public List<RainfallReadings> ProcessingRainfall()
        {
            var status = new List<RainfallReadings>();
            var devices = LoadDeviceCsv();
            var data = LoadDataCsv();
            var latestTime = data.Max(da=>da.Time);//assuming latest time is within Data*.csv
            var cutoffTime = latestTime.AddHours(-4);//4 hours ago of latest time
            
            //based on each devices, matches the DeviceID based in Data*.csv from 4 hours ago of latest time
            foreach(var d in devices)
            {
                var deviceData = data.Where(da=> da.DeviceID == d.DeviceID && da.Time >=cutoffTime).OrderBy(da => da.Time).ToList(); 

                if(!deviceData.Any()) continue;//if no data present, proceed anyway

                double avg = deviceData.Average(dd => dd.Rainfall);//average rainfall of current device

                string color = "";

                if(avg >= 15 || deviceData.Any(dd => dd.Rainfall > 30))
                {
                    color = "Red";
                }
                else if(avg >= 10 && avg < 15)
                {
                    color = "Amber";
                }
                else if(avg < 10)
                {
                    color = "Green";
                }

                string trend = "Not enough info";

                if(deviceData.Count>=2)
                {
                    double firstHalfRainfallSum = 0; 
                    double secondHalfRainfallSum = 0; 
                    for(int i = 0; i < deviceData.Count; i++)
                    {
                        if(i < deviceData.Count/2)
                        {
                            firstHalfRainfallSum += deviceData.ElementAt(i).Rainfall;
                        }
                        else
                        {
                            secondHalfRainfallSum += deviceData.ElementAt(i).Rainfall;
                        }
                    }

                    double firstHalfRainfallAvg = firstHalfRainfallSum/(deviceData.Count/2) ; 
                    double secondHalfRainfallAvg = secondHalfRainfallSum/(deviceData.Count/2) ; 

                    if (firstHalfRainfallAvg > secondHalfRainfallAvg)
                    {
                        trend = "Increasing";
                    }
                    else if (secondHalfRainfallAvg > firstHalfRainfallAvg)
                    {
                        trend = "Decreasing";
                    }
                    else
                    {
                        trend = "stable";
                    }

                }
                else if (deviceData.Count == 1)
                {
                    trend = "Only 1 reading so far...";
                }

                status.Add(new RainfallReadings
                {
                    DeviceID = d.DeviceID,
                    DeviceName = d.DeviceName,
                    Location = d.Location,
                    AverageRainfall = avg,
                    ColorStatus = color,
                    Trend = trend,
                });
            }

            return status;
        }
    }
}