using Kassabook.BL.Contract.Account;
using Kassabook.DL;
using Microsoft.EntityFrameworkCore;

namespace Kassabook.BL.Repositories.Account
{
    public class AccountRepository : AsyncRepository<DL.Models.Account>, IAccountRepository
    {
        public AccountRepository(KassabookDbContext context) : base(context)
        {
        }

        public async Task<List<DL.Models.Account>> GetAccountsWithBalances()
        {
            try
            {
                var accounts = await _context.Accounts
                             .OrderBy(a => a.Name)
                             .Select(a => new DL.Models.Account()
                             {
                                 Id = a.Id,
                                 Name = a.Name,
                                 Balance = a.Balance,
                                 Type = a.Type
                             })
                             .ToListAsync();

                return accounts;
            }
            catch (Exception ex)
            {
                return null;
            }

        }
    }
}

