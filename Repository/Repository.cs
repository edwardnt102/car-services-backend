using Dapper;
using EFCore.BulkExtensions;
using Entity.DBContext;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Repository
{
    public class Repository<T> : IRepository<T>
       where T : class
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly DatabaseContext _context;

        public Repository(IUnitOfWork unitOfWork)
        {
            _context = unitOfWork.DbContext;
            _unitOfWork = unitOfWork;
        }
        public async Task<IEnumerable<T>> GetAllUsingDapper(string storedProcedure)
        {
            var entities = await _unitOfWork.Connection.QueryAsync<T>
                (storedProcedure, commandType: CommandType.StoredProcedure);

            return entities;
        }

        public async Task<T> GetByIdUsingDapper(long id, string storedProcedure)
        {
            var p = new DynamicParameters();
            p.Add("@Id", id);

            var entity = await _unitOfWork.Connection.QuerySingleOrDefaultAsync<T>
                (storedProcedure, p, commandType: CommandType.StoredProcedure);

            return entity;
        }

        public async Task<bool> InsertUsingDapper(string storedProcedure, object parms)
        {
            await _unitOfWork.Connection.ExecuteAsync
               (storedProcedure, parms, commandType: CommandType.StoredProcedure, transaction: _unitOfWork.Transaction);
            return true;
        }

        public async Task<bool> BulkInsertUsingDapper(string storedProcedure, object parms)
        {
            await _unitOfWork.Connection.ExecuteAsync
                (storedProcedure, parms, commandType: CommandType.StoredProcedure, transaction: _unitOfWork.Transaction);

            return true;
        }

        public async Task<bool> UpdateUsingDapper(T obj, string storedProcedure, object parms)
        {
            await _unitOfWork.Connection.ExecuteAsync
                (storedProcedure, parms, commandType: CommandType.StoredProcedure, transaction: _unitOfWork.Transaction);

            return true;
        }

        public async Task<bool> DeleteUsingDapper(long id, string storedProcedure)
        {
            var p = new DynamicParameters();
            p.Add("@Id", id);

            await _unitOfWork.Connection.ExecuteAsync
                (storedProcedure, p, commandType: CommandType.StoredProcedure, transaction: _unitOfWork.Transaction);

            return true;
        }
        public virtual void Add(T entity)
        {
            _context.Entry(entity);
            _context.Set<T>().Add(entity);
        }

        public virtual void AddRange(List<T> entity)
        {
            _context.Set<T>().AddRange(entity);
        }

        public virtual bool Commit()
        {
            var recordsCommittedCount = _context.SaveChanges();
            return (recordsCommittedCount > 0);
        }

        public virtual async Task<bool> CommitAsync()
        {
            var recordsCommittedCount = await _context.SaveChangesAsync();
            return (recordsCommittedCount > 0);
        }

        public int Count()
        {
            return _context.Set<T>().Count();
        }

        public async Task<int> CountAsync(Expression<Func<T, bool>> predicate)
        {
            return await _context.Set<T>().CountAsync(predicate);
        }

        public void Delete(T entity)
        {
            EntityEntry dbEntityEntry = _context.Entry(entity);
            dbEntityEntry.State = EntityState.Deleted;
        }

        public void DeleteWhere(Expression<Func<T, bool>> predicate)
        {
            IEnumerable<T> entities = _context.Set<T>().Where(predicate);

            foreach (var entity in entities)
            {
                _context.Entry(entity).State = EntityState.Deleted;
            }
        }

        public IEnumerable<T> FindBy(Expression<Func<T, bool>> predicate)
        {
            return _context.Set<T>().Where(predicate);
        }

        public IQueryable<T> GetAll()
        {
            IQueryable<T> query = _context.Set<T>();
            return query.AsQueryable();
        }

        public IQueryable<T> GetAllNoneDeleted()
        {
            IQueryable<T> query = _context.Set<T>();
            return query.AsQueryable();
        }

        public T GetSingleNoneDeleted(Expression<Func<T, bool>> predicate)
        {
            return _context.Set<T>().FirstOrDefault(predicate);
        }

        public async Task<T> GetSingleNoneDeletedAsync(Expression<Func<T, bool>> predicate)
        {
            return await _context.Set<T>().FirstOrDefaultAsync(predicate);
        }

        public async Task<T> GetSingleNoneDeletedAsync(Expression<Func<T, bool>> predicate, params Expression<Func<T, object>>[] includeProperties)
        {
            IQueryable<T> query = _context.Set<T>();
            foreach (var includeProperty in includeProperties)
            {
                query = query.Include(includeProperty);
            }

            return await query.Where(predicate).FirstOrDefaultAsync();
        }


        public T GetSingle(Expression<Func<T, bool>> predicate)
        {
            return _context.Set<T>().FirstOrDefault(predicate);
        }

        public async Task<T> GetSingleAsync(Expression<Func<T, bool>> predicate)
        {
            return await _context.Set<T>().FirstOrDefaultAsync(predicate);
        }

        public async Task<T> GetSingleAsync(Expression<Func<T, bool>> predicate, params Expression<Func<T, object>>[] includeProperties)
        {
            IQueryable<T> query = _context.Set<T>();
            foreach (var includeProperty in includeProperties)
            {
                query = query.Include(includeProperty);
            }

            return await query.Where(predicate).FirstOrDefaultAsync();
        }

        public T GetSingle(Expression<Func<T, bool>> predicate, params Expression<Func<T, object>>[] includeProperties)
        {
            IQueryable<T> query = _context.Set<T>();
            foreach (var includeProperty in includeProperties)
            {
                query = query.Include(includeProperty);
            }

            return query.Where(predicate).FirstOrDefault();
        }

        public void Update(T entity)
        {
            EntityEntry dbEntityEntry = _context.Entry<T>(entity);
            dbEntityEntry.State = EntityState.Modified;
        }

        public virtual void BulkInsert(IList<T> items) => _context.BulkInsert(items);
        public virtual void BulkDelete(IList<T> items) => _context.BulkDelete(items);
        public virtual void BulkUpdate(IList<T> items) => _context.BulkUpdate(items);
    }
}
