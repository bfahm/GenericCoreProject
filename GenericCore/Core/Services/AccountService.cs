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
using GenericCore.ViewModels;
using Microsoft.Extensions.Options;
using GenericCore.ViewModels.Requests.Account;
using AutoMapper;
using GenericCore.ViewModels.Responses.Account;
using GenericCore.Models.Exceptions;
using GenericCore.Constants;
using GenericCore.ViewModels.Wrappers;

namespace GenericCore.Services
{
    public class AccountService
    {
        private readonly AppSettings _appSettings;
        private readonly IMapper _mapper;
        private readonly UserManager<ApplicationUser> _userManager;

        public AccountService(IOptions<AppSettings> appSettings, IMapper mapper, UserManager<ApplicationUser> userManager)
        {
            _appSettings = appSettings.Value;
            _mapper = mapper;
            _userManager = userManager;
        }

        public async Task<string> LoginAsync(LoginRequest request)
        {
            ApplicationUser user = await _userManager.FindByEmailAsync(request.Email);
            if (user == null)
                throw new BusinessException(ErrorCodes.EmailNotFound);

            bool userHasValidPassowrd = await _userManager.CheckPasswordAsync(user, request.Password);

            if (!userHasValidPassowrd)
                throw new BusinessException(ErrorCodes.IncorrectEmailPasswordCombination);

            return GenerateAuthenticationResult(user);
        }

        public async Task<APIResponse> RegisterAsync(RegistrationRequest request)
        {
            ApplicationUser exsistingUserFoundByEmail = await _userManager.FindByEmailAsync(request.Email);
            if(exsistingUserFoundByEmail != null)
                throw new BusinessException(ErrorCodes.ExsitingEmail);

            ApplicationUser exsistingUserFoundByPhoneNumber = _userManager.Users.Where(u => u.PhoneNumber == request.PhoneNumber).FirstOrDefault();
            if (exsistingUserFoundByPhoneNumber != null)
                throw new BusinessException(ErrorCodes.ExsitingPhoneNumber);

            ApplicationUser newUser = new ApplicationUser
            {
                Email = request.Email,
                UserName = request.Email,
                FullName = request.FullName,
                PhoneNumber = request.PhoneNumber
            };

            var creationResult = await _userManager.CreateAsync(newUser, request.Password);

            if (!creationResult.Succeeded)
                return new FailedAPIResponse(ErrorCodes.IdentityError, creationResult);

            var response = _mapper.Map<RegistrationResponse>(newUser);
            response.Token = GenerateAuthenticationResult(newUser);

            return SuccessAPIResponseWrapper.Wrap(response);
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
