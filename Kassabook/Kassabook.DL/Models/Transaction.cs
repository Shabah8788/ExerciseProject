using System;
namespace Kassabook.DL.Models
{
	public class Transaction
	{
        public Guid Id { get; set; }
        public Guid FromAccountId { get; set; }
        public Guid ToAccountId { get; set; }
        public decimal Amount { get; set; }
        public DateTime Date { get; set; }


        public virtual Account FromAccount { get; set; }
        public virtual Account ToAccount { get; set; }
    }
}

