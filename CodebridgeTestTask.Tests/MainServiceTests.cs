using CodebridgeTestTask.Bll.Services;
using CodebridgeTestTask.Common.DTO;
using CodebridgeTestTask.Common.Enum;
using CodebridgeTestTask.Common.Paggination;
using CodebridgeTestTask.Dal.Context;
using CodebridgeTestTask.Dal.Entities;
using CodebridgeTestTask.Tests.Helpers;
using FluentAssertions;
using Microsoft.Data.SqlClient;
using Microsoft.VisualStudio.TestPlatform.TestHost;
using System.ComponentModel.Design;
using System.Drawing.Printing;
using Xunit;

namespace CodebridgeTestTask.Tests
{
    public class MainServiceTests : MainServiceBaseTests
    {
        private const string SampleDataFileName = "Data.json";

        [Theory]
        [InlineData(1)]
        [InlineData(2)]
        public async void GetDogs(int pageSize)
        {
            // Arrange.
            var inMemoryDatabaseName = Guid.NewGuid().ToString();

            await using (Context context = GetInMemoryTenantContext(inMemoryDatabaseName))
            {
                context.AddEntities(GetSampleEntities<Dog>(SampleDataFileName));
            }

            PaginatedDataModel<DogDto> dogsDto = null;

            await using (Context context = GetInMemoryTenantContext(inMemoryDatabaseName))
            {
                MainService mainService = new MainService(
                                                                            context,
                                                                            GetDictionariesMapper()
                                                                           );
                // Act.
                dogsDto = await mainService.GetDogsAsync(string.Empty, Order.Descending, pageSize, 1);
            }

            // Assert.
            dogsDto.Data.Count().Should().Be(pageSize);
        }

        [Fact]
        public async void CreateDogWithExistName()
        {
            DogDto dogDto = new DogDto()
            {
                Name = "Doggy",
                Color = "white",
                TailLength = 55,
                Weight = 20
            };
            DogDto returnDogsDto = null;
            var inMemoryDatabaseName = Guid.NewGuid().ToString();

            await using (Context context = GetInMemoryTenantContext(inMemoryDatabaseName))
            {
                context.AddEntities(GetSampleEntities<Dog>(SampleDataFileName));
            }

            await using (Context context = GetInMemoryTenantContext(inMemoryDatabaseName))
            {
                MainService mainService = new MainService(
                                                                            context,
                                                                            GetDictionariesMapper()
                                                                           );
                // Act.
                try
                {
                    returnDogsDto = await mainService.CreateDogAsync(dogDto);
                }
                catch(Exception ex)
                {
                    ex.Message.Should().Be("Dog with the same name already exists in DB.");
                }
            }
        }

        [Fact]
        public async void CreateDogWithoutErrors()
        {
            DogDto dogDto = new DogDto()
            {
                Name = "NoError",
                Color = "white",
                TailLength = 55,
                Weight = 20
            };
            DogDto returnDogsDto = null;
            var inMemoryDatabaseName = Guid.NewGuid().ToString();

            await using (Context context = GetInMemoryTenantContext(inMemoryDatabaseName))
            {
                context.AddEntities(GetSampleEntities<Dog>(SampleDataFileName));
            }

            await using (Context context = GetInMemoryTenantContext(inMemoryDatabaseName))
            {
                MainService mainService = new MainService(
                                                                            context,
                                                                            GetDictionariesMapper()
                                                                           );
                // Act.
                    returnDogsDto = await mainService.CreateDogAsync(dogDto);
                returnDogsDto.Should().NotBeNull();
            }
        }

        [Fact]
        public async void VersionChange()
        {
            const string version = "CodebridgeTestTask.Bll 1.0.1.0";

            var inMemoryDatabaseName = Guid.NewGuid().ToString();

            await using (Context context = GetInMemoryTenantContext(inMemoryDatabaseName))
            {
                MainService mainService = new MainService(
                                                                            context,
                                                                            GetDictionariesMapper()
                                                                           );
                // Act.
                string result = await mainService.GetAssemblyVersionAsync();

                result.Should().Be(version);

            }
        }


    }
}