using System;
namespace Kassabook.DL.Models
{
    public class Account
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public AccountType Type { get; set; }
        public decimal Balance { get; set; }

        public virtual ICollection<Transaction> Transactions { get; set; }
    }
}

