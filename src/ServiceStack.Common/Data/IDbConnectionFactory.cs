#if !SL5
#if !DNXCORE50
using System.Data;
#else
using System.Data.Common;
#endif

namespace ServiceStack.Data
{
#if !DNXCORE50
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
