namespace centrala.Models;

public static class ConverterValidator
{
    public static bool convertValidateDate(string? d, out DateTime convertedDate)
    {
        convertedDate = DateTime.Today;
        DateTime preConvertedDate;
        bool valid = false;

        if (d == null) valid = true;
        else if (DateTime.TryParse(d, out preConvertedDate))
        {
            valid = true;
            convertedDate = preConvertedDate;
        }

        return valid;
    }

    public static bool convertValidateOffset(int? o, out int convertedOffset)
    {
        convertedOffset = 0;
        bool valid = false;

        if (o == null) valid = true;
        else if (Math.Abs((int)o) < 36525)
        {
            valid = true;
            convertedOffset = (int)o;
        }

        return valid;
    }

    public static bool isEmpty(CSVData[] array) {
        foreach (CSVData item in array)
            if (!item.isEmpty())
                return false;

        return true;
    }
}