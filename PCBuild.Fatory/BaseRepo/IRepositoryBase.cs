using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using NHibernate;
using NHibernate.Criterion;

namespace PCBuild.Fatory.Repos
{
    public interface IRepositoryBase<T> where T : class
    {
        Task<T> Get(int id);
        Task<T> SingleOrDefault(Expression<Func<T, bool>> criteria);
        Task<IList<T>> Get(Expression<Func<T, bool>> criteria);
        Task<List<T>> Get(Expression<Func<T, bool>> criteria, int count, int start);
        Task<IList<T>> Get(ICriteria criteria, int count, int start);
        Task<IList<T>> Get(ICriteria criteria);
        Task<List<T>> Get(int count, int start);
        Task<IList<T>> GetAll();
        Task<int> GetCount();
        Task<int> GetCount(ICriterion criteria);
        Task<int> GetCount(ICriteria criteria);
        Task<int> GetCount(Expression<Func<T, bool>> criteria);
        Task<T> Create(T item);
        Task<List<T>> Create(List<T> items);
        Task<T> Update(T item);
        Task<List<T>> Update(List<T> items);
        Task<T> CreateOrUpdate(T item);
        Task<List<T>> CreateOrUpdate(List<T> items);
        Task Delete(T item);
        ISession GetSession();
        void CloseSession();      

    }
}
