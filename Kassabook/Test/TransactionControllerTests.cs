using System;
using Kassabook.Api.Contracts.Request;
using Kassabook.Api.Controllers;
using Kassabook.Dto;
using Kassabook.Service.Contracts.Transaction;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace Test
{
    [TestFixture]
    public class TransactionControllerTests
    {
        private TransactionController _controller;
        private Mock<ITransactionService> _transactionServiceMock;

        [SetUp]
        public void SetUp()
        {
            _transactionServiceMock = new Mock<ITransactionService>();
            _controller = new TransactionController(_transactionServiceMock.Object);
        }

        [Test]
        public async Task Get_ReturnsOkResultWithTransactions()
        {
            // Arrange
            var transactions = new List<TransactionDto>()
                                    {
                                        new TransactionDto() { FromAccount = "A", ToAccount = "B", Amount = 10 },
                                        new TransactionDto() { FromAccount = "B", ToAccount = "C", Amount = 20 },
                                        new TransactionDto() { FromAccount = "C", ToAccount = "D", Amount = 30 }
                                    };

            _transactionServiceMock.Setup(s => s.GetTransactions()).ReturnsAsync(transactions);

            // Act
            var result = await _controller.Get();

            // Assert
            var okResult = result as OkObjectResult;
            Assert.IsNotNull(okResult);

            var transactionList = okResult.Value as List<TransactionDto>;
            Assert.IsNotNull(transactionList);
            Assert.AreEqual(3, transactionList.Count);
        }

        [Test]
        public async Task Post_ReturnsOkResultWithTrue()
        {
            // Arrange
            var request = new TransactionRequest() { FromAccount = "A", ToAccount = "B", Amount = 10 };

            _transactionServiceMock.Setup(s => s.CreateTransaction(It.IsAny<TransactionDto>())).ReturnsAsync(true);

            // Act
            var result = await _controller.Post(request);

            // Assert
            var okResult = result as OkObjectResult;
            Assert.IsNotNull(okResult);

            var success = okResult.Value as bool?;
            Assert.IsNotNull(success);
            Assert.IsTrue(success.Value);
        }

    }
}

