
namespace IntuitAssignment.Engine.Interfaces
{
    public interface IEngineDataParser
    {
        Task ParseData(string dataUrl, CancellationToken ct, int batchSize = 1000);
    }
}
