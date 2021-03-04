using PCBuild.Fatory.NHibernate;
using PCBuild.Fatory.Repos;
using PCBuild.Modles;
using NHibernate;
using Ninject.Activation;
using Ninject.Modules;

namespace PCBuild.Fatory.Install
{
    public class Install : NinjectModule
    {
        
        public override void Load()
        {
            //Account and User
            Bind<ISession>().ToMethod(GetSession);
            Bind<IPCBuildsRepository>().To<PCBuildsRepository>();
            Bind<IGenericRepo<MemoryEntity>>().To<GenericRepo<MemoryEntity>>();
            Bind<IGenericRepo<ProcessorEntity>>().To<GenericRepo<ProcessorEntity>>();
            Bind<IGenericRepo<UsbToBuildEntity>>().To<GenericRepo<UsbToBuildEntity>>();
            Bind<IGenericRepo<UsbEntity>>().To<GenericRepo<UsbEntity>>();
            Bind<IGenericRepo<GraphicsEntity>>().To<GenericRepo<GraphicsEntity>>();
            Bind<IGenericRepo<DiskStorageEntity>>().To<GenericRepo<DiskStorageEntity>>();
            Bind<IGenericRepo<PowerSupplyEntity>>().To<GenericRepo<PowerSupplyEntity>>();
        }

        
        private static ISession GetSession(IContext context)
        {
            return NHibernateSessionFactory.GetSession();
        }


    } 
}

