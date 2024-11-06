using CodebridgeTestTask.Bll.Interfaces;
using CodebridgeTestTask.Common.DTO;
using CodebridgeTestTask.Common.Enum;
using CodebridgeTestTask.Common.Paggination;
using CodebridgeTestTask.Dal.Entities;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.RateLimiting;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CodebridgeTestTask.Controllers
{
    public class MainController : BaseController
    {
        private readonly IMainService _mainService;
        public MainController(IMainService mainService) : base() {
            _mainService = mainService;
        }

        /// <summary>
        ///     Gets the ping message
        /// </summary>
        /// <response code="200">Successful response</response>
        [HttpGet]
        [Route("/Ping")]
        [DisableRateLimiting] 
        public async Task<IActionResult> GetPingAsync()
        {     
            return ResolveResponse(await _mainService.GetAssemblyVersionAsync());
        }

        /// <summary>
        ///     Get dogs
        /// <param name="attribute">The atribute identifier</param>
        /// <param name="order">The order identifier</param>
        /// <param name="pageNumber">The page number identifier</param>
        /// <param name="pageSize">The page size identifier</param>
        /// </summary>
        /// <response code="200">Successful response</response>
        [HttpGet]
        [Route("/Dogs")]
        public async Task<IActionResult> GetDogAsync([FromQuery] string attribute
            , [FromQuery] Order order
            , [FromQuery] int pageSize = DefaultPageSize
            , [FromQuery] int pageNumber = DefaultPageNumber)
        {
            PaginatedDataModel<DogDto> result = await _mainService.GetDogsAsync(attribute, order, pageSize, pageNumber);
            return ResolveResponse(result);
        }

        [HttpPost]
        [Route("/Dog")]
        public async Task<IActionResult> CreateDogAsync([FromBody]DogDto dogDto)
        {
            var result = await _mainService.CreateDogAsync(dogDto);
            return ResolveResponse(result);
        }
    }

   
}

