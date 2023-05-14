using System;
namespace Kassabook.Dto
{
	public class AccountDto
	{
        public Guid Id { get; set; }
        public string Name { get; set; }
        public AccountTypeDto Type { get; set; }
        public decimal Balance { get; set; }
    }
}

