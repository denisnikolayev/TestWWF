using System;
using System.Linq;
using System.Web;
using NHibernate;
using NHibernate.Cfg;
using NHibernate.Cfg.MappingSchema;
using NHibernate.Dialect;
using NHibernate.Mapping.ByCode;
using NHibernate.Tool.hbm2ddl;
using System.Reflection;


namespace WebApplication1
{
    public class NHibernateHelper
    {
        private static ISessionFactory _sessionFactory;

        private static ISessionFactory SessionFactory
        {
            get
            {
                if (_sessionFactory == null)
                    InitializeSessionFactory();
                return _sessionFactory;
            }
        }

        public static void CreateSessionFactory()
        {
            if (_sessionFactory == null)
                InitializeSessionFactory();
        }

        private static void InitializeSessionFactory()
        {
            var cfg = new Configuration().Configure();
            var mapper = new ModelMapper();
            mapper.AddMappings(Assembly.GetExecutingAssembly().GetExportedTypes());
            HbmMapping mapping = mapper.CompileMappingForAllExplicitlyAddedEntities();
            cfg.AddMapping(mapping);
            var schemaUpdate = new SchemaUpdate(cfg);
            schemaUpdate.Execute(true, true);

            if (schemaUpdate.Exceptions.Count > 0)
            {
                string error = schemaUpdate.Exceptions.Aggregate("", (current, e) => current + (current + "; " + e));
                throw new Exception($"Error Create schema Oracle: {error}");
            }

            _sessionFactory = cfg.BuildSessionFactory();
        }

        public static ISession OpenSessionFactory()
        {

            return SessionFactory.OpenSession();
        }

        public static void CloseSessionFactory()
        {
            _sessionFactory?.Close();
        }
    }
}
