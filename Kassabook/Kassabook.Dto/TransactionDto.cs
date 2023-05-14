using System;
namespace Kassabook.Dto
{
	public class TransactionDto
	{	
		public string FromAccount { get; set; }
		public string ToAccount { get; set; }
		public decimal Amount { get; set; }
		public string Date { get; set; }
	}
}

