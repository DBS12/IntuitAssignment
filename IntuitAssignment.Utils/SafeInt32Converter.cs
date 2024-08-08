using CsvHelper;
using CsvHelper.Configuration;
using CsvHelper.TypeConversion;
using System;

public class SafeInt32Converter : DefaultTypeConverter
{
    public override object ConvertFromString(string text, IReaderRow row, MemberMapData memberMapData)
    {
        if (string.IsNullOrWhiteSpace(text))
        {
            return 0; // Default value for empty or null strings
        }

        if (int.TryParse(text, out int result))
        {
            return result;
        }

        throw new NotSupportedException();
    }
}