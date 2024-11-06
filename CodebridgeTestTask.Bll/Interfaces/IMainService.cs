using CodebridgeTestTask.Common.DTO;
using CodebridgeTestTask.Common.Enum;
using CodebridgeTestTask.Common.Paggination;
using CodebridgeTestTask.Dal.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodebridgeTestTask.Bll.Interfaces
{
    public interface IMainService
    {
        void CreateInsertDataBase();
        Task<PaginatedDataModel<DogDto>> GetDogsAsync(string attribute, Order order, int pageSize, int pageNumber);
        Task<DogDto> CreateDogAsync(DogDto dogDto);
        Task<string> GetAssemblyVersionAsync();
    }
}
