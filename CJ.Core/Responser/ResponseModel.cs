﻿
using System.IO;
using System.Text;

namespace CJ.Core.Responser
{
    public class ResponseModel
    {
        /// <summary>
        /// 请求响应实体类
        /// </summary>
        public ResponseModel()
        {
            Code = 200;
            Message = "操作成功";
            status = "ok";
        }
        public string status { get; set; }
        /// <summary>
        /// 响应代码
        /// </summary>
        public int Code { get; set; }
        /// <summary>
        /// 响应消息内容
        /// </summary>
        public string Message { get; set; }
        /// <summary>
        /// 返回的响应数据
        /// </summary>
        public object Data { get; set; }

        public object Pagination { get; set; }

        /// <summary>
        /// 设置响应状态为成功
        /// </summary>
        /// <param name="message"></param>
        public void SetSuccess(string message = "成功")
        {
            Code = 200;
            Message = message;
            status = "ok";
        }
        /// <summary>
        /// 设置响应状态为失败
        /// </summary>
        /// <param name="message"></param>
        public void SetFailed(string message = "失败")
        {
            Message = message;
            Code = 999;
            status = "error";
        }

        /// <summary>
        /// 设置响应状态为体验版(返回失败结果)
        /// </summary>
        /// <param name="message"></param>
        public void SetIsTrial(string message = "体验版,功能已被关闭")
        {
            Message = message;
            Code = 999;
            status = "error";
        }

        /// <summary>
        /// 设置响应状态为错误
        /// </summary>
        /// <param name="message"></param>
        public void SetError(string message = "错误")
        {
            Code = 500;
            Message = message;
            status = "error";
        }

        /// <summary>
        /// 设置响应状态为未知资源
        /// </summary>
        /// <param name="message"></param>
        public void SetNotFound(string message = "未知资源")
        {
            Message = message;
            Code = 404;
            status = "error";
        }

        /// <summary>
        /// 设置响应状态为无权限
        /// </summary>
        /// <param name="message"></param>
        public void SetNoPermission(string message = "无权限")
        {
            Message = message;
            Code = 403;
            status = "error";
        }

        /// <summary>
        /// 设置响应数据
        /// </summary>
        /// <param name="data"></param>
        public void SetData(object data)
        {
            Data = data;
        }
        public void SetPagination(object pagination)
        {
            Pagination = pagination;
        }
        public async System.Threading.Tasks.Task<string> GetRequestAsync(Stream stream)
        {
            var result = string.Empty;
            using (var reader = new StreamReader(stream, Encoding.UTF8))
            {
                result = await reader.ReadToEndAsync();
            }
            return result;
        }
    }
}
