using System.ComponentModel.DataAnnotations;

namespace GenericCore.ViewModels.Requests.Account
{
    public class LoginRequest
    {
        [EmailAddress]
        public string Email { get; set; }
        public string Password { get; set; }
    }
}
