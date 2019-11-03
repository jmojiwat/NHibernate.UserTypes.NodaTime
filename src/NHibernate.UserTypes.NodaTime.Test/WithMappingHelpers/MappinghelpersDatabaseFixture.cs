using System;
using System.Linq;
using System.Reflection;
using NHibernate.Cfg;
using NHibernate.Dialect;
using NHibernate.Driver;
using NHibernate.Mapping.ByCode;
using NHibernate.Tool.hbm2ddl;

namespace NHibernate.UserTypes.NodaTime.Test.WithMappingHelpers
{
    public class MappingHelpersDatabaseFixture : IDisposable
    {
        public ISessionFactory SessionFactory { get; }

        public MappingHelpersDatabaseFixture()
        {
            var types = Assembly.Load("NHibernate.UserTypes.NodaTime.Test").GetExportedTypes()
                .Where(t => t.Namespace != null && t.Namespace.StartsWith("NHibernate.UserTypes.NodaTime.Test.Infrastructure.WithMappingHelpers"));

            var mapper = new ModelMapper();
            mapper.AddMappings(types);
            var hbmMapping = mapper.CompileMappingForAllExplicitlyAddedEntities();
            hbmMapping.autoimport = false;

            var configuration = BuildNhibernateConfiguration();
            configuration.AddMapping(hbmMapping);

            var schemaExport = BuildSchemaExport(configuration);
            ExecuteSchema(schemaExport);

            SessionFactory = configuration.BuildSessionFactory();
        }

        private static SchemaExport BuildSchemaExport(Configuration configuration) => 
            new SchemaExport(configuration).SetDelimiter(";");

        private static SchemaExport ExecuteSchema(SchemaExport schemaExport)
        {
            schemaExport.Drop(false, true);
            schemaExport.Create(false, true);
            return schemaExport;
        }

        public static Configuration BuildNhibernateConfiguration()
        {
            var configuration = new Configuration();
            configuration.DataBaseIntegration(db =>
            {
                db.Dialect<MsSql2000Dialect>();
                db.Driver<Sql2008ClientDriver>();
                db.ConnectionString = @"Data Source=(localdb)\mssqllocaldb;Initial Catalog=nodatimemappinghelpers;Integrated Security=True";
                db.LogSqlInConsole = true;
                db.LogFormattedSql = true;
//                db.BatchSize = 1;
            });

            configuration.SessionFactory().GenerateStatistics();
            return configuration;
        }

        public void Dispose()
        {
            SessionFactory?.Dispose();
        }
    }
}