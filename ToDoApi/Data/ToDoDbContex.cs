using Microsoft.EntityFrameworkCore;
using System;
using ToDoApi.Data.modelConfigration;
using ToDoApi.Models;


namespace ToDoApi.Data
{
    public class ToDoDbContex : DbContext
    {
        public ToDoDbContex(DbContextOptions<ToDoDbContex> options) : base(options) { }

        public DbSet<User> Users { get; set; }
        public DbSet<TaskCard> TasksCard { get; set; }
        public DbSet<Category> Categories { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);


            modelBuilder.ApplyConfigurationsFromAssembly(typeof(ToDoDbContex).Assembly);
        }
    }

}
