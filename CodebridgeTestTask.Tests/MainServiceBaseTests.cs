using AutoMapper;
using CodebridgeTestTask.Dal.Context;
using CodebridgeTestTask.Infrastructure.MappingProfiles;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System.Reflection;
using Xunit;

namespace CodebridgeTestTask.Tests
{
    public class MainServiceBaseTests 
    {
        private const string SampleDataDirectoryName = "SampleData";
        protected static Mapper GetDictionariesMapper()
        {
            DogProfile dogProfile = new DogProfile();
            MapperConfiguration configuration = new MapperConfiguration(cfg => cfg.AddProfile(dogProfile));
            return new Mapper(configuration);
        }
        protected static Context GetInMemoryTenantContext(string inMemoryDatabaseName)
        {
            DbContextOptions<Context> options = new DbContextOptionsBuilder<Context>()
                                                     .UseInMemoryDatabase(inMemoryDatabaseName)
                                                     .Options;

            return new Context(options);
        }

        public static List<TEntity> GetSampleEntities<TEntity>(string sampleDataFileName) where TEntity : class
        {
            string sampleDataFilePath = GetSampleDataFilePath(sampleDataFileName);
            string sampleDataJson = File.ReadAllText(sampleDataFilePath);
            List<TEntity> res = JsonConvert.DeserializeObject<List<TEntity>>(sampleDataJson);
            return res;
        }

        private static string GetSampleDataFilePath(string filename)
        {
            string executable = new Uri(Assembly.GetExecutingAssembly().CodeBase).LocalPath;
            string sampleDataDirectory = Path.Combine(Path.GetDirectoryName(executable), SampleDataDirectoryName);
            string res = Path.GetFullPath(Path.Combine(sampleDataDirectory, filename));
            return res;
        }

    }
}