using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using CJ.Application.CoreDbContext.CorePermissionApp;
using CJ.Core.Responser;
using CJ.Data.NetCoreModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Newtonsoft.Json;

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
        public IActionResult GetList(int currentPage, int pageSize, string name = "")
        {
            var response = ResponseModelFactory.CreateInstance;
            //var request = Request.Query["paramsInfo"];
            //var dic = JsonConvert.DeserializeObject<Dictionary<string, object>>(request);
            Expression<Func<CorePermission, bool>> where = f => 1 == 1;
            //int currentPage = 0; int pageSize = 0;
            //if (dic!=null)
            //{
            //    if (dic.Keys.Contains("currentPage"))
            //    {
            //        currentPage = Convert.ToInt32(dic["currentPage"]);
            //    }
            //    if (dic.Keys.Contains("pageSize"))
            //    {
            //        pageSize = Convert.ToInt32(dic["pageSize"]);
            //    }
            //    if (dic.Keys.Contains("name"))
            //    {
            //        where = f => f.Name.Contains(dic["name"].ToString());
            //    }
            //}
            if (!string.IsNullOrEmpty(name))
            {
                where = f => f.Name.Contains(name);
            }
            var list = _permissionAppService.GetList(currentPage, pageSize, where);
            response.SetData(list);
            response.SetPagination(list.Pagination);
            return Ok(response);
        }

        // GET: api/Permission/5
        //[HttpGet]
        //public string Get(int id)
        //{
        //    return "value";
        //}

        // POST: api/Permission
        //[FromBody]
        //string permission
        [HttpPost]
        public IActionResult Add(int currentPage, int pageSize)
        {
            var response = ResponseModelFactory.CreateInstance;
            var result = response.GetRequestAsync(Request.Body).Result;
            var dic = JsonConvert.DeserializeObject<Dictionary<string, string>>(result);
            var request = _permissionAppService.AddPermission(dic);
            Expression<Func<CorePermission, bool>> where = f => 1 == 1;
            var list = _permissionAppService.GetList(currentPage, pageSize, where);
            response.SetData(list);
            response.SetPagination(list.Pagination);

            if (string.IsNullOrEmpty(result))
            {
                response.SetError("新建失败");
            }
            return Ok(response);
            //var dic = JsonConvert.DeserializeObject<Dictionary<string, object>>(permission);
            // var request=_permissionAppService.AddPermission(permission);
        }

        // PUT: api/Permission/5
        [HttpPost("updateRule")]
        public IActionResult Update(int currentPage, int pageSize)
        {
            var response = ResponseModelFactory.CreateInstance;
            var result = response.GetRequestAsync(Request.Body).Result;
            var dic = JsonConvert.DeserializeObject<Dictionary<string, string>>(result);
           // var request = _permissionAppService.AddPermission(dic);
            Expression<Func<CorePermission, bool>> where = f => 1 == 1;
            var list = _permissionAppService.GetList(currentPage, pageSize, where);
            response.SetData(list);
            response.SetPagination(list.Pagination);

            if (string.IsNullOrEmpty(result))
            {
                response.SetError("新建失败");
            }
            return Ok(response);
            //var dic = JsonConvert.DeserializeObject<Dictionary<string, object>>(permission);
            // var request=_permissionAppService.AddPermission(permission);
        }
        //[HttpPost("updateRule")]
        //public IActionResult Update(object pr)
        //{
        //    return Ok();
        //}
        //[HttpPost("updateRule")]
        //public IActionResult Update()
        //{
        //    return Ok();
        //}

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
