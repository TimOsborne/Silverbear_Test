using PCBuild.Modles;
using NHibernate.Mapping.ByCode;
using NHibernate.Mapping.ByCode.Conformist;

namespace PCBuild.Mappings
{
    public class UsbMap : ClassMapping<UsbEntity>
    {
        public UsbMap()
        {
            
                Table("usb");
                Id(x => x.Id, map => map.Generator(Generators.Identity));
                Property(x => x.UsbType); 

        }

    }
}
