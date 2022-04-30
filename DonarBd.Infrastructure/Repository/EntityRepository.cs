using DonarBd.Core;
using DonarBd.Core.Domain.Auth;
using DonarBd.Infrastructure.Contexts;
using DonarBd.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace DonarBd.Infrastructure.Repository
{
    public class EntityRepository<T> : IEntityRepository<T> where T : BaseEntity
    {
        #region ctor
        protected readonly DonarBDContext _context;
        protected readonly IWorkContext _workContext;
        public EntityRepository(IWorkContext workContext, DonarBDContext context)
        {
            _workContext = workContext;
            _context = context;
        }
        #endregion

        #region methods
        public async Task<T> AddAsync(T entity, CancellationToken cancellationToken = default)
        {
            entity = await GetAddAsyncProperties(entity);
            await _context.Set<T>().AddAsync(entity);
            await _context.SaveChangesAsync(cancellationToken);

            return entity;
        }

        public async Task AddRangeAsync(IEnumerable<T> entities, CancellationToken cancellationToken = default)
        {
            User user = await _workContext.GetCurrentUserAsync();
            List<T> EntityList = new List<T>();
            foreach (var item in entities)
            {
                item.CreatedBy = "testuser";
                item.CreatedOn = DateTime.Now;
                EntityList.Add(item);
            }
            await _context.Set<T>().AddRangeAsync(EntityList);
        }

        public IQueryable<T> AllAsIQueryable()
        {
            return _context.Set<T>().Where(s => s.IsSoftDeleted == false).AsQueryable();
        }

        public IDbContextTransaction BeginTransaction()
        {
            return _context.Database.BeginTransaction();
        }

        public async Task<IDbContextTransaction> BeginTransactionAsync()
        {
            return await _context.Database.BeginTransactionAsync();
        }

        public int Count()
        {
            return _context.Set<T>().Where(s => s.IsSoftDeleted == false).Count();
        }

        public async Task<int> CountAsync(CancellationToken cancellationToken = default)
        {
            return await _context.Set<T>().Where(s => s.IsSoftDeleted == false).CountAsync(cancellationToken);
        }

        public int CountDeleted()
        {
            return _context.Set<T>().Where(s => s.IsSoftDeleted).Count();
        }

        public async Task<int> CountDeletedAsync(CancellationToken cancellationToken = default)
        {
            return await _context.Set<T>().Where(s => s.IsSoftDeleted).CountAsync();
        }

        public async Task<bool> DeleteAsync(T entity, CancellationToken cancellationToken = default)
        {
            entity = await GetSoftDeleteAsyncProperties(entity);

            _context.Entry(entity).State = EntityState.Modified;
            await _context.SaveChangesAsync(cancellationToken);
            return await Task.FromResult(true);
        }

        public async Task<bool> DeleteByIdAsync(long id, CancellationToken cancellationToken = default)
        {
            var entity = await _context.Set<T>().FindAsync(id);
            entity = await GetSoftDeleteAsyncProperties(entity);
            _context.Entry(entity).State = EntityState.Modified;
            await _context.SaveChangesAsync(cancellationToken);
            return await Task.FromResult(true);
        }

        public async Task<T> FirstAsync(CancellationToken cancellationToken = default)
        {
            return await _context.Set<T>().Where(s => s.IsSoftDeleted == false).FirstAsync(cancellationToken);
        }

        public async Task<T> FirstOrDefaultAsync(CancellationToken cancellationToken = default)
        {
            return await _context.Set<T>().Where(s => s.IsSoftDeleted == false).FirstOrDefaultAsync(cancellationToken);
        }

        public async Task<IReadOnlyList<T>> GetAllAsync(CancellationToken cancellationToken = default)
        {
            return await _context.Set<T>().Where(s => s.IsSoftDeleted == false).ToListAsync(cancellationToken);
        }

        public async Task<IReadOnlyList<T>> GetAllDeletedAsync(CancellationToken cancellationToken = default)
        {
            return await _context.Set<T>().Where(s => s.IsSoftDeleted).ToListAsync(cancellationToken);
        }

        public async Task<IReadOnlyList<T>> GetAllPagedAsync(int recSkip, int recTake, CancellationToken cancellationToken = default)
        {
            return await _context.Set<T>().Where(s => s.IsSoftDeleted == false).Skip(recSkip).Take(recTake).ToListAsync(cancellationToken);
        }

        public async Task<T> GetByIdAsync(long id, CancellationToken cancellationToken = default)
        {
            var findData = await _context.Set<T>().FindAsync(new object[] { id }, cancellationToken);
            return findData;
        }

        public async Task<IReadOnlyList<T>> GetDeletedPagedAsync(int recSkip, int recTake, CancellationToken cancellationToken = default)
        {
            return await _context.Set<T>().Where(s => s.IsSoftDeleted).Skip(recSkip).Take(recTake).ToListAsync(cancellationToken);
        }

        public async Task<bool> PermanentDeleteAsync(T entity, CancellationToken cancellationToken = default)
        {
            _context.Set<T>().Remove(entity);
            await _context.SaveChangesAsync(cancellationToken);
            return await Task.FromResult(true);
        }

        public async Task<bool> PermanentDeleteByIdAsync(long id, CancellationToken cancellationToken = default)
        {
            _context.Set<T>().Remove(_context.Set<T>().Find(id));
            await _context.SaveChangesAsync(cancellationToken);
            return await Task.FromResult(true);
        }

        public async Task<bool> RestoreAsync(T entity, CancellationToken cancellationToken = default)
        {
            entity = await GetRestoreAsyncProperties(entity);
            _context.Entry(entity).State = EntityState.Modified;
            await _context.SaveChangesAsync(cancellationToken);
            return await Task.FromResult(true);
        }

        public async Task<bool> RestoreByIdAsync(long id, CancellationToken cancellationToken = default)
        {
            var entity = await _context.Set<T>().FindAsync(id);
            entity = await GetRestoreAsyncProperties(entity);
            _context.Entry(entity).State = EntityState.Modified;
            await _context.SaveChangesAsync(cancellationToken);
            return await Task.FromResult(true);
        }

        public async Task<bool> UpdateAsync(T entity, CancellationToken cancellationToken = default)
        {
            entity = await GetUpdateAsyncProperties(entity);
            _context.Entry(entity).State = EntityState.Modified;
            await _context.SaveChangesAsync(cancellationToken);
            return await Task.FromResult(true);
        }
        #endregion

        #region property

        private async Task<T> GetAddAsyncProperties(T entity)
        {
            User user = await _workContext.GetCurrentUserAsync();
            entity.CreatedOn = DateTime.Now;
            //entity.CreatedBy = user.Id; 
            entity.CreatedBy = "testuser";
            return entity;
        }
        private async Task<T> GetUpdateAsyncProperties(T entity)
        {
            User user = await _workContext.GetCurrentUserAsync();
            entity.UpdatedOn = DateTime.Now;
            //entity.UpdatedBy = user.Id;
            entity.UpdatedBy = "testuser";

            return entity;
        }

        private async Task<T> GetSoftDeleteAsyncProperties(T entity)
        {
            User user = await _workContext.GetCurrentUserAsync();
            entity.DeletedOn = DateTime.Now;
            //entity.DeletedBy = user.Id;
            entity.DeletedBy = "testuser";

            entity.IsSoftDeleted = true;
            return entity;
        }

        private async Task<T> GetRestoreAsyncProperties(T entity)
        {
            User user = await _workContext.GetCurrentUserAsync();
            entity.IsSoftDeleted = false;
            //entity.UpdatedBy = user.Id;
            entity.UpdatedBy = "testuser";

            entity.UpdatedOn = DateTime.Now;
            return entity;
        }
        #endregion
    }
}
