using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CJ.Application.CoreDbContext.CoreUserApp;
using CJ.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CJ.WebApi.Controllers.Api.v2
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IUserAppService _userApp;
        public AccountController(IUserAppService userApp)
        {
            _userApp = userApp;
        }
        [HttpPost]
        public IActionResult GetUser(LoginInputModel model)
        {
            return Ok(_userApp.GetUser(model.Username)) ;
        }
    }
}