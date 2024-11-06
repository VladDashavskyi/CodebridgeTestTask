using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodebridgeTestTask.Common.Paggination
{
    /// <summary>
    /// Represents the paginated data
    /// </summary>
    /// <typeparam name="T">Generic type</typeparam>
    public class PaginatedDataModel<T> where T : class
    {
        /// <summary>
        /// Default constructor
        /// </summary>
        public PaginatedDataModel()
        {
            Data = new List<T>();
            PageNumber = 1;
        }

        public PaginatedDataModel(IEnumerable<T> data)
        {
            Data = data;
            PageNumber = 1;
        }

        /// <summary>
        /// Gets or sets the count of pages with a maximum of 30 items on page.
        /// </summary>
        public int TotalItems { get; set; }

        /// <summary>
        /// Gets or sets the current page number.
        /// </summary>
        public int PageNumber { get; set; }

        /// <summary>
        /// Gets or sets the paging state.
        /// </summary>
        public int PageSize { get; set; }

        /// <summary>
        /// Gets or sets the array of data.
        /// </summary>
        public IEnumerable<T> Data { get; set; }
    }
}
