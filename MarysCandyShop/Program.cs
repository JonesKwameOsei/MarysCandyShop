using MarysCandyShop;

if (!File.Exists(Configuration.docPath))
{
    DataSeed.SeedData();
}

var productController = new ProductController();
productController.CreateDatabase();

UserInterface.RuningMenu();

Console.WriteLine();





