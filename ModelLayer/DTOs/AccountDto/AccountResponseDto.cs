using System;
using System.Collections.Generic;
using System.Text;

namespace ModelLayer.DTOs.AccountDto
{
    public class AccountResponseDto
    {
        public int AccountId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public DateTime DateOfBirth { get; set; }
        public long PhoneNumber { get; set; }

    }
}
