using System.Collections.Generic;
using System.Threading.Tasks;
using Kassabook.Api.Contracts.Request;
using Kassabook.Api.Contracts.Response;
using Kassabook.Api.Controllers;
using Kassabook.Dto;
using Kassabook.Service.Contracts.Account;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestPlatform.CommunicationUtilities;
using Moq;
using NUnit.Framework;

namespace Test
{
    [TestFixture]
    public class AccountControllerTests
	{
        private AccountController _controller;
        private Mock<IAccountService> _accountServiceMock;

        [SetUp]
        public void Setup()
        {
            _accountServiceMock = new Mock<IAccountService>();
            _controller = new AccountController(_accountServiceMock.Object);
        }


        [Test]
        public async Task Get_ReturnsOkWithListOfAccountBalanceResponse()
        {
            // Arrange
            var accounts = new List<AccountDto>()
                            {
                                new AccountDto { Name = "Account1",Type=AccountTypeDto.Income, Balance = 100 },
                                new AccountDto { Name = "Account2",Type=AccountTypeDto.Expense, Balance = 200 },
                                new AccountDto { Name = "Account3",Type=AccountTypeDto.Checking, Balance = 300 }
                            };
            _accountServiceMock.Setup(x => x.GetAccounts()).ReturnsAsync(accounts);

            // Act
            var result = await _controller.Get();

            // Assert
            Assert.IsInstanceOf<OkObjectResult>(result);
            var okResult = (OkObjectResult)result;
            Assert.IsInstanceOf<List<AccountBalanceResponse>>(okResult.Value);
            var responseList = (List<AccountBalanceResponse>)okResult.Value;
            Assert.AreEqual(accounts.Count, responseList.Count);
            for (int i = 0; i < accounts.Count; i++)
            {
                Assert.AreEqual(accounts[i].Name, responseList[i].Name);
                Assert.AreEqual(accounts[i].Balance, responseList[i].Balance);
            }
        }


        [Test]
        public async Task Get_WithValidName_ReturnsOkWithAccountBalanceResponse()
        {
            // Arrange
            var account = new AccountDto { Name = "Account1", Balance = 100 };
            _accountServiceMock.Setup(x => x.GetAccountByName(account.Name)).ReturnsAsync(account);

            // Act
            var result = await _controller.Get(account.Name);

            // Assert
            Assert.IsInstanceOf<OkObjectResult>(result);
            var okResult = (OkObjectResult)result;
            Assert.IsInstanceOf<AccountBalanceResponse>(okResult.Value);
            var response = (AccountBalanceResponse)okResult.Value;
            Assert.AreEqual(account.Name, response.Name);
            Assert.AreEqual(account.Balance, response.Balance);
        }

        [Test]
        public async Task Get_WithInvalidName_ReturnsNotFound()
        {
            // Arrange
            _accountServiceMock.Setup(x => x.GetAccountByName(It.IsAny<string>())).ReturnsAsync((AccountDto)null);

            // Act
            var result = await _controller.Get("InvalidName");

            // Assert
            Assert.IsInstanceOf<NotFoundObjectResult>(result);
        }

        [Test]
        public async Task Post_WithValidRequest_ReturnsOk()
        {
            // Arrange
            var request = new AccountRequest { Name = "Account4", Type = "Expense" };
            _accountServiceMock.Setup(x => x.IsAccountTypeDefind(request.Type)).Returns(true);
            _accountServiceMock.Setup(x => x.CreateAccount(It.IsAny<AccountDto>())).ReturnsAsync(true);
            // Act
            var result = await _controller.Post(request);

            // Assert
            Assert.IsInstanceOf<OkObjectResult>(result);
            var okResult = (OkObjectResult)result;
            Assert.AreEqual(true, okResult.Value);
        }



        [Test]
        public async Task Post_WithInvalidRequest_ReturnsBadRequest()
        {
            // Arrange
            var request = new AccountRequest { Name = "Account1", Type = "InvalidType" };
            _accountServiceMock.Setup(x => x.IsAccountTypeDefind(request.Type)).Returns(false);

            // Act
            var result = await _controller.Post(request);

            // Assert
            Assert.IsInstanceOf<BadRequestObjectResult>(result);
        }
    }
}

