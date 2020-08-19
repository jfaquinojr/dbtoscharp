using AutoFixture;
using dbtocs.Core;
using dbtocs.Core.Mock;
using dbtocs.Core.SqlServer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xunit;

namespace Test.Db2Scharp
{
    public class GetColumnTests
    {
        [Fact]
        public void TestGetAllColumns()
        {
            var fixture = new Fixture();
            var db = fixture.Create<MockDbAdapater>();
            var cols = db.GetAllColumns();

            Assert.True(cols.Count() > 0);
        }

        [Fact]
        public void TestGetColumnsByTable()
        {

            var fixture = new Fixture();
            var db = fixture.Create<MockDbAdapater>();
            var all = db.GetAllColumns();
            var first = all.First();
            var cols = db.GetColumns(first.TableName);

            Assert.True(cols.Count() > 0);
        }

    }
}
