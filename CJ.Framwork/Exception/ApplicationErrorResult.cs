using System.Net;
using Microsoft.AspNetCore.Mvc;

namespace CJ.Framwork.Exception
{
    public class ApplicationErrorResult:ObjectResult
    {
        public ApplicationErrorResult(object value) : base(value)
        {
            StatusCode = (int)HttpStatusCode.InternalServerError;
        }
    }
}