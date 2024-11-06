using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.EntityFrameworkCore;
using CodebridgeTestTask.Dal.Entities;
using CodebridgeTestTask.Dal.EntityTypeConfigurations;
using System.Linq;
using System.Threading.Tasks;

namespace CodebridgeTestTask.Dal.Context
{
    public class Context : DbContext
    {
        private IDbContextTransaction _transaction;

        public Context(DbContextOptions<Context> options) : base(options)
        {

        }
        public DbSet<Dog> Dogs { get; set; }

        public async Task<bool> SaveChangesAsync()
        {
            int changes = ChangeTracker
                          .Entries()
                          .Count(p => p.State == EntityState.Modified
                                   || p.State == EntityState.Deleted
                                   || p.State == EntityState.Added);

            if (changes == 0)
            {
                return true;
            }

            return await base.SaveChangesAsync() > 0;
        }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new DogConfiguration());
        }
    }
}
