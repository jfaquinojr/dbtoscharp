using System;
using System.IO;
using System.Reflection;

namespace dbtocs.Core.Helpers
{
    public static class ResourceHelper
    {
        public static string GetTemplate(string template)
        {
            var assembly = Assembly.GetExecutingAssembly();
            var resourceName = $"dbtocs.Core.Templates.{template}";

            using (Stream stream = assembly.GetManifestResourceStream(resourceName))
            using (StreamReader reader = new StreamReader(stream))
            {
                return reader.ReadToEnd();
            }
        }
    }
}
