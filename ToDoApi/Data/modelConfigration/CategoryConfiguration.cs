using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using ToDoApi.Models;

namespace ToDoApi.Data.modelConfigration
{
    public class CategoryConfiguration : IEntityTypeConfiguration<Category>
    {
        public void
    Configure(EntityTypeBuilder<Category> builder)
        {
            builder.HasKey(c => c.CategoryId);
            builder.Property(c => c.Name).IsRequired().HasMaxLength(50);
        }
    }
}
