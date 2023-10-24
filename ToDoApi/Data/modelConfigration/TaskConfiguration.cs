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
            builder.Property(t => t.Status).IsRequired().HasMaxLength(50); // make it .hascoulnType(varchar).maxleng(255) 
            builder.Property(t => t.DueDate).IsRequired();
            builder.Property(t => t.EstimateDate).IsRequired();
            builder.Property(t => t.Title).IsRequired().HasMaxLength(255);

            // Define foreign key relationships if needed
            builder.HasOne(t => t.User)
                .WithMany(u => u.Tasks)
                .HasForeignKey(t => t.UserId)
                .OnDelete(DeleteBehavior.Cascade); // means if the user is deleted all tasks will deleted also

                //builder.hasindex(x=>x.officeId).isunique() ==> this means that this forign key when you enter data must not be rpeated
                //hon wala mara 3mlet .IsRequierd lesh??
                

            builder.HasOne(t => t.Category)
                .WithMany(c => c.Tasks)
                .HasForeignKey(t => t.CategoryId)
                .OnDelete(DeleteBehavior.Cascade);

            //jreb 2nak t3mel default data ==> builder.HasData(loadTasks()) ==>#10 (29:00) 
        }
    }
}
