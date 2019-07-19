using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CJ.Application.CoreDbContext.CorePermissionApp;
using CJ.Data.NetCoreModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CJ.WebApi.Controllers.Api.v1
{
    [Route("api/[controller]")]
    [ApiController]
    public class PermissionController : ControllerBase
    {
        private readonly IPermissionAppService _permissionAppService;
        public PermissionController(IPermissionAppService permissionAppService)
        {
            _permissionAppService = permissionAppService;
        }
        // GET: api/Permission
        [HttpGet]
        public IActionResult GetList()
        {
            var tt = HttpContext;
            var  result=  _permissionAppService.GetList();
            return Ok(result);
        }

        // GET: api/Permission/5
        //[HttpGet]
        //public string Get(int id)
        //{
        //    return "value";
        //}

        // POST: api/Permission
        [HttpPost]
        public void Add(CorePermission permission)
        {
            var request=_permissionAppService.AddPermission(permission);
        }

        // PUT: api/Permission/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
