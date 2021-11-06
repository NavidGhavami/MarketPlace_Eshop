using System;
using System.Linq;
using Application.Interfaces.Contexts;
using Domain.Attributes;
using Domain.Users;
using Microsoft.EntityFrameworkCore;

namespace Persistence.Contexts
{
    public class DatabaseContext : DbContext, IDataBaseContext
    {
        public DatabaseContext(DbContextOptions options) : base(options)
        {

        }

        public DbSet<User> Users { get; set; }




        protected override void OnModelCreating(ModelBuilder builder)
        {

            foreach (var entityType in builder.Model.GetEntityTypes())
            {
                if (entityType.ClrType.GetCustomAttributes(typeof(AuditableAttribute), true).Length > 0)
                {
                    builder.Entity(entityType.Name).Property<DateTime>("InsertTime");
                    builder.Entity(entityType.Name).Property<DateTime?>("UpdateTime");
                    builder.Entity(entityType.Name).Property<DateTime?>("RemoveTime");
                    builder.Entity(entityType.Name).Property<bool>("IsRemoved");

                }
            }

            base.OnModelCreating(builder);
        }

        public override int SaveChanges()
        {

            var modifiedEntries = ChangeTracker.Entries()
                .Where(p => p.State == EntityState.Added ||
                            p.State == EntityState.Modified ||
                            p.State == EntityState.Deleted);


            foreach (var item in modifiedEntries)
            {
                var entityType = item.Context.Model.FindEntityType(item.Entity.GetType());

                var insert = entityType.FindProperty("InsertTime");
                var update = entityType.FindProperty("UpdateTime");
                var remove = entityType.FindProperty("RemoveTime");
                var isRemove = entityType.FindProperty("IsRemoved");

                if (item.State == EntityState.Added && insert != null)
                {
                    item.Property("InsertTime").CurrentValue = DateTime.Now;
                }

                if (item.State == EntityState.Modified && update != null)
                {
                    item.Property("UpdateTime").CurrentValue = DateTime.Now;
                }

                if (item.State == EntityState.Deleted && remove != null && isRemove != null)
                {
                    item.Property("RemoveTime").CurrentValue = DateTime.Now;
                    item.Property("IsRemoved").CurrentValue = true;
                }
            }

            return base.SaveChanges();
        }
    }
}
