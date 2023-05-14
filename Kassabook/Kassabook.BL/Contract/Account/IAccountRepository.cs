using System;
namespace Kassabook.BL.Contract.Account
{
    public interface IAccountRepository : IAsyncRepository<Kassabook.DL.Models.Account>
    {
        Task<List<DL.Models.Account>> GetAccountsWithBalances();
    }
}

