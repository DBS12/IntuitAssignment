using CsvHelper;

namespace IntuitAssignment.Utils
{
    public static class CsvReaderExtensions
    {
        public static bool TryGetRecord<T>(this CsvReader reader, out T res) where T : class
        {
            res = null;
            try
            {
                res = reader.GetRecord<T>();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
