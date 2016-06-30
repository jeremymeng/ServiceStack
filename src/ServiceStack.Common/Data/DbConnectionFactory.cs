#if !SL5
using System;
#if !NET_CORE
using System.Data;
#else
using System.Data.Common;
#endif

namespace ServiceStack.Data
{
#if !NET_CORE
    public class DbConnectionFactory : IDbConnectionFactory
    {
        private readonly Func<IDbConnection> connectionFactoryFn;

        public DbConnectionFactory(Func<IDbConnection> connectionFactoryFn)
        {
            this.connectionFactoryFn = connectionFactoryFn;
        }

        public IDbConnection OpenDbConnection()
        {
            var dbConn = CreateDbConnection();
            dbConn.Open();
            return dbConn;
        }

        public IDbConnection CreateDbConnection()
        {
            return connectionFactoryFn();
        }
    }
#else
    public class DbConnectionFactory : IDbConnectionFactory
    {
        private readonly Func<DbConnection> connectionFactoryFn;

        public DbConnectionFactory(Func<DbConnection> connectionFactoryFn)
        {
            this.connectionFactoryFn = connectionFactoryFn;
        }

        public DbConnection OpenDbConnection()
        {
            var dbConn = CreateDbConnection();
            dbConn.Open();
            return dbConn;
        }

        public DbConnection CreateDbConnection()
        {
            return connectionFactoryFn();
        }
    }
#endif
}
#endif