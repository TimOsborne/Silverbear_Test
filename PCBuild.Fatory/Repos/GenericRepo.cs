using NHibernate;

namespace PCBuild.Fatory.Repos
{
    public class GenericRepo<T> : RepositoryBase<T>, IGenericRepo<T> where T : class
    {

        public GenericRepo(ISession session) : base(session)
        {
        }

    }
}
