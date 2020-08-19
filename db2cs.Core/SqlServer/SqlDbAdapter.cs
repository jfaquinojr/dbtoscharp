using Dapper;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace dbtocs.Core.SqlServer
{
    public class SqlDbAdapter : IDbAdapter
    {

        private readonly Dictionary<string, (string, string)> data;
        private readonly string connectionString;

        public SqlDbAdapter(string connectionString)
        {
            data = new Dictionary<string, (string, string)>();
            data.Add("bit", ("bool", "bool?"));
            data.Add("tinyint", ("byte", "byte?"));
            data.Add("binary", ("byte[]", "byte?[]"));
            data.Add("image", ("byte[]", "byte?[]"));
            data.Add("rowversion", ("byte[]", "byte?[]"));
            data.Add("timestamp", ("byte[]", "byte?[]"));
            data.Add("varbinary", ("byte[]", "byte?[]"));
            data.Add("date", ("DateTime", "DateTime?"));
            data.Add("datetime", ("DateTime", "DateTime?"));
            data.Add("datetime2", ("DateTime", "DateTime?"));
            data.Add("datetimeoffset", ("DateTimeOffset", "DateTimeOffset?"));
            data.Add("decimal", ("decimal", "decimal?"));
            data.Add("money", ("decimal", "decimal?"));
            data.Add("numeric", ("decimal", "decimal?"));
            data.Add("smallmoney", ("decimal", "decimal?"));
            data.Add("float", ("double", "double?"));
            data.Add("uniqueidentifier", ("Guid", "Guid?"));
            data.Add("smallint", ("int", "int?"));
            data.Add("int", ("int", "int?"));
            data.Add("bigint", ("int", "int?"));
            data.Add("sql_variant", ("object", "object"));
            data.Add("real", ("Single", "Single?"));
            data.Add("char", ("string", "string"));
            data.Add("nchar", ("string", "string"));
            data.Add("ntext", ("string", "string"));
            data.Add("nvarchar", ("string", "string"));
            data.Add("text", ("string", "string"));
            data.Add("varchar", ("string", "string"));
            data.Add("time", ("TimeSpan", "TimeSpan"));
            data.Add("_", ("object", "object"));
            this.connectionString = connectionString;
        }

        public IEnumerable<TableColumn> GetAllColumns()
        {
            using (var cn = new SqlConnection(connectionString))
            {
                string sql = GenerateSqlWithOutParam();
                var results = cn.Query<TableColumn>(sql).ToList();
                return results;
            }

        }

        private static string GenerateSelectSql()
        {
            return @"
                        SELECT   SchemaName = c.table_schema,
                                 TableName = c.table_name,
                                 ColumnName = c.column_name,
                                 DataType = data_type,
                                 Nullable = c.IS_NULLABLE,
                                 TableType = t.TABLE_TYPE
                        FROM     information_schema.columns c
                                 INNER JOIN information_schema.tables t
                                   ON c.table_name = t.table_name
                                      AND c.table_schema = t.table_schema
                                      --AND t.table_type = 'BASE TABLE'
                ";
        }
        private static string GenerateSqlWithParam()
        {
            return @$"
                        {GenerateSelectSql()}
                        WHERE 1=1
                        AND T.TABLE_NAME = @tablename
                        ORDER BY SchemaName,
                                 TableName,
                                 ordinal_position
                    ";
        }

        private static string GenerateSqlWithOutParam()
        {
            return @$"
                        {GenerateSelectSql()}
                        ORDER BY SchemaName,
                                 TableName,
                                 ordinal_position
                    ";
        }

        public IEnumerable<TableColumn> GetColumns(string table)
        {
            using (var cn = new SqlConnection(connectionString))
            {
                var parameters = new { tablename = table };
                string sql = GenerateSqlWithParam();
                var results = cn.Query<TableColumn>(sql, parameters).ToList();
                return results;
            }
        }

        public string GetFieldType(string datatype, bool isNullable)
        {
            if(string.IsNullOrEmpty(datatype) || !data.ContainsKey(datatype))
            {
                return "object";
            }

            return isNullable ? data[datatype].Item2 : data[datatype].Item1;
        }
    }
}
