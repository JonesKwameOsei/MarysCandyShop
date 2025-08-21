using Microsoft.Data.Sqlite;
using Spectre.Console;
using static MarysCandyShop.Enums;

namespace MarysCandyShop;

public interface IProductsController
{
    void CreateDatabase();

    List<Product> GetProducts();

    void AddSingleProduct(Product product);

    void AddProducts(List<Product> products);

    void UpdateProduct(Product product);

    void DeleteProduct(Product product);
}

public class ProductController : IProductsController
{
    private string ConnectionString { get; } = "Data Source = products.db";

    public void CreateDatabase()
    {
        try
        {
            if (File.Exists("products.db"))
            {
                return;
            }
            using var connection = new SqliteConnection(ConnectionString);
            connection.Open();

            using var tableCmd = connection.CreateCommand();
            tableCmd.CommandText = @"
            CREATE TABLE Products (
                Id INTEGER PRIMARY KEY AUTOINCREMENT,
                Type INTEGER NOT NULL,
                Name TEXT NOT NULL,
                Price REAL NOT NULL,
                CocoaPercentage INTEGER NULL,
                Shape TEXT Null
            )";
            tableCmd.ExecuteNonQuery();
        }
        catch (SqliteException ex)
        {
            AnsiConsole.MarkupLine($"[red]Error creating database: {ex.Message}[/]");
            Console.WriteLine(UserInterface.divide);

        }
    }

    public List<Product> GetProducts()
    {
        var products = new List<Product>();

        try
        {
            using var connection = new SqliteConnection(ConnectionString);
            connection.Open();

            var selectCmd = connection.CreateCommand();
            selectCmd.CommandText = "SELECT * FROM Products";

            using var reader = selectCmd.ExecuteReader();

            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    if (reader.GetInt32(1) == (int)ProductType.ChocolateBar)
                    {
                        products.Add(new ChocolateBar(reader.GetInt32(0))
                        {
                            Name = reader.GetString(2),
                            Price = reader.GetDecimal(3),
                            CocoaPercentage = reader.IsDBNull(4) ? 0 : reader.GetInt32(4)
                        });
                    }
                    else
                    {
                        products.Add(new Lollipop(reader.GetInt32(0))
                        {
                            Name = reader.GetString(2),
                            Price = reader.GetDecimal(3),
                            Shape = reader.IsDBNull(5) ? string.Empty : reader.GetString(5)
                        });
                    }
                }
            }
        }
        catch (Exception ex)
        {
            AnsiConsole.MarkupLine($"[yellow]Error reading from file: {ex.Message}[/]");
            Console.WriteLine(UserInterface.divide);
        }

        return products;
    }

    public void AddSingleProduct(Product product)
    {
        try
        {
            using var connection = new SqliteConnection(ConnectionString);
            connection.Open();
            using var insertCmd = connection.CreateCommand();

            insertCmd.CommandText = product.GetInsertQuery();
            product.AddParameters(insertCmd);

            insertCmd.ExecuteNonQuery();

            AnsiConsole.MarkupLine("[green]Added product successfully![/]");
        }
        catch (Exception ex)
        {
            AnsiConsole.MarkupLine($"[yellow]Error saving product to database: {ex.Message}[/]");
            Console.WriteLine(UserInterface.divide);
        }
    }

    public void AddProducts(List<Product> products)
    {

        // Products added to the file
        try
        {
            using (StreamWriter writer = new StreamWriter(Configuration.docPath, append: false))
            {
                if (writer.BaseStream.Length <= 3)
                {
                    // Write header if file is empty
                    writer.WriteLine("Id, Type, Name, Price, CocoaPercentage, Shape");
                }

                foreach (var product in products)
                {

                    //var csvLine = product.GetProductsForCsv(product.Id);

                    //writer.WriteLine(csvLine);
                }
            }
            Console.WriteLine("Products saved");
        }
        catch (Exception ex)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine($"Error saving products to file: {ex.Message}");
            Console.WriteLine(UserInterface.divide);
            Console.ResetColor();
        }
    }

    public void UpdateProduct(Product product)
    {
        try
        {
            using var connection = new SqliteConnection(ConnectionString);
            connection.Open();
            using var updateCmd = connection.CreateCommand();

            updateCmd.CommandText = product.GetUpdateQuery();
            product.AddParameters(updateCmd);

            updateCmd.ExecuteNonQuery();

            AnsiConsole.MarkupLine("[green]Updateded product successfully![/]");
        }
        catch (Exception ex)
        {
            AnsiConsole.MarkupLine($"[yellow]Error updating product to database: {ex.Message}[/]");
            Console.WriteLine(UserInterface.divide);
        }
    }

    public void DeleteProduct(Product product)
    {
        try
        {
            // Open the database connection
            using var connection = new SqliteConnection(ConnectionString);
            connection.Open();

            // Create a command to delete the product
            using var deleteCmd = connection.CreateCommand();
            deleteCmd.CommandText = $"DELETE FROM Products WHERE Id = {product.Id}";

            deleteCmd.ExecuteNonQuery();

            AnsiConsole.MarkupLine($"[green]Successfully deleted product: {product.Id} - {product.Name}[/]");
        }
        catch (SqliteException ex)
        {
            AnsiConsole.MarkupLine($"[yellow]Error deleting product: {ex.Message}[/]");
            Console.WriteLine(UserInterface.divide);
        }
    }

    internal string QuitApplication()
    {
        Console.ForegroundColor = ConsoleColor.Red;
        var message = "QUIT_APPLICATION";
        Console.ResetColor();
        return message;
    }

    internal string HandleQuitApplication()
    {
        return QuitApplication();
    }
}
