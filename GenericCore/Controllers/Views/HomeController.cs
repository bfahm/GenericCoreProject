using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GenericCore.Controllers.Views
{
    [ApiExplorerSettings(IgnoreApi = true)]
    public class HomeController : Controller
    {
        [HttpGet("")]
        [HttpGet("[controller]")]
        [HttpGet("[controller]/[action]")]
        [HttpGet("index")]
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet("api/[controller]/index")]
        public IActionResult RedirectFromApi()
        {
            return RedirectToAction(nameof(HomeController.Index));
        }

        [HttpGet("[controller]/authenticated")]
        [Authorize]
        public IActionResult AuthenticatedEndpoint()
        {
            return View();
        }
    }
}
