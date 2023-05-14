using System;
using System.Transactions;
using Kassabook.BL.Contract.Account;
using Kassabook.BL.Contract.Transcation;
using Kassabook.DL.Models;
using Kassabook.Dto;
using Kassabook.Service.Contracts.Transaction;

namespace Kassabook.Service.Services.Transaction
{
    public class TransactionService : ITransactionService
    {
        private readonly ITranscationRepository _transcationRepository;
        private readonly IAccountRepository _accountRepository;

        public TransactionService(ITranscationRepository transcationRepository, IAccountRepository accountRepository)
        {
            _transcationRepository = transcationRepository;
            _accountRepository = accountRepository;
        }


        public async Task<List<TransactionDto>> GetTransactions()
        {
            var transactionDtoList = new List<TransactionDto>();
            var transactions = await _transcationRepository.GetTransactions();

            foreach (var transaction in transactions)
            {
                var fromAccount =await _accountRepository.GetById(transaction.FromAccountId);
                var toAccount =await _accountRepository.GetById(transaction.ToAccountId);

                if (fromAccount == null && toAccount == null) return transactionDtoList;

                transactionDtoList.Add(new TransactionDto()
                {
                    FromAccount = fromAccount.Name,
                    ToAccount = toAccount.Name,
                    Amount = transaction.Amount,
                    Date = transaction.Date.ToString("yy-MM-dd")
                });
            }

            return transactionDtoList;
        }

        public async Task<bool> CreateTransaction(TransactionDto transactionDto)
        {
            var fromAccount = await _accountRepository.Find(a => a.Name == transactionDto.FromAccount);
            var toAccount = await _accountRepository.Find(a => a.Name == transactionDto.ToAccount);

            if (fromAccount == null || toAccount == null) return false;

            if (fromAccount.Id == toAccount.Id) return false;

            using (TransactionScope scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
            {
                try
                {
                    if (fromAccount.Balance >= transactionDto.Amount)
                    {

                        fromAccount.Balance -= transactionDto.Amount;
                        toAccount.Balance += transactionDto.Amount;
                        await _accountRepository.Update(toAccount);

                        await _transcationRepository.Add(new DL.Models.Transaction()
                        {
                            FromAccountId = fromAccount.Id,
                            ToAccountId = toAccount.Id,
                            Amount = transactionDto.Amount,
                            Date = DateTime.Now
                        });


                        scope.Complete(); // Commit the transaction
                        return true;
                    }
                    else
                    {
                        throw new Exception("Insufficient balance");
                    }
                }
                catch (Exception ex)
                {
                    scope.Dispose();
                    return false;
                }
            }
        }

    }
}

