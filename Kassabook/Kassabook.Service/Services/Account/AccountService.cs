using System;
using System.Drawing;
using Kassabook.BL.Contract.Account;
using Kassabook.Dto;
using Kassabook.Service.Contracts.Account;
using System.Linq;
using Kassabook.DL.Models;

namespace Kassabook.Service.Services.Account
{
    public class AccountService : IAccountService
    {
        private readonly IAccountRepository _accountRepository;

        public AccountService(IAccountRepository accountRepository)
        {
            _accountRepository = accountRepository;
        }

        public bool IsAccountTypeDefind(string type)
        {
            bool isDefined = Enum.IsDefined(typeof(AccountTypeDto), type);
            return isDefined;
        }

        public async Task<List<AccountDto>> GetAccounts()
        {
            var accountDtoList = new List<AccountDto>();
            var accounts = await _accountRepository.GetAccountsWithBalances();

            if (accounts.Count() == 0) return new List<AccountDto>();

            accounts.ToList().ForEach(a => accountDtoList.Add(new AccountDto()
            {
                Id = a.Id,
                Name = a.Name.Length > 20 ? a.Name.Substring(0, 20) : a.Name,
                Balance = a.Balance,
                Type = (AccountTypeDto)a.Type
            }));

            return accountDtoList;
        }
        public async Task<AccountDto> GetAccountByName(string name)
        {

            var account = await _accountRepository.Find(a => a.Name == name);

            if (account == null) return null;


            return new AccountDto()
            {
                Id = account.Id,
                Name = account.Name,
                Balance = account.Balance,
                Type = (AccountTypeDto)account.Type
            };
        }
        public async Task<bool> CreateAccount(AccountDto accountDto)
        {
            var account = await _accountRepository.Find(a => a.Name == accountDto.Name);
            if (account == null)
            {
                await _accountRepository.Add(new DL.Models.Account()
                {
                    Id = Guid.NewGuid(),
                    Name = accountDto.Name,
                    Balance = accountDto.Balance,
                    Type = (DL.Models.AccountType)accountDto.Type

                });

                return true;
            }

            return false;
        }
    }
}

