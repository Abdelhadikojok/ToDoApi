using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using ToDoApi.Models;

namespace ToDoApi.Data.modelConfigration
{
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.HasKey(u => u.UserId);
            builder.Property(u => u.Email).IsRequired().HasMaxLength(255);
            builder.Property(u => u.Password).IsRequired().HasMaxLength(255);
        }
    }
}
