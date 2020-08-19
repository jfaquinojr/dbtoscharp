using System;
using System.Collections.Generic;
using System.Text;

namespace dbtocs.Core
{
    public interface IDbAdapter
    {
        IEnumerable<TableColumn> GetColumns(string table);

        IEnumerable<TableColumn> GetAllColumns();

        string GetFieldType(string datatype, bool isNullable);
    }
}
