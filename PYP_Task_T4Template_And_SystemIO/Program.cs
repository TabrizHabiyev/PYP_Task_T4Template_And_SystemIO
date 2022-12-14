
using System.Data.SqlClient;
using TableAndColumn;

public class Program
{

    public static void Main()
    {
        string connectionString = "Server=TABRIZ\\SQLEXPRESS;Database=NORTHWND;Trusted_Connection=True;MultipleActiveResultSets=true";
        string folderPath = @"C:\Users\tabri\OneDrive\Desktop\PYP_Task_T4Template_And_SystemIO\PYP_Task_T4Template_And_SystemIO\Model";

        List<Table> tables = GetTables(connectionString);
        foreach (Table table in tables)
        {
            table.Columns = GetColumns(connectionString, table.Name);
        }

        if (!Directory.Exists(folderPath))
        {
            Directory.CreateDirectory(folderPath);
        }

        foreach (Table table in tables)
        {
            string filePath = Path.Combine(folderPath, table.Name + ".cs");
            using (StreamWriter writer = new StreamWriter(filePath))
            {
                writer.WriteLine("namespace Northwind.Models");
                writer.WriteLine("{");
                writer.WriteLine("    public class " + table.Name);
                writer.WriteLine("    {");

                foreach (Column column in table.Columns)
                {
                    writer.WriteLine("        public " + column.Type + " " + column.Name + " { get; set; }");
                }

                writer.WriteLine("    }");
                writer.WriteLine("}");
            }
        }
    }

    private static List<Table> GetTables(string connectionString)
    {
        List<Table> tables = new List<Table>();

        using (SqlConnection connection = new SqlConnection(connectionString))
        {
            connection.Open();

            using (SqlCommand command = new SqlCommand("SELECT TABLE_NAME FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_TYPE = 'BASE TABLE'", connection))
            {
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        tables.Add(new Table { Name = reader.GetString(0) });
                    }
                }
            }
        }

        return tables;
    }

    private static List<Column> GetColumns(string connectionString, string tableName)
    {
        List<Column> columns = new List<Column>();

        using (SqlConnection connection = new SqlConnection(connectionString))
        {
            connection.Open();
            using (SqlCommand command = new SqlCommand("SELECT COLUMN_NAME, DATA_TYPE, IS_NULLABLE, COLUMNPROPERTY(OBJECT_ID(TABLE_SCHEMA + '.' + TABLE_NAME), COLUMN_NAME, 'IsIdentity') AS IsIdentity FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = @tableName", connection))
            {
                command.Parameters.AddWithValue("@tableName", tableName);
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        columns.Add(new Column
                        {
                            Name = reader.GetString(0),
                            Type = reader.GetString(1),
                            IsNullable = reader.GetString(2) == "YES",
                            IsPrimaryKey = reader.GetInt32(3) == 1
                        });
                    }
                }
            }
        }

        return columns;
    }

}


