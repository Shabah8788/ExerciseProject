using System;
using Kassabook.Dto;

namespace Kassabook.Service.Contracts.Account
{
	public interface IAccountService
	{
        bool IsAccountTypeDefind(string type);
        Task<bool> CreateAccount(AccountDto accountDto);
        Task<List<AccountDto>> GetAccounts();
        Task<AccountDto> GetAccountByName(string name);

    }
}

