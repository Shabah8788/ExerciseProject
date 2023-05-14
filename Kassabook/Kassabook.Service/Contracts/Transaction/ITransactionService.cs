using System;
using Kassabook.Dto;

namespace Kassabook.Service.Contracts.Transaction
{
	public interface ITransactionService
	{
        Task<List<TransactionDto>> GetTransactions();
        Task<bool> CreateTransaction(TransactionDto transactionDto);

    }
}

