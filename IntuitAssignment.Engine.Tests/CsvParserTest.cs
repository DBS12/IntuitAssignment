using IntuitAssignment.DAL.Interfaces;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using IntuitAssignments.DAL.Models;

namespace IntuitAssignment.Engine.Tests
{
    [TestClass]
    public class CsvParserTest
    {
        private Mock<IPlayerDAL> _dal;
        private CsvEngineParser _csvParser;
        private string _fileName = "temp.csv";

        [TestInitialize]
        public async Task TestInitialize()
        {
            _dal = new Mock<IPlayerDAL>();
            _csvParser = new CsvEngineParser(_dal.Object);
            await createCsvFile(new List<string>()
            {
                "playerID,birthYear,birthMonth,birthDay,birthCountry,birthState,birthCity,deathYear,deathMonth,deathDay,deathCountry,deathState,deathCity,nameFirst,nameLast,nameGiven,weight,height,bats,throws,debut,finalGame,retroID,bbrefID",
                "aardsda01,1981,12,27,USA,CO,Denver,,,,,,,David,Aardsma,David Allan,215,75,R,R,2004-04-06,2015-08-23,aardd001,aardsda01",
                "aaronha01,1934,2,5,USA,AL,Mobile,,,,,,,Hank,Aaron,Henry Louis,180,72,R,R,1954-04-13,1976-10-03,aaroh101,aaronha01",
                "aaronto01,1939,8,5,USA,AL,Mobile,1984,8,16,USA,GA,Atlanta,Tommie,Aaron,Tommie Lee,190,75,R,R,1962-04-10,1971-09-26,aarot101,aaronto01",
                "aasedo01,1954,9,8,USA,CA,Orange,,,,,,,Don,Aase,Donald William,190,75,R,R,1977-07-26,1990-10-03,aased001,aasedo01"
            });
        }

        [TestCleanup]
        public void Cleanup()
        {
            File.Delete(_fileName);
        }

        [DataRow(2, 2)]
        [DataRow(2, 3)]
        [DataRow(1, 4)]
        [DataRow(4, 1)]
        [TestMethod]
        public async Task Test_ParseCSV_VerifyFileBatching(int expectedInsertionCalls, int batchSize)
        {
            _dal.Setup(dal => dal.InsertPlayers(It.IsAny<IEnumerable<Player>>(), It.IsAny<CancellationToken>(), 1)).ReturnsAsync(true);

            await _csvParser.ParseData(_fileName, new CancellationToken(), batchSize);

            // Assert
            _dal.Verify(dal => dal.InsertPlayers(It.IsAny<IEnumerable<Player>>(), It.IsAny<CancellationToken>(), 1), Times.Exactly(expectedInsertionCalls));
        }

        private async Task createCsvFile(IEnumerable<string> content)
        {
            await File.WriteAllLinesAsync(_fileName, content);
        }
    }
}