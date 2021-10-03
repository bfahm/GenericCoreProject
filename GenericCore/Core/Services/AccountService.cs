using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using GenericCore.Models;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using GenericCore.ViewModels.Account;
using GenericCore.ViewModels;
using Microsoft.Extensions.Options;

namespace GenericCore.Services
{
    public class AccountService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly AppSettings _appSettings;

        public AccountService(IOptions<AppSettings> appSettings, UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
            _appSettings = appSettings.Value;
        }

        public async Task<APIResponse<string>> LoginAsync(LoginRequestViewModel request)
        {
            APIResponse<string> response = new APIResponse<string>();

            ApplicationUser user = await _userManager.FindByEmailAsync(request.Email);
            if (user == null)
            {
                response.Messages.Add("User with provided email does not exsist.");
                return response;
            }


            bool userHasValidPassowrd = await _userManager.CheckPasswordAsync(user, request.Password);

            if (!userHasValidPassowrd)
            {
                response.Messages.Add("User/Password combination is wrong.");
                return response;
            }

            response.Result = GenerateAuthenticationResult(user);
            response.HasErrors = false;

            return response;
        }

        public async Task<APIResponse<string>> RegisterAsync(RegistrationRequestViewModel request)
        {
            APIResponse<string> response = new APIResponse<string>();

            ApplicationUser exsistingUserFoundByEmail = await _userManager.FindByEmailAsync(request.Email);
            if(exsistingUserFoundByEmail != null)
            {
                response.Messages.Add("User with this email address already exsists.");
                return response;
            }

            ApplicationUser exsistingUserFoundByPhoneNumber = _userManager.Users.Where(u => u.PhoneNumber == request.PhoneNumber).FirstOrDefault();
            if (exsistingUserFoundByPhoneNumber != null)
            {
                response.Messages.Add("User with this Phone Number was registered before.");
                return response;
            }

            ApplicationUser newUser = new ApplicationUser
            {
                Email = request.Email,
                UserName = request.Email,
                FullName = request.FullName,
                PhoneNumber = request.PhoneNumber
            };

            var creationResult = await _userManager.CreateAsync(newUser, request.Password);

            if (!creationResult.Succeeded)
            {
                response.Messages = creationResult.Errors.Select(e => e.Description).ToList();
                return response;
            }

            response.Result = GenerateAuthenticationResult(newUser);
            response.HasErrors = false;

            return response;
        }

        private string GenerateAuthenticationResult(ApplicationUser user)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_appSettings.JWTSettings.Secret);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                // Things to be included and encoded in the token
                Subject = new ClaimsIdentity(new[]
                {
                    new Claim(JwtRegisteredClaimNames.Sub, user.Email),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                    new Claim(JwtRegisteredClaimNames.Email, user.Email),
                    new Claim("id", user.Id)
                }),
                // Token will expire 2 hours from which it was created
                Expires = DateTime.UtcNow.AddHours(_appSettings.JWTSettings.ExpireAfterHours),
                // JWT Signing
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);

            return tokenHandler.WriteToken(token);
        }
    }
}
