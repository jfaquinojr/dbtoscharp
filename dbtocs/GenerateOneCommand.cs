using CliFx;
using CliFx.Attributes;
using Dapper;
using dbtocs.Core;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dbtocs
{

    [Command("single", Description = "Generate a POCO from a table or view")]
    public class GenerateOneCommand : ICommand
    {

        public GenerateOneCommand()
        {
        }

        [CommandOption("table", 't', Description = "Table or View", IsRequired = true)]
        public string TableName { get; set; }

        [CommandOption("cn", 'c', Description = "Connection string", IsRequired = true)]
        public string ConnectionString { get; set; }

        [CommandOption("classname", 'n', Description = "Generated Class name", IsRequired = false)]
        public string ClassOutputName { get; set; }

        [CommandOption("out", 'o', Description = "Output to file", IsRequired = false)]
        public string OutputPath { get; set; }

        public ValueTask ExecuteAsync(IConsole console)
        {
            try
            {
                if (!string.IsNullOrEmpty(OutputPath) && !IsPathValid(OutputPath))
                {
                    console.Output.Write("Invalid output path");
                    return default;
                }

                var columns = GetTableColumns();
                var clsOutput = GenerateClass(columns);

                if (string.IsNullOrEmpty(OutputPath))
                {
                    console.Output.WriteLine(clsOutput);
                }
                else
                {
                    File.WriteAllText(OutputPath, clsOutput);
                }


            }
            catch (Exception)
            {

                throw;
            }


            return default;
        }

        private bool IsPathValid(string path)
        {
            System.IO.FileInfo fi = null;
            try
            {
                fi = new System.IO.FileInfo(path);
            }
            catch (ArgumentException) { }
            catch (System.IO.PathTooLongException) { }
            catch (NotSupportedException) { }
            
            return !ReferenceEquals(fi, null);
        }

        private string GenerateClass(List<TableColumns> columns)
        {
            var props = "";
            foreach (var c in columns)
            {
                props += $@"
                    public {GetFieldType(c.DataType, c.Nullable == "YES")} {(c.ColumnName)} {{ get; set; }}
                ";
            }

            var cls = $@"
            public class {ClassOutputName ?? columns.FirstOrDefault()?.TableName ?? "Table1"} {{
                {props}
            }}
";

            return cls;
        }

        private string GetFieldType(string datatType, bool isNullable)
        {
            var field = FieldFactory.Create(datatType);
            return isNullable ? field.NameNullable : field.Name;
        }

        private List<TableColumns> GetTableColumns()
        {
            List<TableColumns> results;
            using (var cn = new SqlConnection(ConnectionString))
            {
                var parameters = new { tablename = TableName };
                var sql = @"
                        SELECT   SchemaName = c.table_schema,
                                 TableName = c.table_name,
                                 ColumnName = c.column_name,
                                 DataType = data_type,
                                 Nullable = c.IS_NULLABLE
                        FROM     information_schema.columns c
                                 INNER JOIN information_schema.tables t
                                   ON c.table_name = t.table_name
                                      AND c.table_schema = t.table_schema
                                      --AND t.table_type = 'BASE TABLE'
                        WHERE 1=1
                        AND T.TABLE_NAME = @tablename
                        ORDER BY SchemaName,
                                 TableName,
                                 ordinal_position
                    ";
                results = cn.Query<TableColumns>(sql, parameters).ToList();
            }
            return results;
        }



        public class TableColumns
        {
            public string SchemaName { get; set; }
            public string TableName { get; set; }
            public string ColumnName { get; set; }
            public string DataType { get; set; }
            public string Nullable { get; set; }
        }
    }
}
