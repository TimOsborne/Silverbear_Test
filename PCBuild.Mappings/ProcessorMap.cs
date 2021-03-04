using PCBuild.Modles;
using NHibernate.Mapping.ByCode;
using NHibernate.Mapping.ByCode.Conformist;

namespace PCBuild.Mappings
{
    public class ProcessorMap : ClassMapping<ProcessorEntity>
    {
        public ProcessorMap()
        {

            Table("Processor");
            Id(x => x.Id, map => map.Generator(Generators.Identity));
            Property(x => x.Name);

        }
    }
}
