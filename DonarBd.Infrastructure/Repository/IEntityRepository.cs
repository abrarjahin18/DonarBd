using DonarBd.Core;
using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace DonarBd.Infrastructure.Repository
{
    public interface IEntityRepository<T> where T : BaseEntity
    {
        Task<T> GetByIdAsync(long id, CancellationToken cancellationToken = default);
        IQueryable<T> AllAsIQueryable();
        Task<IReadOnlyList<T>> GetAllAsync(CancellationToken cancellationToken = default);
        Task<IReadOnlyList<T>> GetAllDeletedAsync(CancellationToken cancellationToken = default);
        Task<IReadOnlyList<T>> GetAllPagedAsync(int recSkip, int recTake, CancellationToken cancellationToken = default);
        Task<IReadOnlyList<T>> GetDeletedPagedAsync(int recSkip, int recTake, CancellationToken cancellationToken = default);
        Task<T> AddAsync(T entity, CancellationToken cancellationToken = default);
        Task AddRangeAsync(IEnumerable<T> entities, CancellationToken cancellationToken = default);
        Task<bool> UpdateAsync(T entity, CancellationToken cancellationToken = default);
        Task<bool> PermanentDeleteAsync(T entity, CancellationToken cancellationToken = default);
        Task<bool> PermanentDeleteByIdAsync(long id, CancellationToken cancellationToken = default);
        Task<bool> DeleteAsync(T entity, CancellationToken cancellationToken = default);
        Task<bool> DeleteByIdAsync(long id, CancellationToken cancellationToken = default);
        Task<bool> RestoreAsync(T entity, CancellationToken cancellationToken = default);
        Task<bool> RestoreByIdAsync(long id, CancellationToken cancellationToken = default);
        Task<T> FirstAsync(CancellationToken cancellationToken = default);
        Task<T> FirstOrDefaultAsync(CancellationToken cancellationToken = default);
        int Count();
        Task<int> CountAsync(CancellationToken cancellationToken = default);
        int CountDeleted();
        Task<int> CountDeletedAsync(CancellationToken cancellationToken = default);

        IDbContextTransaction BeginTransaction();
        Task<IDbContextTransaction> BeginTransactionAsync();
    }
}
