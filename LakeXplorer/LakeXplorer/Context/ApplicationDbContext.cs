using LakeXplorer.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Security.Cryptography.Xml;

namespace LakeXplorer.Context
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        // Define DbSet properties for each entity to create database tables
        public DbSet<Users> Users { get; set; }
        public DbSet<Lakes> Lakes { get; set; }
        public DbSet<LakeSightings> LakeSightings { get; set; }
        public DbSet<Likes> Likes { get; set; }

        // Define the database schema in the OnModelCreating method
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Define the structure of database tables for each entity
            modelBuilder.Entity<Users>();
            modelBuilder.Entity<Lakes>();
            modelBuilder.Entity<LakeSightings>();
            modelBuilder.Entity<Likes>();

        }
    }

}
