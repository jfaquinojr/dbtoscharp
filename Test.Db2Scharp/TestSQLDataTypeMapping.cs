using AutoFixture;
using dbtocs.Core.SqlServer;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace Test.Db2Scharp
{
    public class TestSQLDataTypeMapping
    {
        [Fact]
        public void TestBit()
        {
            var fixture = new Fixture();
            var db = fixture.Create<SqlDbAdapter>();
            var type = db.GetFieldType("bit", false);

            Assert.Equal("bool", type);
        }

        [Fact]
        public void TestBitNullable()
        {
            var fixture = new Fixture();
            var db = fixture.Create<SqlDbAdapter>();
            var type = db.GetFieldType("bit", true);

            Assert.Equal("bool?", type);
        }

        [Fact]
        public void TestTinyInt()
        {
            var fixture = new Fixture();
            var db = fixture.Create<SqlDbAdapter>();
            var type = db.GetFieldType("tinyint", false);

            Assert.Equal("byte", type);
        }

        [Fact]
        public void TestTinyIntNullable()
        {
            var fixture = new Fixture();
            var db = fixture.Create<SqlDbAdapter>();
            var type = db.GetFieldType("tinyint", true);

            Assert.Equal("byte?", type);
        }


        [Fact]
        public void TestBinary()
        {
            var fixture = new Fixture();
            var db = fixture.Create<SqlDbAdapter>();
            var type = db.GetFieldType("binary", false);

            Assert.Equal("byte[]", type);
        }

        [Fact]
        public void TestBinaryNullable()
        {
            var fixture = new Fixture();
            var db = fixture.Create<SqlDbAdapter>();
            var type = db.GetFieldType("binary", true);

            Assert.Equal("byte?[]", type);
        }







        [Fact]
        public void TestImage()
        {
            var fixture = new Fixture();
            var db = fixture.Create<SqlDbAdapter>();
            var type = db.GetFieldType("image", false);

            Assert.Equal("byte[]", type);
        }

        [Fact]
        public void TestImageNullable()
        {
            var fixture = new Fixture();
            var db = fixture.Create<SqlDbAdapter>();
            var type = db.GetFieldType("image", true);

            Assert.Equal("byte?[]", type);
        }










        [Fact]
        public void TestRowVersion()
        {
            var fixture = new Fixture();
            var db = fixture.Create<SqlDbAdapter>();
            var type = db.GetFieldType("rowversion", false);

            Assert.Equal("byte[]", type);
        }

        [Fact]
        public void TestRowVersionNullable()
        {
            var fixture = new Fixture();
            var db = fixture.Create<SqlDbAdapter>();
            var type = db.GetFieldType("rowversion", true);

            Assert.Equal("byte?[]", type);
        }











        [Fact]
        public void TestTimeStamp()
        {
            var fixture = new Fixture();
            var db = fixture.Create<SqlDbAdapter>();
            var type = db.GetFieldType("timestamp", false);

            Assert.Equal("byte[]", type);
        }

        [Fact]
        public void TestTimeStampNullable()
        {
            var fixture = new Fixture();
            var db = fixture.Create<SqlDbAdapter>();
            var type = db.GetFieldType("timestamp", true);

            Assert.Equal("byte?[]", type);
        }










        [Fact]
        public void TestVarbinary()
        {
            var fixture = new Fixture();
            var db = fixture.Create<SqlDbAdapter>();
            var type = db.GetFieldType("varbinary", false);

            Assert.Equal("byte[]", type);
        }

        [Fact]
        public void TestVarbinaryNullable()
        {
            var fixture = new Fixture();
            var db = fixture.Create<SqlDbAdapter>();
            var type = db.GetFieldType("varbinary", true);

            Assert.Equal("byte?[]", type);
        }




        [Fact]
        public void TestNull()
        {
            var fixture = new Fixture();
            var db = fixture.Create<SqlDbAdapter>();
            var type = db.GetFieldType(null, false);

            Assert.Equal("object", type);
        }
















        [Fact]
        public void TestDate()
        {
            var fixture = new Fixture();
            var db = fixture.Create<SqlDbAdapter>();
            var type = db.GetFieldType("date", false);

            Assert.Equal("DateTime", type);
        }

        [Fact]
        public void TestDateNullable()
        {
            var fixture = new Fixture();
            var db = fixture.Create<SqlDbAdapter>();
            var type = db.GetFieldType("date", true);

            Assert.Equal("DateTime?", type);
        }

        [Fact]
        public void TestDateTime()
        {
            var fixture = new Fixture();
            var db = fixture.Create<SqlDbAdapter>();
            var type = db.GetFieldType("datetime", false);

            Assert.Equal("DateTime", type);
        }

        [Fact]
        public void TestDateTimeNullable()
        {
            var fixture = new Fixture();
            var db = fixture.Create<SqlDbAdapter>();
            var type = db.GetFieldType("datetime", true);

            Assert.Equal("DateTime?", type);
        }

        [Fact]
        public void TestDate2Time()
        {
            var fixture = new Fixture();
            var db = fixture.Create<SqlDbAdapter>();
            var type = db.GetFieldType("datetime2", false);

            Assert.Equal("DateTime", type);
        }

        [Fact]
        public void TestDate2TimeNullable()
        {
            var fixture = new Fixture();
            var db = fixture.Create<SqlDbAdapter>();
            var type = db.GetFieldType("datetime2", true);

            Assert.Equal("DateTime?", type);
        }













        [Fact]
        public void TestDateTimeOffset()
        {
            var fixture = new Fixture();
            var db = fixture.Create<SqlDbAdapter>();
            var type = db.GetFieldType("datetimeoffset", false);

            Assert.Equal("DateTimeOffset", type);
        }

        [Fact]
        public void TestDateTimeOffsetNullable()
        {
            var fixture = new Fixture();
            var db = fixture.Create<SqlDbAdapter>();
            var type = db.GetFieldType("datetimeoffset", true);

            Assert.Equal("DateTimeOffset?", type);
        }


















        [Fact]
        public void TestDecimal()
        {
            var fixture = new Fixture();
            var db = fixture.Create<SqlDbAdapter>();
            var type = db.GetFieldType("decimal", false);

            Assert.Equal("decimal", type);
        }

        [Fact]
        public void TestDecimalNullable()
        {
            var fixture = new Fixture();
            var db = fixture.Create<SqlDbAdapter>();
            var type = db.GetFieldType("decimal", true);

            Assert.Equal("decimal?", type);
        }

        [Fact]
        public void TestMoney()
        {
            var fixture = new Fixture();
            var db = fixture.Create<SqlDbAdapter>();
            var type = db.GetFieldType("money", false);

            Assert.Equal("decimal", type);
        }

        [Fact]
        public void TestMoneyNullable()
        {
            var fixture = new Fixture();
            var db = fixture.Create<SqlDbAdapter>();
            var type = db.GetFieldType("money", true);

            Assert.Equal("decimal?", type);
        }


        [Fact]
        public void TestNumeric()
        {
            var fixture = new Fixture();
            var db = fixture.Create<SqlDbAdapter>();
            var type = db.GetFieldType("numeric", false);

            Assert.Equal("decimal", type);
        }

        [Fact]
        public void TestNumericNullable()
        {
            var fixture = new Fixture();
            var db = fixture.Create<SqlDbAdapter>();
            var type = db.GetFieldType("numeric", true);

            Assert.Equal("decimal?", type);
        }

        [Fact]
        public void TestSmallMoney()
        {
            var fixture = new Fixture();
            var db = fixture.Create<SqlDbAdapter>();
            var type = db.GetFieldType("smallmoney", false);

            Assert.Equal("decimal", type);
        }

        [Fact]
        public void TestSmallMoneyNullable()
        {
            var fixture = new Fixture();
            var db = fixture.Create<SqlDbAdapter>();
            var type = db.GetFieldType("smallmoney", true);

            Assert.Equal("decimal?", type);
        }











        [Fact]
        public void TestUnknown()
        {
            var fixture = new Fixture();
            var db = fixture.Create<SqlDbAdapter>();
            var type = db.GetFieldType("xxxxxxxxxxxx", false);

            Assert.Equal("object", type);
        }

        [Fact]
        public void TestUnknownNullable()
        {
            var fixture = new Fixture();
            var db = fixture.Create<SqlDbAdapter>();
            var type = db.GetFieldType("xxxxxxxxxxxx", true);

            Assert.Equal("object", type);
        }

    }
}
