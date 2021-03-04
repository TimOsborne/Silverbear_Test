using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using NHibernate;
using NHibernate.Criterion;
using NHibernate.Linq;

namespace PCBuild.Fatory.Repos
{
    public class RepositoryBase<T> : IRepositoryBase<T> where T : class
    {
        protected ISession Session { get; private set; }

        public RepositoryBase(ISession session)
        {
            Session = session;
        }

        public async Task<T> Create(T item)
        {
            try
            {

                await (Session.SaveAsync(item));
                using (var transaction = Session.BeginTransaction())
                {
                    await (Session.SaveAsync(item));

                    await transaction.CommitAsync();
                }
            }
            catch (Exception ex) {
                throw ex;
            }
                return item;


        }

        public async Task<List<T>> Create(List<T> items)
        {
          
            using (var transaction = Session.BeginTransaction())
            {
                foreach (var item in items)
                {
                    await Session.SaveAsync(item);
                }

                await transaction.CommitAsync();
            }
            return items;
        }

        public async Task<T> Update(T item)
        {
            
            using (var transaction = Session.BeginTransaction())
            {
                await Session.UpdateAsync(item);

                await transaction.CommitAsync();
            }

            return item;
        }

        public async Task<List<T>> Update(List<T> items)
        {
           
                using (var transaction = Session.BeginTransaction())
                {
                    foreach (var item in items)
                    {
                        await Session.UpdateAsync(item);
                    }

                    await transaction.CommitAsync();
                }
           

            return items;
        }

        public async Task<T> CreateOrUpdate(T item)
        {
           
                using (var transaction = Session.BeginTransaction())
                {
                    await Session.SaveOrUpdateAsync(item);

                    await transaction.CommitAsync();
                }            

            return item;
        }

        public async Task<List<T>> CreateOrUpdate(List<T> items)
        {
            
                using (var transaction = Session.BeginTransaction())
                {
                    foreach (var item in items)
                    {
                        await Session.SaveOrUpdateAsync(item);
                    }

                    await transaction.CommitAsync();
                }
            

            return items;
        }

        public async Task<T> Get(int id)
        {
            return await Session.GetAsync<T>(id);
        }

        public async Task<T> SingleOrDefault(Expression<Func<T, bool>> criteria)
        {
            return await Session.QueryOver<T>().Where(criteria).SingleOrDefaultAsync<T>();
        }


        public async Task<List<T>> Get(Expression<Func<T, bool>> criteria, int count, int start)
        {
            return await Session.Query<T>().Where(criteria).Skip(start).Take(count).ToListAsync();
        }

        public async Task<IList<T>> Get(Expression<Func<T, bool>> criteria)
        {
            return await Session.QueryOver<T>().Where(criteria).ListAsync<T>();
        }

        public async Task<IList<T>> Get(ICriteria criteria, int count, int start)
        {
            return await criteria.SetFirstResult(start).SetMaxResults(count).ListAsync<T>();
        }

        public async Task<IList<T>> Get(ICriteria criteria)
        {
            return await criteria.ListAsync<T>();
        }

        public async Task<List<T>> Get(int count, int start)
        {
            return await Session.Query<T>().Skip(start).Take(count).ToListAsync();
        }

        public async Task<IList<T>> GetAll()
        {
            return await Session.QueryOver<T>().ListAsync();
        }

        public async Task<int> GetCount()
        {
            return await Session.QueryOver<T>().Select(Projections.RowCount()).SingleOrDefaultAsync<int>();
        }

        public async Task<int> GetCount(ICriterion criteria)
        {
            return await Session.CreateCriteria<T>().Add(criteria).SetProjection(Projections.RowCount()).UniqueResultAsync<int>();
        }

        public async Task<int> GetCount(ICriteria criteria)
        {
            return await criteria.SetProjection(Projections.RowCount()).UniqueResultAsync<int>();
        }

        public async Task<int> GetCount(Expression<Func<T, bool>> criteria)
        {
            return await Session.QueryOver<T>().Where(criteria).Select(Projections.RowCount()).SingleOrDefaultAsync<int>();
        }

        public async Task Delete(T item)
        {
            await Session.DeleteAsync(item);
            await Session.FlushAsync();
        }

        public ISession GetSession()
        {
            return Session;
        }
              

        public void CloseSession()
        {
            Session.Close();
        }
    }
}
