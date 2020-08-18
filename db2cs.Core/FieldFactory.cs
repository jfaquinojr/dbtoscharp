using System;
using System.Collections.Generic;
using System.Text;

namespace dbtocs.Core
{
    public static class FieldFactory
    {
        public static IField Create(string type)
        {
            switch(type)
            {
                case "bit":
                    return new BooleanField();
                case "tinyint":
                    return new ByteField();
                case "binary":
                case "image":
                case "rowversion":
                case "timestamp":
                case "varbinary":
                    return new ByteArrayField();
                case "date":
                case "datetime":
                case "datetime2":
                    return new DateTimeField();
                case "datetimeoffset":
                    return new DateTimeOffsetField();
                case "decimal":
                case "money":
                case "numeric":
                case "smallmoney":
                    return new DecimalField();
                case "float":
                    return new DoubleField();
                case "uniqueidentifier":
                    return new GuidField();
                case "smallint":
                case "int":
                case "bigint":
                    return new IntField();
                case "sql_variant":
                    return new ObjectField();
                case "real":
                    return new SingleField();
                case "char":
                case "nchar":
                case "ntext":
                case "nvarchar":
                case "text":
                case "varchar":
                    return new StringField();
                case "time":
                    return new TimeSpanField();
                default:
                    return new UnknownField();
            }
        }

    }
}
