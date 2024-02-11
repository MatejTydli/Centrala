namespace centrala.Models;

public class CSVData
{
    public List<string> data { get; set; } = new List<string>();
    public readonly string name;

    public CSVData(string name)
    {
        this.name = name;
    }

    public bool isEmpty() {
        foreach (string item in data)
            if (!string.IsNullOrEmpty(item))
                return false;

        return true;
    }
}
