using CJ.Data.NetCoreModels;
using CJ.Models;

namespace CJ.Application.CoreDbContext.CoreUserApp
{
    public interface IUserAppService
    {
        LoginInputModel GetUser(string loginName);
    }
}
