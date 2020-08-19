using CliFx;
using CliFx.Attributes;
using Dapper;
using dbtocs.Core;
using dbtocs.Core.Helpers;
using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dbtocs
{

    [Command("multi", Description = "Generate POCOs from an existing database")]
    public class GenerateMultipleCommand : ICommand
    {

        public GenerateMultipleCommand()
        {
        }

        [CommandOption("cn", 'c', Description = "Connection string", IsRequired = true)]
        public string ConnectionString { get; set; }


        [CommandOption("type", 't', Description = "table or view. omit to select all", IsRequired = false)]
        public string TableType { get; set; }

        [CommandOption("out", 'o', Description = "Output to directory", IsRequired = false)]
        public string OutputPath { get; set; }

        [CommandOption("ns", Description = "Namespace of the class", IsRequired = false)]
        public string Namespace { get; set; }

        public ValueTask ExecuteAsync(IConsole console)
        {
            try
            {
                if (!string.IsNullOrEmpty(OutputPath) && !IsPathValid(OutputPath))
                {
                    console.Output.Write("Invalid output path");
                    return default;
                }

                var fullPath =  Path.GetFullPath(OutputPath);
                if (!Directory.Exists(fullPath))
                {
                    Directory.CreateDirectory(fullPath);
                }

                var db = DbAdapterFactory.Create(Constants.DbTypes.SqlServer, ConnectionString);

                var columns = db.GetAllColumns();
                
                if(!string.IsNullOrEmpty(TableType))
                {
                    string tableType = "BASE TABLE";
                    if (TableType.ToLower() == "view")
                    {
                        tableType = "VIEW";
                    }

                    columns = columns.Where(m => m.TableType == tableType).ToList();
                }



                var grouped = columns.GroupBy(g => g.TableName);

                foreach(var g in grouped)
                {
                    var clsOutput = GenerateClass(g.ToList(), db);
                    var filename = Path.Combine(fullPath, $"{g.Key}.cs");
                    if (string.IsNullOrEmpty(OutputPath))
                    {
                        console.Output.WriteLine(filename);
                    }
                    else
                    {
                        File.WriteAllText(filename, clsOutput);
                        console.Output.WriteLine($"file create '{filename}'");
                    }
                }

                //var clsOutput = GenerateClass(columns, db);




            }
            catch (Exception)
            {

                throw;
            }


            return default;
        }


        #region Helper Functions

        private string GenerateClass(IEnumerable<TableColumn> columns, IDbAdapter db)
        {
            var props = "";
            foreach (var c in columns)
            {
                props += $@"
        public {db.GetFieldType(c.DataType, c.Nullable == "YES")} {(c.ColumnName)} {{ get; set; }}
                ";
            }

            var template = ResourceHelper.GetTemplate(Constants.TemplatesPath.StandardClass);
            template = template.Replace("$namespace$", GetNamespace());
            template = template.Replace("$classname$", columns.FirstOrDefault()?.TableName ?? $"Table{Guid.NewGuid()}");
            template = template.Replace("$body$", props);

            return template;
        }

        private string GetNamespace()
        {
            var dirName = new DirectoryInfo(Directory.GetCurrentDirectory()).Name
                .Replace(".", "")
                .Replace(":", "")
                .Replace("\\", "")
                .Replace("//", "");
            return Namespace ?? (IsValidCsharpIdentifier(dirName) ? dirName : "MyNamespace");
        }

        private bool IsValidCsharpIdentifier(string ident)
        {
            CodeDomProvider provider = CodeDomProvider.CreateProvider("C#");
            return provider.IsValidIdentifier(ident);
        }

        private bool IsPathValid(string path)
        {
            try
            {
                var fullPath = Path.GetFullPath(path);
                return true;

            }
            catch { }

            return false;
        }

        #endregion
    }
}
