using System;
using Kassabook.DL.Models;
using Kassabook.DL.Seeders;
using Microsoft.EntityFrameworkCore;

namespace Kassabook.DL
{
    public class KassabookDbContext : DbContext
    {
        public DbSet<Account> Accounts { get; set; }
        public DbSet<Transaction> Transactions { get; set; }

        public KassabookDbContext(DbContextOptions options) : base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            AccountSeeder.Seed(modelBuilder);


            modelBuilder.Entity<Transaction>()
                          .HasOne(t => t.FromAccount)
                          .WithMany(a => a.Transactions)
                          .HasForeignKey(t => t.FromAccountId)
                          .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Transaction>()
                .HasOne(t => t.ToAccount)
                .WithMany()  // Use WithMany() instead of WithMany(a => a.Transactions)
                .HasForeignKey(t => t.ToAccountId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Account>()
                .HasMany(a => a.Transactions)
                .WithOne(t => t.FromAccount)
                .HasForeignKey(t => t.FromAccountId)
                .OnDelete(DeleteBehavior.Restrict);

            // Ignore the ToAccount property for the Account-Transaction relationship
            modelBuilder.Entity<Transaction>()
                .Ignore(t => t.ToAccount);
        }

    }
}

