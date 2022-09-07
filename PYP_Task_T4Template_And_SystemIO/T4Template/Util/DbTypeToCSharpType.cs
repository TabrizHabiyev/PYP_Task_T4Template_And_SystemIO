namespace PYP_Task_T4Template_And_SystemIO.Util;

    public static class  DbTypeConverter
    {

    // Create method for Sql data type to C# data type conversion
    public static string MssqlTypeToCSharpType(string type)
    {
        switch (type)
        {
            case "bigint":
                return "long";
            case "binary":
                return "byte[]";
            case "bit":
                return "bool";
            case "char":
                return "string";
            case "date":
                return "DateTime";
            case "datetime":
                return "DateTime";
            case "datetime2":
                return "DateTime";
            case "datetimeoffset":
                return "DateTimeOffset";
            case "decimal":
                return "decimal";
            case "float":
                return "double";
            case "image":
                return "byte[]";
            case "int":
                return "int";
            case "money":
                return "decimal";
            case "nchar":
                return "string";
            case "ntext":
                return "string";
            case "numeric":
                return "decimal";
            case "nvarchar":
                return "string";
            case "real":
                return "float";
            case "smalldatetime":
                return "DateTime";
            case "smallint":
                return "short";
            case "smallmoney":
                return "decimal";
            case "text":
                return "string";
            case "time":
                return "TimeSpan";
            case "timestamp":
                return "byte[]";
            case "tinyint":
                return "byte";
            case "uniqueidentifier":
                return "Guid";
            case "varbinary":
                return "byte[]";
            case "varchar":
                return "string";
            case "xml":
                return "string";
            default:
                return "string";
        }
    }
}

