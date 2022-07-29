using GenericCore.Models;
using GenericCore.ViewModels.Responses.Account;

namespace GenericCore.Core.Mappers
{
    public class AccountsProfile : AutoMapper.Profile
    {
        public AccountsProfile()
        {
            CreateMap<ApplicationUser, RegistrationResponse>();
        }
    }
}
