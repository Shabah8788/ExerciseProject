using System;
using Kassabook.BL.Contract.Transcation;
using Kassabook.DL;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;

namespace Kassabook.BL.Repositories.Transcation
{
    public class TranscationRepository : AsyncRepository<Kassabook.DL.Models.Transaction>, ITranscationRepository
    {
        public TranscationRepository(KassabookDbContext context) : base(context)
        {
        }


        public async Task<List<DL.Models.Transaction>> GetTransactions()
        {
            var transactions = await _context.Transactions
                                 .ToListAsync();
            return transactions;
        }
    }
}

