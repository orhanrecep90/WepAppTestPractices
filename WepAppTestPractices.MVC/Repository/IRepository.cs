using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WepAppTestPractices.MVC.Repository
{
   public interface IRepository<TEntity> where TEntity:class
    {
        Task<IEnumerable<TEntity>> GetAllAsync();
        Task<TEntity> GetByIdAsync(int Id);
        Task CreateAsync(TEntity entity);
        Task UpdateAsync(TEntity entity);
        Task DeleteAsync(TEntity entity);
    }
}
