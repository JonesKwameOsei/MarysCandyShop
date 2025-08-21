using Microsoft.Data.Sqlite;
using Spectre.Console;
using System.Text;
using static MarysCandyShop.Enums;

namespace MarysCandyShop;

internal class ProductController
{
    private string ConnectionString { get; } = "Data Source = products.db";

    internal void CreateDatabase()
    {
        try
        {
            if (File.Exists("products.db"))
            {
                return; // Database already exists, no need to create it again
            }
            using var connection = new SqliteConnection(ConnectionString);
            connection.Open();

            using var tableCmd = connection.CreateCommand();
            tableCmd.CommandText = @"
            CREATE TABLE IF NOT EXISTS Products (
                Id INTEGER PRIMARY KEY AUTOINCREMENT,
                Type INTEGER NOT NULL,
                Name TEXT NOT NULL,
                Price REAL NOT NULL,
                CocoaPercentage INTEGER NOT NULL,
                Shape TEXT NOT NULL
            )";
            tableCmd.ExecuteNonQuery();
        }
        catch (SqliteException ex)
        {
            AnsiConsole.MarkupLine($"[red]Error creating database: {ex.Message}[/]");
            Console.WriteLine(UserInterface.divide);
            
        }
    }

    internal List<Product> GetProducts()
    {
        var products = new List<Product>();

        try
        {
            //products.Clear();
            if (!File.Exists(Configuration.docPath))
            {
                return products;
            }

            using (StreamReader reader = new(Configuration.docPath))
            {
                // Discard the header line if it exists
                reader.ReadLine();
                var line = reader.ReadLine();

                while (line != null)
                {
                    string[] parts = line.Split(',');
                    if (parts.Length >= 3)
                    {
                        if (int.Parse(parts[1]) == (int)ProductType.ChocolateBar)
                        {
                            var product = new ChocolateBar(int.Parse(parts[0].Trim()));
                            product.Name = parts[2].Trim();
                            product.Price = decimal.Parse(parts[3].Trim());
                            product.CocoaPercentage = int.Parse(parts[4].Trim());
                            products.Add(product);
                        }
                        else
                        {
                            var product = new Lollipop(int.Parse(parts[0].Trim()));
                            product.Name = parts[2].Trim();
                            product.Price = decimal.Parse(parts[3].Trim());
                            product.Shape = parts[4].Trim();
                            products.Add(product);
                        }
                        line = reader.ReadLine();
                    }
                }
            }
        }
        catch (Exception ex)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine($"Error reading from file: {ex.Message}");
            Console.WriteLine(UserInterface.divide);
            Console.ResetColor();
        }

        return products;
    }

    //internal string HandleAddProduct()
    //{
    //    var id = GetProducts().Count;
    //    if (id == 0)
    //    {
    //        Console.WriteLine("No products found. Adding the first product.");
    //    }
    //    else
    //    {
    //        Console.WriteLine($"Next product ID will be: {id + 1}");
    //    }

    //    Console.WriteLine(UserInterface.divide);

    //    Console.Write("Product name: ");
    //    var name = Console.ReadLine() ?? string.Empty;

    //    if (string.IsNullOrEmpty(name))
    //    {
    //        return "Product name cannot be empty.";
    //    }

    //    Console.WriteLine("Enter product price: ");
    //    if (decimal.TryParse(Console.ReadLine(), out var price) && price > 0)
    //    {
    //        var newProduct = new Product(Helpers.GetNextId())
    //        {

    //            Name = name,
    //            Price = price
    //        };

    //        return AddSingleProduct(newProduct);
    //    }

    //    return "Invalid price entered. Please try again.";
    //}

    // Pure business logic method

    internal string AddSingleProduct(Product product)
    {
        var id = GetProducts().Count;

        try
        {
            using (StreamWriter writer = new StreamWriter(Configuration.docPath, append: true, new UTF8Encoding(false)))
            {
                if (writer.BaseStream.Length <= 3)
                {
                    // Write header if file is empty
                    writer.WriteLine("Id, Type, Name, Price, CocoaPercentage, Shape");
                }

                var csvLine = product.GetProductsForCsv(product.Id);
                writer.WriteLine(csvLine);
            }
            return "Product added successfully";
        }
        catch (Exception ex)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine($"Error saving product to file: {ex.Message}");
            Console.WriteLine(UserInterface.divide);
            Console.ResetColor();
            return "Error saving product. Please try again.";
        }
    }

    internal void AddProducts(List<Product> products)
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

                    var csvLine = product.GetProductsForCsv(product.Id);

                    writer.WriteLine(csvLine);
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

    internal void UpdateProduct(Product product)
    {
        var products = GetProducts();
        if (product == null || products.Count == 0)
        {
            AnsiConsole.MarkupLine("[yellow]No products available to update.[/]");
            return;
        }

        var updatedProducts = products.Where(p => p != null && p.Id != product.Id).ToList();
        updatedProducts.Add(product);

        AddProducts(updatedProducts);
    }

    internal void DeleteProduct(Product product)
    {
        var products = GetProducts();
        if (product == null || products.Count == 0)
        {
            AnsiConsole.MarkupLine("[yellow]No products available to delete.[/]");
            return;
        }

        var updatedProducts = products.Where(p => p != null && p.Id != product.Id).ToList();
        AddProducts(updatedProducts);
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
