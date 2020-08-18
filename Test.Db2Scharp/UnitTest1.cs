using dbtocs.Core;
using System;
using Xunit;

namespace Test.Db2Scharp
{
    public class UnitTest1
    {
        [Fact]
        public void TestBoolean()
        {
            var ooo = FieldFactory.Create("bit");
            Assert.Equal("bool", ooo.Name);
        }

        [Fact]
        public void TestBooleanNull()
        {
            var ooo = FieldFactory.Create("bit");
            Assert.Equal("bool?", ooo.NameNullable);
        }




        [Fact]
        public void TestTinyInt()
        {
            var fld = FieldFactory.Create("tinyint");
            Assert.Equal("byte", fld.Name);
        }


        [Fact]
        public void TestTinyIntNull()
        {
            var fld = FieldFactory.Create("tinyint");
            Assert.Equal("byte?", fld.NameNullable);
        }


        [Fact]
        public void TestByteArray()
        {
            var fld = FieldFactory.Create("binary");
            Assert.Equal("byte[]", fld.Name);
        }


        [Fact]
        public void TestByteArrayNull()
        {
            var fld = FieldFactory.Create("binary");
            Assert.Equal("byte?[]", fld.NameNullable);
        }

        [Fact]
        public void TestImage()
        {
            var fld = FieldFactory.Create("binary");
            Assert.Equal("byte[]", fld.Name);
        }


        [Fact]
        public void TestImageNull()
        {
            var fld = FieldFactory.Create("binary");
            Assert.Equal("byte?[]", fld.NameNullable);
        }
    }
}
