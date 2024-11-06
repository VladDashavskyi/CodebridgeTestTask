using CodebridgeTestTask.Bll.Implementations.Specifications;
using CodebridgeTestTask.Bll.Interfaces;
using CodebridgeTestTask.Common.DTO;
using CodebridgeTestTask.Common.Enum;
using CodebridgeTestTask.Common.Paggination;
using CodebridgeTestTask.Dal.Context;
using CodebridgeTestTask.Dal.Entities;
using Microsoft.EntityFrameworkCore;
using AutoMapper.QueryableExtensions;
using AutoMapper;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using CodebridgeTestTask.Bll.Extension;

namespace CodebridgeTestTask.Bll.Services
{
    public class MainService : IMainService
    {
        private readonly IMapper _mapper;
        private readonly Context _context;
        public MainService(Context context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public void CreateInsertDataBase()
        {
            _context.Database.EnsureCreated();

            Dog dog = new Dog()
            {
                Name = "Neo",
                Color = "red & amber",
                TailLength = 22,
                Weight = 35
            };
            _context.Dogs.Add(dog);

            dog = new Dog()
            {
                Name = "Jessy",
                Color = "black & white",
                TailLength = 7,
                Weight = 14
            };
            _context.Dogs.Add(dog);
            _context.SaveChanges();
        }

        public async Task<DogDto> CreateDogAsync(DogDto dogDto)
        {
            _context.Database.EnsureCreated();
            if (_context.Dogs.AsNoTracking().Any(w => w.Name == dogDto.Name))
            {
                throw new ValidationException($"Dog with the same name: '{dogDto.Name}', already exists in DB.");
            }


            Dog dog = _mapper.Map<Dog>(dogDto);

            EntityEntry<Dog> result = _context.Dogs.Add(dog);

            await _context.SaveChangesAsync();
            return _mapper.Map<DogDto>(result.Entity);
        }


        public async Task<PaginatedDataModel<DogDto>> GetDogsAsync(string attribute, Order order, int pageSize, int pageNumber)
        {

            IQueryable<Dog> query = _context.Dogs.AsNoTracking();

            int totalItems = await query.CountAsync();

           
            List<DogDto> result = await query
                    .ApplyPagination(pageSize, pageNumber)
                    .ProjectTo<DogDto>(_mapper.ConfigurationProvider)
                    .ToListAsync();

            if (!string.IsNullOrEmpty(attribute) && order == Order.Ascending && HasProperty(result?.FirstOrDefault(), attribute))
            {
                result = result.OrderBy(o => o.GetType()
                                       .GetProperty(attribute)
                                       .GetValue(o, null)).ToList();
            }

            if (!string.IsNullOrEmpty(attribute) && order == Order.Descending && HasProperty(result?.FirstOrDefault(), attribute))
            {
                result = result.OrderByDescending(o => o.GetType()
                                       .GetProperty(attribute)
                                       .GetValue(o, null)).ToList();
            }


            return new PaginatedDataModel<DogDto>
            {
                PageSize = pageSize,
                PageNumber = pageNumber,
                Data = result,
                TotalItems = totalItems
            };
        }
        private bool HasProperty(object obj, string propertyName)
        {
            return obj.GetType().GetProperty(propertyName) != null;
        }

        public async Task<string> GetAssemblyVersionAsync()
        {
            return $"{GetType().Assembly.GetName().Name} {GetType().Assembly.GetName().Version.ToString()}";
        }

    }
}
