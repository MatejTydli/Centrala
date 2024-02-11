namespace centrala.Models;

public class Sensor
{
    public readonly string id;
    public readonly string name;
    public readonly string data;
    public readonly string unit;

    public Sensor(string id, string name, string data, string unit)
    {
        this.id = id;
        this.name = name;
        this.data = data;
        this.unit = unit;
    }
}
