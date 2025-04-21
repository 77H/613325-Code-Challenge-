Author: Tien Hong TEOH
Program Name: Rainfall Monitor
Github Repo: https://github.com/77H/613325-Code-Challenge-.git
Created: 21-04-2025

Input Files:
    Devices.csv
    Eg:
        Device ID,Device Name,Location
        10451,Gauge 1,Biyamiti
        11271,Gauge 2,Balule

    Data*.csv
    Eg:
        Device ID,Time,Rainfall
        11271,05/06/2020 12:00,3
        10451,05/06/2020 12:00,0

Output Sample:
---Average Rainfall over the last 4 hours for each device:---
DeviceID   DeviceName      Location        AverageRainfall     ColorStatus           Trend
10451      Gauge 1         Biyamiti                  1.000           Green      Increasing
11271      Gauge 2         Balule                    1.800           Green      Decreasing
25832      Gauge 3         Punda                    11.444           Amber      Increasing
46759      Gauge 4         Maroela                  18.333             Red      Increasing

Assumptions:
-The provided CSV represents all available data
-The current time is based on the latest timestamp found within the dataset
-Files sit within 2 directories above (where files will sit to be further implemented and updated)
-The trend logic is comparing the average first and second half to determine the trend
-Duplicate device IDs will be displayed with their respective locations and names until further instruction has been provided
-If there is no data present for device, it will be excluded from output