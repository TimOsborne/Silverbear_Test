using PCBuild.Modles;
using NHibernate.Mapping.ByCode;
using NHibernate.Mapping.ByCode.Conformist;


namespace PCBuild.Mappings
{
    public class PCBuildMap : ClassMapping<PCBuildsEntity>
    {

        public PCBuildMap() {

            Table("PCBuilds");
            Id(x => x.Id, m => m.Generator(Generators.Identity));
            Property(x => x.PcWeight);
            ManyToOne(x => x.Memory, m =>
            {
                m.Class(typeof(MemoryEntity));
                m.ForeignKey("fk_PcBuild_Memory");
                
                m.Fetch(FetchKind.Join);
            });

            Bag(x => x.USBs, mapper => {
                mapper.Inverse(true);
                mapper.Cascade(Cascade.All);
                mapper.Lazy(CollectionLazy.Lazy);
                mapper.Key(k =>
                {
                    k.Column("PcBuild");
                    k.ForeignKey("fk_account_users");
                    k.Update(false);
                });
            }, 
                a=> a.OneToMany(c => c.Class(typeof(UsbToBuildEntity)))
            );           

            ManyToOne(x => x.DiskStorage, m =>
            {
                m.Class(typeof(DiskStorageEntity));
                m.ForeignKey("fk_PcBuild_DiskStorage");
                
                m.Fetch(FetchKind.Join);
            });

            ManyToOne(x => x.Graphics, m =>
            {
                m.Class(typeof(GraphicsEntity));
                m.ForeignKey("fk_PcBuild_Graphics");
                
                m.Fetch(FetchKind.Join);
            });


            ManyToOne(x => x.PowerSupply, m =>
            {
                m.Class(typeof(PowerSupplyEntity));
                m.ForeignKey("fk_PcBuild_PowerSupply");
                
                m.Fetch(FetchKind.Join);
            });

            ManyToOne(x => x.Processors, m =>
            {
                m.Class(typeof(ProcessorEntity));
                m.ForeignKey("fk_PcBuild_Processors");
                
                m.Fetch(FetchKind.Join);
            });

            


        }


    }
}
