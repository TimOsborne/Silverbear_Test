using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PCBuild.Modles
{
    public class DiskStorageEntity : Entity
    {
        public virtual string Size { get; set; }
        public virtual string DiskType { get; set; }
    }
}
