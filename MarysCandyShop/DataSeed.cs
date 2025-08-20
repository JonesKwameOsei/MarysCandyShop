namespace MarysCandyShop;

internal class DataSeed
{
    internal static void SeedData()
    {
        List<Product> products = new List<Product>
        {
            new ChocolateBar(1)
            {
                Name = "Snickers",
                Price = 1.49m,
                CocoaPercentage = 35
            },
            new ChocolateBar(2)
            {
                Name = "KitKat",
                Price = 1.29m,
                CocoaPercentage = 30
            },
            new ChocolateBar(3)
            {
                Name = "Milka Alpine Milk",
                Price = 1.99m,
                CocoaPercentage = 28
            },
            new ChocolateBar(4)
            {
                Name = "Godiva Dark Chocolate",
                Price = 3.49m,
                CocoaPercentage = 70
            },
            new ChocolateBar(5)
            {
                Name = "Hershey's Cookies 'n' Creme",
                Price = 1.59m,
                CocoaPercentage = 20
            },
            new Lollipop(6)
            {
                Name = "Chupa Chups Strawberry",
                Price = 2.89m,
                Shape = "Round"
            },
            new Lollipop(7)
            {
                Name = "Tootsie Pop Chocolate",
                Price = 0.79m,
                Shape = "Round"
            },
            new Lollipop(8)
            {
                Name = "Dum Dums Cotton Candy",
                Price = 2.59m,
                Shape = "Mini Round"
            },
            new Lollipop(9)
            {
                Name = "Ring Pop Blue Raspberry",
                Price = 1.19m,
                Shape = "Gem/Ring"
            },
            new Lollipop(10)
            {
                Name = "Charms Blow Pop Sour Apple",
                Price = 4.99m,
                Shape = "Round"
            }
        };


        var productController = new ProductController();

        // Assuming AddProducts should take a list of Product, not just names
        productController.AddProducts(products);
    }
}
