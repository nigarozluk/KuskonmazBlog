using Microsoft.EntityFrameworkCore;
using Project.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Project.DAL.Repository.Concrete.EntityFramework
{
    public class KuskonmazContext:DbContext
    {
        public KuskonmazContext(DbContextOptions<KuskonmazContext> options)
           : base(options)
        {

        }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Post> Posts { get; set; }
        public DbSet<Image> Images { get; set; }
        public DbSet<PostTag> PostTags { get; set; }
        public DbSet<PostCategory> PostCategories { get; set; }
        public DbSet<Tag> Tags { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<About> Abouts { get; set; }
        public DbSet<Contact> Contacts { get; set; }
        public DbSet<Admin> Admins { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<PostCategory>().HasKey(pk => new { pk.postId, pk.categoryId });
            modelBuilder.Entity<PostTag>().HasKey(pk => new { pk.postId, pk.tagId });
        }
    }
}
