using CodebridgeTestTask.Dal.Context;

namespace CodebridgeTestTask.Tests.Helpers
{
    internal static class ContextExtensions
    {
        public static void AddEntities<TEntity>(
            this Context context, IEnumerable<TEntity> entities) where TEntity : class
        {
            foreach (TEntity entity in entities)
            {
                context.Set<TEntity>().Add(entity);
            }

            context.SaveChanges();
        }
    }
}
