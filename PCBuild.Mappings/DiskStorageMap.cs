using PCBuild.Modles;
using NHibernate.Mapping.ByCode;
using NHibernate.Mapping.ByCode.Conformist;

namespace PCBuild.Mappings
{
    public class DiskStorageMap : ClassMapping<DiskStorageEntity>
    {
        public DiskStorageMap() {

            Table("DiskStorage");
            Id(x => x.Id, map => map.Generator(Generators.Identity));
            Property(x => x.Size);
            Property(x => x.DiskType);

        }
        
    }
}
