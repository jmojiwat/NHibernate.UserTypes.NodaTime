using System;
using System.Reflection;
using NHibernate.Cfg;
using NHibernate.Dialect;
using NHibernate.Driver;
using NHibernate.Mapping.ByCode;
using NHibernate.Tool.hbm2ddl;

namespace NHibernate.UserTypes.NodaTime.Test
{
    public class DatabaseFixture
    {
        public ISessionFactory SessionFactory { get; }

        public DatabaseFixture()
        {
            var types = Assembly.Load("NHibernate.UserTypes.NodaTime.Test").GetExportedTypes();
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
            schemaExport.Drop(true, true);
            schemaExport.Create(true, true);
            return schemaExport;
        }

        public static Configuration BuildNhibernateConfiguration()
        {
            var configuration = new Configuration();
            configuration.DataBaseIntegration(db =>
            {
                db.Dialect<MsSql2012Dialect>();
                db.Driver<Sql2008ClientDriver>();
                db.ConnectionString = @"Data Source=(localdb)\mssqllocaldb;Initial Catalog=nodatime;Integrated Security=True";
                db.LogSqlInConsole = true;
                db.LogFormattedSql = true;
                db.BatchSize = 1;
            });

            configuration.SessionFactory().GenerateStatistics();
            return configuration;
        }
    }
}