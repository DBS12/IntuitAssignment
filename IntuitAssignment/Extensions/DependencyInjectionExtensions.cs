using IntuitAssignment.Api;
using IntuitAssignment.API.DataFetcher;
using IntuitAssignment.DAL;
using IntuitAssignment.DAL.Interfaces;
using IntuitAssignment.Engine;
using IntuitAssignment.Engine.Interfaces;
using System.Net.NetworkInformation;

namespace IntuitAssignment.Extensions
{
    public static class DependencyInjectionExtensions
    {
        public static void AddDAL(this IServiceCollection services) 
        {
            services.AddSingleton<IPlayerDAL, PlayersDAL>();
        }

        public static void AddEngine(this IServiceCollection services) 
        {
            services.AddSingleton<IEngineDataParser, CsvEngineParser>();
        }

        public static void AddDataFetchersEngine(this IServiceCollection services)
        {
            services
                .AddSingleton<BaseballDataFetcher>()
                .AddSingleton<RetrosheetDataFetcher>();
        }

        public static void AddApi(this IServiceCollection services)
        {
            services.AddSingleton<PlayerApi>();
        }
    }
}
