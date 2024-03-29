﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GenericCore.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class DeveloperToolsController : ControllerBase
    {
        [Authorize]
        [HttpGet]
        public string AuthenticatedEndpoint()
        {
            return "Authenticated!";
        }
    }
}