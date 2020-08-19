using System;
using System.Collections.Generic;
using System.Text;

namespace dbtocs.Core
{
    public class TableColumn
    {
        public string SchemaName { get; set; }
        public string TableName { get; set; }
        public string ColumnName { get; set; }
        public string DataType { get; set; }
        public string Nullable { get; set; }
        public string TableType { get; set; }
    }
}
