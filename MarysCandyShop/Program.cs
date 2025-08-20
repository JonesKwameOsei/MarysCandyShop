using MarysCandyShop;

if (!File.Exists(Configuration.docPath))
{
    DataSeed.SeedData();
}

UserInterface.RuningMenu();

Console.WriteLine();





