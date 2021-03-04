using PCBuild.Modles;
using NHibernate.Mapping.ByCode;
using NHibernate.Mapping.ByCode.Conformist;

namespace PCBuild.Mappings
{
    public class UsbToBuildMap : ClassMapping<UsbToBuildEntity>
    {
        public UsbToBuildMap() {

            Table("UseToBuild");
            Id(x => x.Id, m => m.Generator(Generators.Identity));
            Property(x => x.NumberOf);
            ManyToOne(x => x.PcBuild, m =>
            {
                m.Class(typeof(PCBuildsEntity));
                m.ForeignKey("fk_UsbPc_PcBuild");
                m.Update(false);
                m.Fetch(FetchKind.Join);
            });

            ManyToOne(x => x.Usb, m =>
            {
                m.Class(typeof(UsbEntity));
                m.ForeignKey("fk_UsbPc_Usb");
                m.Update(false);
                m.Fetch(FetchKind.Join);
            });


        }
    }
}
