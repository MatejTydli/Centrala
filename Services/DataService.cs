using System.Diagnostics;
using centrala.Models;

namespace centrala.Services;

public class DataService : IHostedService, IDisposable
{
    private Timer? _timer = null;

    public List<Sensor> GetData()
    {
        List<Sensor> data = new List<Sensor>();

        Process goodweData = new Process()
        {
            StartInfo = new ProcessStartInfo()
            {
                FileName = "/bin/python3",
                Arguments = "./wwwroot/goodwe_data.py",
                RedirectStandardOutput = true,
                CreateNoWindow = true
            }
        };

        goodweData.Start();

        while (!goodweData.StandardOutput.EndOfStream)
        {
            string[] lnSplit = goodweData.StandardOutput.ReadLine()!.Trim().Split(',');
            data.Add(new Sensor(lnSplit[0], lnSplit[1], lnSplit[2], lnSplit[3]));
        }

        return data;
    }

    private void WriteData(object? state)
    {
        Console.WriteLine("[{0}] DataService is now writing and saving data.", DateTime.Now.ToString("HH:mm:ss"));

        List<Sensor> data = GetData();
        GlobalValues.latestSensors = data.ToArray();

        var writer = new StreamWriter("Data/data.csv", append: true);
        string record = "";

        List<string> usableIds = GlobalValues.dataIDs.ToList();

        foreach (string usableId in usableIds)
            foreach (Sensor sensor in data)
                if (sensor.id == usableId) {
                    record += sensor.data + ',';
                    break;
                }

        writer.WriteLine(record[..^1]);
        writer.Close();
    }

    public Task StartAsync(CancellationToken stoppingToken)
    {
        _timer = new Timer(WriteData, null, TimeSpan.Zero,
            TimeSpan.FromSeconds(GlobalValues.dataServiceTimeSpanSeconds));

        return Task.CompletedTask;
    }

    public Task StopAsync(CancellationToken stoppingToken)
    {
        _timer?.Change(Timeout.Infinite, 0);
        return Task.CompletedTask;
    }

    public void Dispose()
    {
        _timer?.Dispose();
    }
}
