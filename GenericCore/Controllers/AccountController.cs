using GenericCore.Services;
using GenericCore.ViewModels.Requests.Account;
using GenericCore.ViewModels.Wrappers;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace GenericCore.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly AccountService _accountService;

        public AccountController(AccountService accountService)
        {
            _accountService = accountService;
        }

        [HttpPost]
        public async Task<IActionResult> Register([FromBody] RegistrationRequest request)
        {
            var authResponse = await _accountService.RegisterAsync(request);

            if (!authResponse.Status)
                return BadRequest(authResponse);

            return Ok(authResponse);
        }

        [HttpPost]
        public async Task<IActionResult> Login([FromBody] LoginRequest request)
        {
            var authResponse = await _accountService.LoginAsync(request);
            return Ok(SuccessAPIResponseWrapper.Wrap(authResponse));
        }
    }
}