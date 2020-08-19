using dbtocs.Core.SqlServer;
using System;
using System.Collections.Generic;
using System.Text;

namespace dbtocs.Core
{
    public static class DbAdapterFactory
    {
        public static IDbAdapter Create(string dbType, string connectionString)
        {
            switch(dbType)
            {
                case Constants.DbTypes.SqlServer:
                default:
                    return new SqlDbAdapter(connectionString);
            }
        }
    }
}
