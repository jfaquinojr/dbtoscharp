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

    [Command("single", Description = "Generate a POCO from a table or view")]
    public class GenerateSingleCommand : ICommand
    {

        public GenerateSingleCommand()
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

        [CommandOption("ns", Description = "Output to file", IsRequired = false)]
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

                var db = DbAdapterFactory.Create(Constants.DbTypes.SqlServer, ConnectionString);

                var columns = db.GetColumns(TableName);
                var clsOutput = GenerateClass(columns, db);

                if (string.IsNullOrEmpty(OutputPath))
                {
                    console.Output.WriteLine(clsOutput);
                }
                else
                {
                    File.WriteAllText(OutputPath, clsOutput);
                    console.Output.WriteLine($"file create '{OutputPath}'");
                }


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
            template = template.Replace("$classname$", ClassOutputName ?? columns.FirstOrDefault()?.TableName ?? "Table1");
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

        #endregion
    }
}
