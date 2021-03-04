using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PCBuild.Modles
{
    public class UsbToBuildEntity : Entity
    {
        public virtual PCBuildsEntity PcBuild { get; set; }
        public virtual UsbEntity Usb { get; set; }
        public virtual int NumberOf { get; set; }

    }
}
