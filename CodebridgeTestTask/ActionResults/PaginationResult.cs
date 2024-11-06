using CodebridgeTestTask.Common.Paggination;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Primitives;
using System.Threading.Tasks;

namespace CodebridgeTestTask.ActionResults
{
    /// <summary>
    ///     Represents the pagination result
    /// </summary>
    /// <typeparam name="T">Generic type of response data</typeparam>
    public class PaginationResult<T> : IActionResult where T : class
    {
        /// <summary>
        ///     Generic response data
        /// </summary>
        private readonly PaginatedDataModel<T> _paginatedData;

        /// <summary>
        ///     Default constructor
        /// </summary>
        /// <param name="data">Generic type of response data</param>
        public PaginationResult(PaginatedDataModel<T> data)
        {
            _paginatedData = data;
        }

        /// <summary>
        ///     Execute result
        /// </summary>
        /// <param name="context">Context object for execution of action which has been selected as part of an HTTP request.</param>
        /// <returns></returns>
        public async Task ExecuteResultAsync(ActionContext context)
        {
            ObjectResult result = new ObjectResult(_paginatedData.Data)
            {
                StatusCode = 200
            };

            context.HttpContext.Response.Headers.Add("PageNumber"
                                                    , new StringValues(_paginatedData.PageNumber.ToString()));

            context.HttpContext.Response.Headers.Add("PageSize"
                                                    , new StringValues(_paginatedData.PageSize.ToString()));

            context.HttpContext.Response.Headers.Add("TotalItems"
                                                    , new StringValues(_paginatedData.TotalItems.ToString()));

            await result.ExecuteResultAsync(context);
        }
    }
}