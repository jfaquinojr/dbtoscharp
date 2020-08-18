using System;
using System.Collections.Generic;
using System.Text;

namespace dbtocs.Core
{

    /// <summary>
    /// All implementations come from this table (xml is not implemented)
    /// https://docs.microsoft.com/en-us/dotnet/framework/data/adonet/sql-server-data-type-mappings
    /// </summary>
    public interface IField
    {
        string Name { get; }
        string NameNullable { get; }
    }
    public class BooleanField : IField
    {
        public string Name => "bool";

        public string NameNullable => "bool?";
    }

    public class DoubleField : IField
    {
        public string Name => "double";
        public string NameNullable => "double?";
    }



    public class ByteField : IField
    {
        public string Name => "byte";

        public string NameNullable => "byte?";
    }

    public class ByteArrayField : IField
    {
        public string Name => "byte[]";

        public string NameNullable => "byte?[]";
    }

    public class DateTimeField : IField
    {
        public string Name => "DateTime";
        public string NameNullable => "DateTime?";
    }

    public class DateTimeOffsetField : IField
    {
        public string Name => "DateTimeOffset";
        public string NameNullable => "DateTimeOffset?";
    }

    public class DecimalField : IField
    {
        public string Name => "decimal";
        public string NameNullable => "decimal?";
    }

    public class GuidField : IField
    {
        public string Name => "Guid";
        public string NameNullable => "Guid?";
    }

    public class IntField : IField
    {
        public string Name => "int";
        public string NameNullable => "int?";
    }

    public class ObjectField : IField
    {
        public string Name => "object";
        public string NameNullable => "object";
    }

    public class SingleField : IField
    {
        public string Name => "Single";
        public string NameNullable => "Single?";
    }

    public class StringField : IField
    {
        public string Name => "string";
        public string NameNullable => "string";
    }

    public class TimeSpanField : IField
    {
        public string Name => "TimeSpan";
        public string NameNullable => "TimeSpan?";
    }

    public class UnknownField : IField
    {
        public string Name => "Unknown";

        public string NameNullable => "Unknown?";
    }


}
