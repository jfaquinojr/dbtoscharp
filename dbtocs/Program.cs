using CliFx;
using CliFx.Attributes;
using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace dbtocs
{
    //[Command]
    //public class HelloWorldCommand : ICommand
    //{
    //    public ValueTask ExecuteAsync(IConsole console)
    //    {
    //        console.Output.WriteLine("Hello world!");

    //        // Return empty task because our command executes synchronously
    //        return default;
    //    }
    //}

    [Command(Description = "Generate a POCO from a table or view")]
    public class GenerateOneCommand : ICommand
    {

        //[CommandParameter(0, Description = "Value whose logarithm is to be found.")]
        //public double Value { get; set; }

        public GenerateOneCommand()
        {
        }

        [CommandOption("table", 't', Description = "Table or View", IsRequired = true)]
        public string TableName { get; set; }

        [CommandOption("cn", 'c', Description = "Connection string", IsRequired = true)]
        public string ConnectionString { get; set; }

        public ValueTask ExecuteAsync(IConsole console)
        {
            try
            {

                var columns = GetTableColumns();

            }
            catch (Exception)
            {

                throw;
            }

            console.Output.WriteLine("YOLOOO");

            return default;
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

        public void DoThis(List<TableColumns> columns)
        {
            var code = "";
            foreach (var c in columns)
            {
                code += $@"
            	public {getFieldType(c.DataType)} {(c.ColumnName)} {{ get; set; }}
                ";
            }


            ////STEP 1 change the element inside loop
            ////STEP 2 if VIEW, change make BindingFlags.Private else BindingFlags.Public
            //foreach (var f in Ent_Projects.First().GetType().GetFields(BindingFlags.Public | BindingFlags.Instance))
            //{
            //    if (f.MemberType == MemberTypes.Field)
            //    {
            //        code += $@"
            //	public {getFieldType(f.FieldType.UnderlyingSystemType.ToString())} {removeLeadingSymbol(f.Name)} {{ get; set; }}
            //";
            //    }
            //    f.CustomAttributes.Dump();
            //    f.Dump();
            //}
            //code.Dump();

            //string getFieldType(string s)
            //{
            //    var isNullable = s.Contains("Nullable`1");
            //    var s1 = s
            //        .Replace("System.Nullable`1", "")
            //        .Replace("[", "")
            //        .Replace("]", "");
            //    s1 += isNullable ? "?" : "";
            //    return s1;
            //}

            //string removeLeadingSymbol(string s, string symbol = "_")
            //{
            //    if (s.StartsWith(symbol))
            //    {
            //        return s.Substring(symbol.Length, s.Length - 1);
            //    }

            //    return s;
            //}
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

    public static class Program
    {

        public static async Task<int> Main()
        {

            return await new CliApplicationBuilder()
                .AddCommandsFromThisAssembly()
                .Build()
                .RunAsync();
        }
    }

}
