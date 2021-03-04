using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NHibernate;
using PCBuild.Modles;

namespace PCBuild.Fatory.Repos
{
    public class PCBuildsRepository : RepositoryBase<PCBuildsEntity>, IPCBuildsRepository
    {
        public PCBuildsRepository(ISession session) : base(session)
        {
        }
    }
}
