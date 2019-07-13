using System.Collections.Generic;
using CJ.Application.CoreDbContext.CoreUserApp;
using CJ.Core.Caching;
using CJ.Core.Responser;
using CJ.Data.NetCoreModels;
using CJ.Framwork.Ftw.jwt;
using Microsoft.AspNetCore.Mvc;

namespace CJ.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IJwt _jwt;
        private readonly IUserAppService _userAppService;
        private readonly ICacheManager _redis;
        public AuthController(IJwt jwt,IUserAppService userAppService, ICacheManager redis)
        {
            this._jwt = jwt;
            _userAppService = userAppService;
            _redis = redis;
        }
        [HttpPost]
        public IActionResult GetToken(User postuser)
        {
            var response = ResponseModelFactory.CreateInstance;
            var userInfo = new CoreUser
            {
                LoginName = postuser.userName,
                Password = postuser.password
            };
            var user = _userAppService.GetUser(userInfo);
            if (user!=null)
            {
                Dictionary<string, string> clims = new Dictionary<string, string>();
                clims.Add("userId", user.Id.ToString());
                clims.Add("username", user.DisplayName);
                //写入缓存
                _redis.Set(user.Id, user);
                response.SetData(_jwt.GetToken(clims));
                return Ok(response);
            }
            response.SetError();
            return Ok(response);
        }
    }
    public class User
    {
        public string userName { get; set; }
        public string password { get; set; }
        public string mobile { get; set; }
        public string captcha { get; set; }
    }
}