using System;
namespace Kassabook.Api.Contracts.Request
{
	public class TransactionRequest
	{
		public string FromAccount { get; set; }
        public string ToAccount { get; set; }
		public decimal Amount { get; set; }
	}
}

