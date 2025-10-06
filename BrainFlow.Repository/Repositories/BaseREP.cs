using BrainFlow.Repository.Context;
using BrainFlow.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace BrainFlow.Repository.Repositories
{
    public class BaseREP<T> : IBaseREP<T> where T : class
    {
        #region Context
        protected readonly BrainFlowContext _context;
        #endregion

        #region Constructor
        public BaseREP(BrainFlowContext context)
        {
            _context = context;
        }
        #endregion

        #region Methods

        #region Add
        public async Task<T> Add(T entity)
        {
            await _context.Set<T>().AddAsync(entity);
            await _context.SaveChangesAsync();
            return entity;
        }
        #endregion

        #region Upate
        public async Task<T> Update(T entity)
        {
            _context.Set<T>().Update(entity);
            await _context.SaveChangesAsync();
            return entity;
        }
        #endregion

        #region Delete
        public async Task<bool> Delete(int id)
        {
            var entity = await GetById(id);
            if (entity == null) return false;

            _context.Set<T>().Remove(entity);
            await _context.SaveChangesAsync();
            return true;
        }
        #endregion

        #region GetById
        public async Task<T> GetById(int id)
        {
            return await _context.Set<T>().FindAsync(id);
        }
        #endregion

        #region GetAll
        public async Task<List<T>> GetAll()
        {
            return await _context.Set<T>().ToListAsync();
        }
        #endregion

        #endregion
    }
}