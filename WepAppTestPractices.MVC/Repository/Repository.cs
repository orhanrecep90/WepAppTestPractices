using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WepAppTestPractices.MVC.Models;

namespace WepAppTestPractices.MVC.Repository
{
    public class Repository<TEntity> : IRepository<TEntity> where TEntity : class
    {
        private readonly WepAppTestPracticesDBContext _context;
        //private readonly DbSet<TEntity> _dbSet;


        public Repository(WepAppTestPracticesDBContext context)
        {
            _context = context;
            //_dbSet = _context.Set<TEntity>();
        }
        public async Task CreateAsync(TEntity entity)
        {
            //await _dbSet.AddAsync(entity);
            await _context.AddAsync(entity);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(TEntity entity)
        {
            _context.Entry(entity).State = EntityState.Deleted;
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<TEntity>> GetAllAsync()
        {
            return await _context.Set<TEntity>().ToListAsync();
        }

        public async Task<TEntity> GetByIdAsync(int id)
        {
            return await _context.Set<TEntity>().FindAsync(id);
        }

        public async Task UpdateAsync(TEntity entity)
        {
             _context.Entry(entity).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }
    }
}
