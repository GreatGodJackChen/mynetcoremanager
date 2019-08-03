using IdentityServer4.Extensions;
using IdentityServer4.Models;
using IdentityServer4.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace IdentityServer
{
    public class UserProfileService : IProfileService
    {
        /// <summary>
        /// 只要有关用户的身份信息单元被请求（例如在令牌创建期间或通过用户信息终点），就会调用此方法
        /// </summary>
        /// <param name="context">The context.</param>
        /// <returns></returns>
        public Task GetProfileDataAsync(ProfileDataRequestContext context)
        {
            //判断是否有请求Claim信息
            if (context.RequestedClaimTypes.Any())
            {
                var user = GetUserById(context.Subject.GetSubjectId());
                if (user != null)
                {
                    //调用此方法以后内部会进行过滤，只将用户请求的Claim加入到 context.IssuedClaims 集合中 这样我们的请求方便能正常获取到所需Claim
                    var claims = new[]
                    {
                        new Claim("Username", user.Username),  //请求用户的账号，这个可以保证User.Identity.Name有值
                        new Claim("DisplayName", user.DisplayName),  //请求用户的姓名
                    };
                    //返回apiresource中定义的claims   
                    context.AddRequestedClaims(claims);
                }
            }

            return Task.CompletedTask;

        }
        /// <summary>
        /// 验证用户是否有效 例如：token创建或者验证
        /// </summary>
        /// <param name="context">The context.</param>
        /// <returns></returns>

        public Task IsActiveAsync(IsActiveContext context)
        {
            var user = GetUserById(context.Subject.GetName());
            //context.IsActive = user?. == 0;
            return Task.CompletedTask;
        }

        private CJ.Models.LoginInputModel GetUserById(string id)
        {
            CJ.Models.LoginInputModel user=new CJ.Models.LoginInputModel();

            //user = UserManager.Get(id);
            return user;
        }
    }
}
