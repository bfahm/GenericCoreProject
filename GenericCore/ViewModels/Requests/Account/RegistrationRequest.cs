using System.ComponentModel.DataAnnotations;

namespace GenericCore.ViewModels.Requests.Account
{
    public class RegistrationRequest
    {
        [Required]
        [Display(Name = "Full Name")]
        public string FullName { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [RegularExpression(@"^[0-9]{11}$", ErrorMessage = "Phone number is invalid")]
        [Required]
        public string PhoneNumber { get; set; }
    }
}
