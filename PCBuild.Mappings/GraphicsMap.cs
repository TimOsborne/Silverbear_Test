using PCBuild.Modles;
using NHibernate.Mapping.ByCode;
using NHibernate.Mapping.ByCode.Conformist;

namespace PCBuild.Mappings
{
    public class GraphicsMap : ClassMapping<GraphicsEntity>
    {
        public GraphicsMap()
        {

            Table("Graphics");
            Id(x => x.Id, map => map.Generator(Generators.Identity));
            Property(x => x.Name);           

        }

    }
}
