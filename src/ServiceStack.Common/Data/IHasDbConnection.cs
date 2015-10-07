//Copyright (c) Service Stack LLC. All Rights Reserved.
//License: https://raw.github.com/ServiceStack/ServiceStack/master/license.txt

#if !SL5 && !XBOX
#if !DNXCORE50
using System.Data;
#else
using System.Data.Common;
#endif

namespace ServiceStack.Data
{
#if !DNXCORE50
    public interface IHasDbConnection
	{
		IDbConnection DbConnection { get; }
	}

    public interface IHasDbCommand
    {
        IDbCommand DbCommand { get; }
    }
#else
    public interface IHasDbConnection
    {
        DbConnection DbConnection { get; }
    }

    public interface IHasDbCommand
    {
        DbCommand DbCommand { get; }
    }
#endif
}
#endif