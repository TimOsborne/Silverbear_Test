using System.Configuration;
using System.Data.SqlClient;
using System.IO;
using System.Reflection;
using NHibernate.Cfg;
using NHibernate.Cfg.MappingSchema;
using NHibernate.Dialect;
using NHibernate.Driver;
using NHibernate.Mapping.ByCode;
using NHibernate.Tool.hbm2ddl;
using Configuration = NHibernate.Cfg.Configuration;

namespace GenDb
{
    class Program
    {
        static void Main(string[] args)
        {
            var connectionString = ConfigurationManager.AppSettings["ConnectionString"];

            var currentAssembly = Assembly.Load(ConfigurationManager.AppSettings["MappingDll"]);// typeof (NHibernateSessionFactory).Assembly;
            var manifestName = currentAssembly.ManifestModule.Name.Replace(".dll", string.Empty);

            var configuration = new NHibernate.Cfg.Configuration();
            configuration.DataBaseIntegration(x => {

                x.ConnectionString =connectionString;
                x.Driver<SqlClientDriver>();
                x.Dialect<MsSql2008Dialect>();
                x.LogSqlInConsole = true;
                x.BatchSize = 10;
               
            });

            var mappings = GetMappings();
            configuration.AddDeserializedMapping(mappings, manifestName);
            
            //this will generate the SQL schema file in the executable folder
            BuildSchema(configuration);
        }

        private static void BuildSchema(Configuration config)
        {
            var dialect = Dialect.GetDialect(config.Properties);
            string[] schemaUpdateScript;
            using (var conn = new SqlConnection(
                    config.GetProperty("connection.connection_string")))
            {
                conn.Open();
                schemaUpdateScript = config.GenerateSchemaUpdateScript(dialect,
                                          new DatabaseMetadata(conn, dialect));
            }

            using (var file = new FileStream("db.sql", FileMode.Create, FileAccess.Write))
            using (var sw = new StreamWriter(file))
            {
                foreach (var line in schemaUpdateScript)
                {
                   
                    sw.WriteLine(line);
                    sw.WriteLine();
                }
                sw.Close();
            }
        }

        private static HbmMapping GetMappings()
        {
            var mapper = new ModelMapper();
            mapper.AddMappings(Assembly.Load(ConfigurationManager.AppSettings["MappingDll"]).GetExportedTypes());
            var mapping = mapper.CompileMappingForAllExplicitlyAddedEntities();
            return mapping;
        }
    }
}
