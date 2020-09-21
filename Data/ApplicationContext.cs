using Microsoft.EntityFrameworkCore;
using Exam.Models;

namespace Exam.Data
{
    public class ApplicationContext : DbContext
    {
        public DbSet<Country> Counties { get; set; }
        public DbSet<City> Cities { get; set; }

        private readonly string connectionString;
        public ApplicationContext(string connectionString)
        {
            this.connectionString = connectionString;

            Database.EnsureCreated();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            //optionsBuilder.UseNpgsql("Server=localhost; Database = scaffolding; User ID = postgres; Password = postgres;");
            optionsBuilder.UseNpgsql(connectionString);
        }
    }
}