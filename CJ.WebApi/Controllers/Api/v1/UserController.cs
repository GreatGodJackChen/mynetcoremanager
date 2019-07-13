using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CJ.Core.Caching;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;

namespace CJ.WebApi.Controllers.Api.v1
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly ICacheManager _redis;
        public UserController(ICacheManager redis)
        {
            _redis = redis;
        }
        // GET: api/User
        [HttpGet]
        public IActionResult GetUser()
        {
            var id = HttpContext.Items["userId"].ToString();
            var user = _redis.Get<UserStore>(id);
            return Ok(user);
        }

        // GET: api/User/5
        [HttpGet("{id}", Name = "Get")]
        public string Get(int id)
        {
            return "value";
        }

        // POST: api/User
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT: api/User/5
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
