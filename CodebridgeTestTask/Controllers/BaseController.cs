using CodebridgeTestTask.ActionResults;
using CodebridgeTestTask.Common.Paggination;
using CodebridgeTestTask.Infrastructure.Attributes;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.RateLimiting;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace CodebridgeTestTask.Controllers
{
    [ApiController]
    [ValidatePagingAttribute]
    [EnableRateLimiting("fixed")] // Enable rate limiting
    public class BaseController : ControllerBase
    {
        public BaseController() { }
        /// <summary>
        ///     Default page size
        /// </summary>
        public const int DefaultPageSize = 10;

        /// <summary>
        ///     Default page number
        /// </summary>
        public const int DefaultPageNumber = 1;

        /// <summary>
        ///     Paginated result
        /// </summary>
        /// <typeparam name="T">Response data</typeparam>
        /// <param name="results"></param>
        /// <returns></returns>
        public static PaginationResult<T> PaginatedResult<T>(IEnumerable<T> results) where T : class
        {
            return new PaginationResult<T>(new PaginatedDataModel<T>(results));
        }

        /// <summary>
        ///     Paginated result
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="results"></param>
        /// <returns></returns>
        public static PaginationResult<T> PaginatedResult<T>(PaginatedDataModel<T> results) where T : class
        {
            return new PaginationResult<T>(results);
        }

        /// <summary>
        ///     Paginated result
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="results"></param>
        /// <returns></returns>
        public IActionResult ResolveResponse<T>(T results) where T : class
        {
            return results == null ? NoContent() : (IActionResult)Ok(results);
        }

        /// <summary>
        ///     Paginated result
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="results"></param>
        /// <returns></returns>
        public IActionResult ResolveResponse<T>(IEnumerable<T> results) where T : class
        {
            return results == null || !results.Any() ? NoContent() : (IActionResult)Ok(results);
        }

        /// <summary>
        ///     Paginated result
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="results"></param>
        /// <returns></returns>
        public IActionResult ResolveResponse<T>(PaginatedDataModel<T> results) where T : class
        {
            return results?.Data == null || !results.Data.Any()
                       ? NoContent()
                       : (IActionResult)PaginatedResult(results);
        }
    }
}


