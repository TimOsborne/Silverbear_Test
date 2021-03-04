using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PCBuild.Modles
{
    public class NewPcBuild
    {
        public int Id { get; set; }
        public  MemoryEntity Memory { get; set; }
        public  DiskStorageEntity DiskStorage { get; set; }
        public  List<NewUsbList> Usbs { get; set; }
        public  GraphicsEntity Graphics { get; set; }        
        public  PowerSupplyEntity PowerSupply { get; set; }
        public  ProcessorEntity Processors { get; set; }
        public string PcWeight { get; set; }

    }
}
