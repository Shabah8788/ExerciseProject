using Kassabook.Api.Contracts.Request;
using Kassabook.Service.Contracts.Transaction;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Kassabook.Api.Controllers
{
    [Route("api/[controller]")]
    public class TransactionController : Controller
    {
        private readonly ITransactionService _transactionService;

        public TransactionController(ITransactionService transactionService)
        {
            _transactionService = transactionService;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var transactions = await _transactionService.GetTransactions();
            return Ok(transactions);
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] TransactionRequest request)
        {
            var result = await _transactionService.CreateTransaction(new Dto.TransactionDto()
            {
                FromAccount = request.FromAccount,
                ToAccount = request.ToAccount,
                Amount = request.Amount
            });

            return Ok(result);
        }

    }
}

