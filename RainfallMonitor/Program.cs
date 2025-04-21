using RainfallMonitor.Processing;
//FileName: Processing.cs
//Date Created: 21-04-2025
//Author: Tien Hong TEOH
//Date Updated:  21-04-2025
//Updated By: Tien Hong TEOH
//
//Desc: Main Program
class Program{
    static void Main(string[] args)
    {
        var process = new FileProcessing();

        var output = process.ProcessingRainfall();

        Console.WriteLine("---Average Rainfall over the last 4 hours for each device:---");
        Console.WriteLine("{0,-10} {1,-15} {2,-15} {3,-15} {4,-15} {5,-15}", "DeviceID","DeviceName","Location","AverageRainfall","ColorStatus","Trend");

        foreach (var trend in output)
        {
            Console.WriteLine("{0,-10} {1,-15} {2,-15} {3,-15} {4,-15} {5,-15}", trend.DeviceID, trend.DeviceName, trend.Location,trend.AverageRainfall,trend.ColorStatus,trend.Trend);  
        }
    }
}