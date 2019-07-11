using CJ.Data.NetCoreModels;

namespace CJ.Application.CoreDbContext.CoreUserApp
{
    public interface IUserAppService
    {
        CoreUser GetUser(CoreUser user);
    }
}
