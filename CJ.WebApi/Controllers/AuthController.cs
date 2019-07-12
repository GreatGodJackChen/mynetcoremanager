﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CJ.Application.CoreDbContext.CoreUserApp;
using CJ.Core.Ftw.jwt;
using CJ.Core.Responser;
using CJ.Data.NetCoreModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CJ.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IJwt _jwt;
        private readonly IUserAppService _userAppService;
        public AuthController(IJwt jwt,IUserAppService userAppService)
        {
            this._jwt = jwt;
            _userAppService = userAppService;
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
                clims.Add("user", user.Id.ToString());
                clims.Add("username", user.DisplayName);
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