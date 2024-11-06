using CodebridgeTestTask.Dal.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CodebridgeTestTask.Dal.EntityTypeConfigurations
{
    class DogConfiguration : IEntityTypeConfiguration<Dog>
    {
        public void Configure(EntityTypeBuilder<Dog> builder)
        {
            builder
               .ToTable("Dog", "dbo");

            builder
               .HasKey(e => e.Id);
            builder
                .Property(e => e.Name)
                .IsRequired()
                .HasMaxLength(25);
            builder
                .HasIndex(e => e.Name)
                .IsUnique();
            builder
                .Property(e =>e.Color)
                .IsRequired()
                .HasMaxLength(50);
            builder
                .Property(e => e.TailLength)
                .IsRequired()
                .HasColumnType("integer");
            builder
                .Property(e=>e.Weight)
                .IsRequired()
                .HasColumnType ("integer");
                


        }
    }
}
