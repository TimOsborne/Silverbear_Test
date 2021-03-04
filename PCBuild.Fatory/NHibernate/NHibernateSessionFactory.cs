using System;
using System.Collections.Generic;
using System.Reflection;
using System.Web;
using NHibernate;
using NHibernate.Cfg;
using NHibernate.Cfg.MappingSchema;
using NHibernate.Mapping.ByCode;
using NHibernate.Tool.hbm2ddl;
using PCBuild.Common.Settings;
using Environment = NHibernate.Cfg.Environment;


namespace PCBuild.Fatory.NHibernate
{
    public static class NHibernateSessionFactory
    {
        
        private const string SessionKey = "69f8cac7-df4e-4083-b667-4531f4737fce";
        private static readonly object SessionFactoryCacheLock = new object();
        private static IDictionary<string, ISessionFactory> _sessionFactoryCache;

        public static IDictionary<string, ISessionFactory> SessionFactoryCache
        {
            get
            {
                if (_sessionFactoryCache == null)
                    lock (typeof(NHibernateSessionFactory))
                    {
                        _sessionFactoryCache
                            = new Dictionary<string, ISessionFactory>();
                    }
                return _sessionFactoryCache;
            }
        }
                
        public static ISession GetSession()
        {
            string sessionKey = SessionKey;


            lock (SessionFactoryCacheLock)
            {
                if (!SessionFactoryCache.ContainsKey(sessionKey))
                {
                    string connectionString;


                    var applicationSettings = new ApplicationSettings();
                    connectionString = applicationSettings.ConnectionString;


                    BuildSessionFactory(sessionKey, connectionString);
                }
            }

            if (!SessionFactoryCache.ContainsKey(sessionKey))
            {
                throw new Exception("Unable to initiate session as session factory not found");
            }

            var sessionFactory = SessionFactoryCache[sessionKey];

            ISession session = null;

            if (HttpContext.Current != null)
            {
                if (HttpContext.Current.Items[sessionKey] == null)
                {
                    session = sessionFactory.OpenSession();
                    HttpContext.Current.Items[sessionKey] = session;
                }
                else
                {
                    session = (ISession)HttpContext.Current.Items[sessionKey];
                }
            }
            else
            {
                session = sessionFactory.OpenSession();
            }

            return session;
        }
                
        private static void BuildSessionFactory(string userName, string connectionString)
        {
            var currentAssembly = typeof(NHibernateSessionFactory).Assembly;
            var manifestName = currentAssembly.ManifestModule.Name.Replace(".dll", string.Empty);

            var configuration = new Configuration();
            configuration.SetProperty(Environment.ConnectionString, connectionString);
            configuration.Configure(currentAssembly, $"{manifestName}.NHibernate.hibernate.cfg.xml");
            configuration.AddAssembly(currentAssembly);
            configuration.SetProperty(Environment.UseSqlComments, "false");

            var mappings = GetMappings();
            configuration.AddDeserializedMapping(mappings, manifestName);
            SchemaMetadataUpdater.QuoteTableAndColumns(configuration);

            configuration.DataBaseIntegration(db =>
            {
                db.LogFormattedSql = true;
                db.LogSqlInConsole = true;
                db.AutoCommentSql = true;
            });

            SessionFactoryCache.Add(userName, configuration.BuildSessionFactory());
        }

        private static HbmMapping GetMappings()
        {
            var mapper = new ModelMapper();

            mapper.AddMappings(Assembly.Load("PCBuild.Mappings").GetExportedTypes());
            var mapping = mapper.CompileMappingForAllExplicitlyAddedEntities();

            return mapping;
        }
    }

}