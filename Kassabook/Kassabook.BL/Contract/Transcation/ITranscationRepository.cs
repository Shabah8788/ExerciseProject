using System;
namespace Kassabook.BL.Contract.Transcation
{
	public interface ITranscationRepository: IAsyncRepository<Kassabook.DL.Models.Transaction>
    {
        Task<List<DL.Models.Transaction>> GetTransactions();

    }
}

