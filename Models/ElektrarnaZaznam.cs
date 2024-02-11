namespace centrala.Models;

public class ElektrarnaZaznam
{
    public CSVData[] GetData(string date)
    {
        var reader = new StreamReader("Data/data.csv");
        string? ln;

        CSVData[] data = new CSVData[GlobalValues.dataIDs.Length];

        for (int i = 0; i < data.Length; i++)
            data[i] = new CSVData(GlobalValues.dataIDs[i]);

        while ((ln = reader.ReadLine()) != null)
        {
            if (ln.Contains(date))
            {
                string[] splited = ln.Replace(date, "").Split(',');
                for (int i = 0; i < data.Length; i++)
                    data[i].data.Add(splited[i]);
            }
        }

        return data;
    }

    public Sensor? GetBatterySOC()
    {
        foreach (Sensor sensor in GlobalValues.latestSensors)
            if (sensor.id == "battery_soc")
                return sensor;

        return null;
    }
}
