using PCBuild.Modles;
using NHibernate.Mapping.ByCode;
using NHibernate.Mapping.ByCode.Conformist;


namespace PCBuild.Mappings
{
    public class MemoryMap : ClassMapping<MemoryEntity>

    {
        public MemoryMap()
        {

            Table("Memory");
            Id(x => x.Id, map => map.Generator(Generators.Identity));
            Property(x => x.Size);

        }

    }
}
