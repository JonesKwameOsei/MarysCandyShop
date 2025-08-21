using MarysCandyShop;

// Initialize data and database
if (!File.Exists(Configuration.docPath))
{
    DataSeed.SeedData();
}

var productController = new ProductController();
productController.CreateDatabase();

// Start the application
UserInterface.RuningMenu();

Console.WriteLine();





