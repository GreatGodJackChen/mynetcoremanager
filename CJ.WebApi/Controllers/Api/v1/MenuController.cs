using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CJ.Application.CoreDbContext.CoreMenuApp;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CJ.WebApi.Controllers.Api.v1
{
    [Route("api/[controller]")]
    [ApiController]
    public class MenuController : ControllerBase
    {
        private readonly IMenuApp _menuApp;
        public MenuController(IMenuApp menuApp)
        {
            _menuApp = menuApp;
        }
        [HttpGet]
        public IActionResult Menu()
        {
            var menus = _menuApp.GetMenus();
            return Ok(menus);
        }
    }
}