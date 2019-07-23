using CJ.Data.BaseEntity;

namespace CJ.Repositories.BaseRepositories
{
    public interface IRepository<TEntity> : IRepository<TEntity, string> where TEntity : class, IEntity<string>
    {

    }
}
