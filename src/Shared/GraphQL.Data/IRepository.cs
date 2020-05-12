using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraphQL.Data
{
    public interface IRepository<TEntity> where TEntity : class
    {
        Task<IList<TEntity>> FetchAll();
        Task<TEntity> Fetch(string id);
        Task<TEntity> Add(TEntity entity);
        Task<TEntity> Update(TEntity entity);
        Task<bool> Delete(string id);
    }
}
