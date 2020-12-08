using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ModelLayer.DTOs.AccountDto
{
    public class AccountRequestDto
    {
        [Required]
        [Column(TypeName = "varchar(50)")]
        public string FirstName { get; set; }

        [Required]
        [Column(TypeName = "varchar(50)")]
        public string LastName { get; set; }

        [Required]
        [MaxLength(100)]
        [RegularExpression(@"^[a-zA-Z0-9]+[\\.\\-\\+\\_]?[a-zA-Z0-9]+@[a-zA-Z0-9]+[.]?[a-zA-Z]{2,4}[\\.]?([a-z]{2,4})?$",
            ErrorMessage = "Please provide valid email id")]
        public string Email { get; set; }

        [Required]
        [RegularExpression(@"^(?=.*[0-9])(?=.*[A-Z])(?=[a-zA-Z0-9]*[^a-zA-Z0-9][a-zA-Z0-9]*$).{8,}"
        , ErrorMessage = "Password should contain atleast one Uppercase, lowercase, special and digit and minimum length should be 8")]
        public string Password { get; set; }

        [Required]
        [Column(TypeName = "date")]
        public DateTime DateOfBirth { get; set; }

        [Required]
        [Column(TypeName = "BigInt")]

        [RegularExpression("[0-9]{10}"
        , ErrorMessage = "Please provide valid phone number")]
        public long PhoneNumber { get; set; }

    }
}
