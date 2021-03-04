using PCBuild.Modles;
using NHibernate.Mapping.ByCode;
using NHibernate.Mapping.ByCode.Conformist;

namespace PCBuild.Mappings
{
    public class PowerSupplyMap : ClassMapping<PowerSupplyEntity>
    {
        public PowerSupplyMap()
        {

            Table("PowerSupply");
            Id(x => x.Id, map => map.Generator(Generators.Identity));
            Property(x => x.watts);

        }
    }
}
