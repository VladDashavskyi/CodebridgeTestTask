using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodebridgeTestTask.Bll.Implementations.Specifications
{
    internal static class SpecificationIQueryableExtensions
    {
        /// <summary>
        /// Augments the initial query based on the pagination parameters provided.
        /// </summary>
        /// <typeparam name="TEntity">Type of the entity to compose queries for.</typeparam>
        /// <param name="query">The initial query.</param>
        /// <param name="pageSize">Number of the entity instances per page.</param>
        /// <param name="pageNumber">Page index (starts from 1).</param>
        /// <returns>The query paginated based on the parameters provided.</returns>
        public static IQueryable<TEntity> ApplyPagination<TEntity>(
            this IQueryable<TEntity> query, int pageSize, int pageNumber) where TEntity : class
        {
            int skip = pageSize * (pageNumber - 1);
            IQueryable<TEntity> res = query.Skip(skip).Take(pageSize);
            return res;
        }
    }
}

