using Microsoft.EntityFrameworkCore;
using System.Data;
using System;
using univercityProject.Models.DBModel;
using System.Threading.Tasks;
using System.Threading;

namespace univercityProject.Models.Context
{
    public interface IUNDBContext
    {
        public DbSet<User> users { get; set; }
        public DbSet<Request> Requests { get; set; }
        int SaveChanges(bool acceptAllChangesOnSuccess);
        int SaveChanges();
        Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess, CancellationToken cancellationToken = new CancellationToken());
        Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken());
    }
    public class UNDBContext : DbContext,IUNDBContext
    {
        public UNDBContext(DbContextOptions options) : base(options)
        {

        }
        public DbSet<User> users { get; set; }
        public DbSet<Request> Requests { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            SeedData(modelBuilder);
            //modelBuilder.Entity<User>().HasIndex(x => x.PhoneNumber).IsUnique();
            ApplyQueryFilter(modelBuilder);
        }
        private void SeedData(ModelBuilder modelBuilder)
        {
            //modelBuilder.Entity<User>().HasData(new User() { Email = "test@gmail.com", FullName = "system", Id = 1, InsertTime = DateTime.Now, IsActive = true, IsRemoved = false, Password = "b59c67bf196a4758191e42f76670ceba" });
        }
        private void ApplyQueryFilter(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>().HasQueryFilter(p => !p.IsRemoved);
        }
    }
}
