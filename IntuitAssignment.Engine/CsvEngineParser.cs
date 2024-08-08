using CsvHelper;
using CsvHelper.Configuration;
using IntuitAssignment.DAL.Interfaces;
using IntuitAssignment.Engine.Interfaces;
using IntuitAssignment.Utils;
using IntuitAssignments.DAL.Models;

namespace IntuitAssignment.Engine
{
    public class CsvEngineParser : IEngineDataParser
    {
        private readonly IPlayerDAL _playerDal;

        public CsvEngineParser(IPlayerDAL playerDal)
        {
            _playerDal = playerDal;
        }

        public async Task ParseData(string path, CancellationToken ct, int batchSize = 1000)
        {
            await ReadCsvAsync(path, batchSize, ct);
        }

        public async Task ReadCsvAsync(string filePath, int batchSize, CancellationToken ct)
        {
            using (var stream = new FileStream(filePath, FileMode.Open, FileAccess.Read, FileShare.Read, bufferSize: 4096, useAsync: true))
            using (var reader = new StreamReader(stream))
            using (var csv = new CsvReader(reader, new CsvConfiguration(System.Globalization.CultureInfo.InvariantCulture)))
            {
                var recordBuffer = new List<Player>();

                while (await csv.ReadAsync())
                {
                    if (!csv.TryGetRecord<Player>(out var record))
                    {
                        continue;
                    }
                    recordBuffer.Add(record);

                    if (recordBuffer.Count >= batchSize)
                    {
                        var insertionSucceded = await _playerDal.InsertPlayers(recordBuffer, ct);
                        recordBuffer.Clear();

                        if (!insertionSucceded)
                        {
                            // Log DB is not responsible
                            return;
                        }
                    }
                }

                if (recordBuffer.Any())
                {
                    await _playerDal.InsertPlayers(recordBuffer, ct);
                    recordBuffer.Clear();
                }
            }
        }
    }
}