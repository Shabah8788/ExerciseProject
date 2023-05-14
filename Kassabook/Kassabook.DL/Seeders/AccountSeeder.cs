using System;
using Kassabook.DL.Models;
using Microsoft.EntityFrameworkCore;

namespace Kassabook.DL.Seeders
{
	public class AccountSeeder
	{
        public static void Seed(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Account>().HasData(
            new Account { Id = Guid.Parse("0e0e6b0e-1428-4129-8e22-357395e45474"), Name = "Livsmedel",Balance=2000 ,Type = AccountType.Expense },
            new Account { Id = Guid.Parse("122f4071-1d4e-4df2-9b8b-2fea2579d66e"), Name = "Bankkonto", Balance = 1500, Type = AccountType.Checking },
            new Account { Id = Guid.Parse("8f6e4f60-c198-42f4-a96a-ef7f2679e3ed"), Name = "Hyra", Balance = 1300, Type = AccountType.Expense },
            new Account { Id = Guid.Parse("4b8ee890-1b36-470b-bd61-6edaa7588158"), Name = "Lön", Balance = 3000, Type = AccountType.Income }
        );
        }

    }
}

