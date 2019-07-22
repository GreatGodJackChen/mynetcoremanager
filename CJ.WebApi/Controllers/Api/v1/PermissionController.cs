using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using CJ.Application.CoreDbContext.CorePermissionApp;
using CJ.Core.ExpressionHelper;
using CJ.Core.Responser;
using CJ.Data.NetCoreModels;
using Microsoft.AspNetCore.Mvc;
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
            var predicate = ExpressionBuilder.True<CorePermission>();
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
                predicate = predicate.And(f => f.Name.Contains(name));
            }
            Expression<Func<CorePermission, DateTime?>> orderby = f => f.CreatedTime;
            var list = _permissionAppService.GetList(currentPage, pageSize, predicate, orderby);
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
            var predicate = ExpressionBuilder.True<CorePermission>();
            Expression<Func<CorePermission, DateTime?>> orderby = f=>f.CreatedTime;
            var list = _permissionAppService.GetList(currentPage, pageSize, predicate, orderby);
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
        [HttpPost("updatePermission")]
        public IActionResult Update(int currentPage, int pageSize)
        {
            var response = ResponseModelFactory.CreateInstance;
            var result = response.GetRequestAsync(Request.Body).Result;
            var dic = JsonConvert.DeserializeObject<Dictionary<string, string>>(result);
            CorePermission permission = new CorePermission()
            {
                Name = dic["Name"],
                MenuId = dic["MenuId"],
                ActionCode = dic["ActionCode"],
                Id = dic["Id"],
            };
            Expression<Func<CorePermission, DateTime?>> orderby = f => f.CreatedTime;
            var predicate = ExpressionBuilder.True<CorePermission>();
            var list = _permissionAppService.Update(currentPage,pageSize, predicate, permission, orderby);
            //var list = _permissionAppService.GetList(currentPage, pageSize, where);
            response.SetData(list);
            response.SetPagination(list.Pagination);

            if (string.IsNullOrEmpty(result))
            {
                response.SetError("更新失败");
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
        [HttpDelete("deletePermission")]
        public IActionResult Delete(int currentPage, int pageSize)
        {
            var response = ResponseModelFactory.CreateInstance;
            var result = response.GetRequestAsync(Request.Body).Result;
            var dic = JsonConvert.DeserializeObject<Dictionary<string, string[]>>(result);
            string[] strId = dic!=null?dic["key"]:new string[] { };
            Expression<Func<CorePermission, DateTime?>> orderby = f => f.CreatedTime;
            var list=  _permissionAppService.Delete(currentPage, pageSize, strId, orderby);
            response.SetData(list);
            response.SetPagination(list.Pagination);
            return Ok(response);
        }
    }
}
