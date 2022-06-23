using Microsoft.EntityFrameworkCore;
using MyBlogProject.CORE.Entity;
using MyBlogProject.MODEL.Entities;
using MyBlogProject.MODEL.Maps;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyBlogProject.MODEL.Context
{
    public class BlogContext : DbContext
    {
        public BlogContext(DbContextOptions<BlogContext>options):base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new CategoryMap());
            modelBuilder.ApplyConfiguration(new PostMap());
            modelBuilder.ApplyConfiguration(new UserMap());



            base.OnModelCreating(modelBuilder);
        }

        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //{
        //    optionsBuilder.UseSqlServer("conn string");

        //    base.OnConfiguring(optionsBuilder);
        //}
        public DbSet<Category> Categories { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Post> Posts { get; set; }

        public override int SaveChanges()
        {
            var modifiedEntries = ChangeTracker.Entries().Where(x=>x.State == EntityState.Modified|| x.State == EntityState.Added).ToList();
            string computerName = Environment.MachineName;
            string ipAddress = "127.0.0.1";
            DateTime date = DateTime.Now;

            foreach (var item in modifiedEntries)
            {
                CoreEntity entity = item.Entity as CoreEntity;

                if (item != null)
                {
                    switch (item.State)
                    {
                        case EntityState.Modified:
                            entity.UpdatedComputerName = computerName;
                            entity.UpdatedIP = ipAddress;
                            entity.UpdatedDate = date;
                            break;
                        case EntityState.Added:
                            entity.CreatedComputerName = computerName;
                            entity.CreatedIP = ipAddress;
                            entity.CreatedDate = date;
                            break;
                    }
                }
            }







            return base.SaveChanges();
        }
    }
}
