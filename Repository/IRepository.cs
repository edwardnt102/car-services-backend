using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Repository
{
    public interface IRepository<T> where T : class
    {
        Task<IEnumerable<T>> GetAllUsingDapper(string storedProcedure);
        Task<T> GetByIdUsingDapper(long id, string storedProcedure);
        Task<bool> InsertUsingDapper(string storedProcedure, object parms);
        Task<bool> BulkInsertUsingDapper(string storedProcedure, object parms);
        Task<bool> UpdateUsingDapper(T obj, string storedProcedure, object parms);
        Task<bool> DeleteUsingDapper(long id, string storedProcedure);
        IQueryable<T> GetAll();
        IQueryable<T> GetAllNoneDeleted();
        void AddRange(List<T> entity);
        int Count();
        Task<int> CountAsync(Expression<Func<T, bool>> predicate);
        Task<T> GetSingleNoneDeletedAsync(Expression<Func<T, bool>> predicate);
        Task<T> GetSingleAsync(Expression<Func<T, bool>> predicate,
            params Expression<Func<T, object>>[] includeProperties);
        T GetSingleNoneDeleted(Expression<Func<T, bool>> predicate);
        T GetSingle(Expression<Func<T, bool>> predicate);
        Task<T> GetSingleAsync(Expression<Func<T, bool>> predicate);
        T GetSingle(Expression<Func<T, bool>> predicate, params Expression<Func<T, object>>[] includeProperties);
        IEnumerable<T> FindBy(Expression<Func<T, bool>> predicate);
        void Add(T entity);
        void Update(T entity);
        void Delete(T entity);
        void DeleteWhere(Expression<Func<T, bool>> predicate);
        bool Commit();
        Task<bool> CommitAsync();
        void BulkInsert(IList<T> items);
        void BulkUpdate(IList<T> items);
        void BulkDelete(IList<T> items);
    }
}
