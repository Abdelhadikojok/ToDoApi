using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using ToDoApi.Models;

namespace ToDoApi.Data.modelConfigration
{
    public class TaskConfiguration : IEntityTypeConfiguration<TaskCard>
    {
        public void Configure(EntityTypeBuilder<TaskCard> builder)
        {
            builder.HasKey(t => t.TaskId);
            builder.Property(t => t.Status).IsRequired().HasMaxLength(50);
            builder.Property(t => t.DueDate).IsRequired();
            builder.Property(t => t.EstimateDatenumber).HasColumnType("numeric");
            builder.Property(t => t.EstimateDateUnit);
            builder.Property(t => t.Title).IsRequired().HasMaxLength(255);
            builder.Property(t => t.importance);

            builder.HasOne(t => t.User)
                .WithMany(u => u.Tasks)
                .HasForeignKey(t => t.UserId)
                .OnDelete(DeleteBehavior.Cascade); 
                

            builder.HasOne(t => t.Category)
                .WithMany(c => c.Tasks)
                .HasForeignKey(t => t.CategoryId)
                .OnDelete(DeleteBehavior.Cascade);

        }
    }
}
