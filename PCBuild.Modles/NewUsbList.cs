using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PCBuild.Modles
{
    public class NewUsbList
    {
        public virtual int Id { get; set; } = 0;
        public virtual UsbEntity Usb { get; set; }
        public virtual int NumberOf { get; set; }

    }
}
