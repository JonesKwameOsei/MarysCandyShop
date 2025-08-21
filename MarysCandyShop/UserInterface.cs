using Spectre.Console;
using static MarysCandyShop.Enums;

namespace MarysCandyShop;

internal static class UserInterface
{
    internal static readonly string divide = new string('=', 35);

    internal static void RuningMenu()
    {
        var controller = new ProductController();

        var isMenuRunning = true;

        while (isMenuRunning)
        {
            PrintHeader();

            var userChoice = AnsiConsole.Prompt(
                new SelectionPrompt<MainMenuOptions>()
                .Title("What would you like to do?")
                .AddChoices(
                    MainMenuOptions.AddProduct,
                    MainMenuOptions.ViewProducts,
                    MainMenuOptions.ViewSingleProduct,
                    MainMenuOptions.UpdateProduct,
                    MainMenuOptions.DeleteProduct,
                    MainMenuOptions.QuitApplication)
                );

            string result = string.Empty; // Initialize result to avoid CS0165

            switch (userChoice)
            {
                case MainMenuOptions.AddProduct:
                    var product = GetProductInput();
                    controller.AddSingleProduct(product);
                    Console.ReadKey();
                    PromptToViewProducts(controller);

                    break;
                case MainMenuOptions.ViewProducts:
                    //var products = GetProduct();
                    HandleViewProducts(controller);
                    break;
                case MainMenuOptions.ViewSingleProduct:
                    var productChoice = GetProductChoice();
                    ViewProduct(productChoice);
                    break;
                case MainMenuOptions.UpdateProduct:
                    var updatedProduct = GetProductChoice();
                    var updatedResult = GetProductUpdateInput(updatedProduct);
                    controller.UpdateProduct(updatedResult);
                    PromptToViewProducts(controller, "Would you like to view the updated products?");
                    break;
                case MainMenuOptions.DeleteProduct:
                    var deleteProduct = GetProductChoice();
                    controller.DeleteProduct(deleteProduct);
                    PromptToViewProducts(controller, "Would you like to view the remaining products?");
                    break;
                case MainMenuOptions.QuitApplication:
                    result = controller.QuitApplication();
                    break;
                default:
                    result = WarningMessage();
                    break;
            }


            if (result == "QUIT_APPLICATION")
            {
                isMenuRunning = false;
                AnsiConsole.MarkupLine("[green]Thank you for using this app. Goodbye![/]");
            }
            else
            {
                Console.WriteLine(result);
            }

            // Wait for user input before closing the console window
            AnsiConsole.MarkupLine("\n[dim]Press any key to Go Back to Menu[/]");
            Console.ReadLine();
            Console.Clear();
        }
    }

    private static void PromptToViewProducts(ProductController controller, string message = "Would you like to view all products?")
    {
        if (AnsiConsole.Confirm(message))
        {
            HandleViewProducts(controller);
        }
    }

    private static Product GetProductUpdateInput(Product product)
    {
        if (product == null)
        {
            AnsiConsole.MarkupLine("[red]No product selected for update.[/]");
            return null!;
        }

        AnsiConsole.MarkupLine("[yellow]You'll be prompted with the choice to update each property. Press Enter for [green]Yes[/] and N for [red]No[/].[/]");

        product.Name = AnsiConsole.Confirm("Update Name?") ? AnsiConsole.Ask<string>("Enter new product name: ") : product.Name;
        product.Price = AnsiConsole.Confirm("Update Price?") ? AnsiConsole.Ask<decimal>("Enter new product price: ") : product.Price;

        var updateType = AnsiConsole.Confirm("Update product category?");

        var type = ProductType.ChocolateBar;
        if (updateType)
        {
            type = AnsiConsole.Prompt(
            new SelectionPrompt<ProductType>()
            .Title("Product Type:")
            .AddChoices(
                ProductType.ChocolateBar,
                ProductType.Lollipop));
        }

        if (type == ProductType.ChocolateBar)
        {
            Console.Write("Add Cocoa % (0-100): ");
            var cocoaPercentage = int.Parse(Console.ReadLine() ?? string.Empty);

            return new ChocolateBar(product.Id)
            {
                Name = product.Name,
                Price = product.Price,
                CocoaPercentage = cocoaPercentage,
            };
        }

        Console.WriteLine("Enter Shape: ");
        var shape = Console.ReadLine() ?? string.Empty;

        while (!Validation.IsStringValid(shape))
        {
            AnsiConsole.MarkupLine("[red]Shape cannot be empty or have more than 20 characters. Try again![/]");
        }

        return new Lollipop(product.Id)
        {
            Name = product.Name,
            Price = product.Price,
            Shape = shape,
        };
    }

    private static void ViewProduct(Product productChoice)
    {
        var panel = new Panel(productChoice.GetProductForPanel());
        panel.Border = BoxBorder.Rounded;
        panel.Header = new PanelHeader("Product Details");
        panel.Padding = new Padding(2, 2, 2, 2);

        AnsiConsole.Write(panel);
    }

    private static Product GetProductChoice()
    {
        try
        {
            var controller = new ProductController();
            var products = controller.GetProducts();
            if (products == null || products.Count == 0)
            {
                AnsiConsole.MarkupLine("[red bold]No products available. Please add a product.[/]");
                return null!;
            }

            var productOptions = products.Select(p => $"{p.Id} - {p.Name ?? "Unnamed"}").ToArray();
            var option = AnsiConsole.Prompt(new SelectionPrompt<string>()
                .Title("Select a product")
                .AddChoices(productOptions));

            // Extract the ID from the selected option
            var idString = option.Split(" - ")[0].Replace("ID: ", "");
            var selectedId = int.Parse(idString);

            // Find the product by ID
            var product = products.SingleOrDefault(p => p.Id == selectedId);

            if (product == null)
            {
                AnsiConsole.MarkupLine("[red]Selected product not found.[/]");
                return null!;
            }

            return product;
        }
        catch (Exception ex)
        {
            AnsiConsole.MarkupLine($"[red]Error retrieving products: {ex.Message}[/]");
            return null!;
        }

    }

    private static void HandleViewProducts(ProductController controller)
    {
        var products = controller.GetProducts();
        ViewProducts(products);
    }

    internal static void ViewProducts(List<Product> products)
    {
        var table = new Table();
        table.AddColumn("ID");
        table.AddColumn("Name");
        table.AddColumn("Price");
        table.AddColumn("Type");
        table.AddColumn("CocoaPercentage");
        table.AddColumn("Shape");

        if (products == null || products.Count == 0)
        {
            AnsiConsole.MarkupLine("[red]No products available.[/]");
            return;
        }

        foreach (var product in products)
        {
            var columns = product.GetColumnsArray(product);
            table.AddRow(columns);
        }
        AnsiConsole.Write(table);
    }

    private static void PrintHeader()
    {
        var title = "Mary's Candy Shop";
        var divide = new string('=', 35);
        var dateTime = DateTime.Now;
        var daysSinceOpening = Helpers.GetDaysSinceOpening();
        var todaysProfit = 5.5m;
        var isTodaysTargetAchieved = false;

        Console.WriteLine($@"{title}
{divide}
Today's date: {dateTime}
Days since opening: {daysSinceOpening}
Today's profit: ${todaysProfit}
Today's target achieved: {isTodaysTargetAchieved}
{divide}
");
    }

    private static string WarningMessage()
    {
        return "\nInvalid choice. Please select one of the options";
    }

    private static Product GetProductInput()
    {
        Console.Write("Product name: ");
        var name = Console.ReadLine() ?? string.Empty;

        while (!Validation.IsStringValid(name))
        {
            AnsiConsole.MarkupLine("[red]Product name cannot be empty. Please try again.[/]");
            Console.Write("Product name: ");
            name = Console.ReadLine() ?? string.Empty;
        }

        Console.Write("Enter product price: ");
        var priceInput = Console.ReadLine();
        var priceValidation = Validation.IsPriceValid(priceInput);

        // Price validation with retry logic
        while (!priceValidation.IsValid)
        {
            AnsiConsole.MarkupLine($"[red]{priceValidation.ErrorMessage}[/]");
            Console.Write("Enter product price: ");
            priceInput = Console.ReadLine();
            priceValidation = Validation.IsPriceValid(priceInput);
        }

        var nextId = Helpers.GetNextId();

        // Prompt for product type
        var type = AnsiConsole.Prompt(
            new SelectionPrompt<ProductType>()
             .Title("Select product type:")
             .AddChoices(
                 ProductType.ChocolateBar,
                 ProductType.Lollipop)
         );

        if (type == ProductType.ChocolateBar)
        {
            Console.Write("Add Cocoa % (0-100): ");
            var cocoaInput = Console.ReadLine();
            var CocoaInputValidation = Validation.IsCocoaInputValid(cocoaInput);

            while (!CocoaInputValidation.IsValid)
            {
                AnsiConsole.MarkupLine($"[red]{CocoaInputValidation.ErrorMessage}[/]");
                Console.Write("Add Cocoa % (0-100): ");
                cocoaInput = Console.ReadLine();
                CocoaInputValidation = Validation.IsCocoaInputValid(cocoaInput);
            }

            return new ChocolateBar(nextId)
            {
                Name = name,
                Price = priceValidation.Price,
                CocoaPercentage = CocoaInputValidation.CocoaPercentage,
            };
        }

        Console.Write("Add Shape: ");
        var shape = Console.ReadLine() ?? string.Empty;

        while (!Validation.IsStringValid(shape))
        {
            AnsiConsole.MarkupLine("[red]Shape cannot contain Numbers and must be 20 characters or less. Try again![/]");
            Console.Write("Add Shape: ");
            shape = Console.ReadLine() ?? string.Empty;
        }

        return new Lollipop(nextId)
        {
            Name = name,
            Price = priceValidation.Price,
            Shape = shape,
        };
    }
}

