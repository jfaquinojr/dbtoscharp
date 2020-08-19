using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace dbtocs.Core.Mock
{
    public class MockDbAdapater : IDbAdapter
    {
        private readonly List<TableColumn> data;

        public MockDbAdapater(List<TableColumn> data)
        {
            this.data = data;
        }

        public IEnumerable<TableColumn> GetAllColumns()
        {
            return data;
        }

        public IEnumerable<TableColumn> GetColumns(string table)
        {
            return data.Where(d => d.TableName.ToLower() == table.ToLower());
        }

        public string GetFieldType(string datatype, bool isNullable)
        {
            throw new NotImplementedException();
        }
    }
}
