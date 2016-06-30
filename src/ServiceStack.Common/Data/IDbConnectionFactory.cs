#if !SL5
#if !NET_CORE
using System.Data;
#else
using System.Data.Common;
#endif

namespace ServiceStack.Data
{
#if !NET_CORE
    public interface IDbConnectionFactory
    {
        IDbConnection OpenDbConnection();
        IDbConnection CreateDbConnection();
    }
#else
    public interface IDbConnectionFactory
    {
        DbConnection OpenDbConnection();
        DbConnection CreateDbConnection();
    }
#endif
}
#endif
