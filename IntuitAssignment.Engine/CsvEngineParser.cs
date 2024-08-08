using CsvHelper;
using CsvHelper.Configuration;
using IntuitAssignment.DAL.Interfaces;
using IntuitAssignment.Engine.Interfaces;
using IntuitAssignments.DAL.Models;
using System.Runtime.CompilerServices;

namespace IntuitAssignment.Engine
{
    public class CsvEngineParser : IEngineDataParser
    {
        private readonly IPlayerDAL _playerDal;

        public CsvEngineParser(IPlayerDAL playerDal)
        {
            _playerDal = playerDal;
        }

        public async Task ParseData(string dataUrl)
        {
            await ReadCsvAsync(dataUrl);
        }

        public async Task ReadCsvAsync(string filePath, int batchSize = 1000)
        {
            using (var stream = new FileStream(filePath, FileMode.Open, FileAccess.Read, FileShare.Read, bufferSize: 4096, useAsync: true))
            using (var reader = new StreamReader(stream))
            using (var csv = new CsvReader(reader, new CsvConfiguration(System.Globalization.CultureInfo.InvariantCulture)))
            {
                var recordBuffer = new List<Player>();

                while (await csv.ReadAsync())
                {
                    var record = csv.GetRecord<Player>();
                    recordBuffer.Add(record);

                    if (recordBuffer.Count >= batchSize)
                    {
                        // Add to DB and update DB update state to of file to already read: X
                        _playerDal.InsertPlayers(recordBuffer);
                        recordBuffer.Clear();
                    }
                }

                // Add remaining records
                if (recordBuffer.Any())
                {
                    // Add to DB and update DB update state to of file to already read: X
                    _playerDal.InsertPlayers(recordBuffer);
                    recordBuffer.Clear();
                }
            }
        }
    }
}