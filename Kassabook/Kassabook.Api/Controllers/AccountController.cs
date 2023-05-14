using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Kassabook.Api.Contracts.Request;
using Kassabook.Api.Contracts.Response;
using Kassabook.DL.Models;
using Kassabook.Dto;
using Kassabook.Service.Contracts.Account;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Kassabook.Api.Controllers
{
    [Route("api/[controller]")]
    public class AccountController : Controller
    {
        private readonly IAccountService _accountService;

        public AccountController(IAccountService accountService)
        {
            _accountService = accountService;
        }
        // GET: api/values
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var responseAccountBalanceList = new List<AccountBalanceResponse>();
            var accounts = await _accountService.GetAccounts();

            accounts.ForEach(a => responseAccountBalanceList.Add(new AccountBalanceResponse()
            {
                Name = a.Name,
                Balance = a.Balance
            }));


            return Ok(responseAccountBalanceList);
        }

        // GET api/values/5
        [HttpGet("{name}")]
        public async Task<IActionResult> Get(string name)
        {
            var account = await _accountService.GetAccountByName(name);

            if (account == null) return NotFound(new
            {
                statusCode = 404,
                message = "The account is not exists"
            });

            return Ok(new AccountBalanceResponse()
            {
                Name = account.Name,
                Balance = account.Balance
            });
        }

        // POST api/values
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] AccountRequest request)
        {
            var isTypeDefind = _accountService.IsAccountTypeDefind(request.Type);
            if (!isTypeDefind) return BadRequest(new
            {
                statusCode = 400,
                message = "The account type is not defind"
            });

            var result = await _accountService.CreateAccount(new AccountDto()
            {
                Name = request.Name,
                Type = (AccountTypeDto)Enum.Parse(typeof(AccountTypeDto), request.Type)

            });

            if (!result) return BadRequest();

            return Ok(result);
        }
    }
}

