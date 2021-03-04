using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PCBuild.Fatory.Repos
{
    public interface IGenericRepo<T> : IRepositoryBase<T> where T : class
    {

    }
}
