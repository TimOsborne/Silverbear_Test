using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PCBuild.Modles
{
    public class PCBuildsEntity : Entity
    {
        public virtual MemoryEntity Memory { get; set; }
        public virtual DiskStorageEntity DiskStorage { get; set; }
        public virtual IList<UsbToBuildEntity> USBs { get; set; } = new List<UsbToBuildEntity>();
        public virtual GraphicsEntity Graphics { get; set; }        
        public virtual PowerSupplyEntity PowerSupply { get; set; }
        public virtual ProcessorEntity Processors { get; set; }
        public virtual string PcWeight { get; set; }
    }
}

