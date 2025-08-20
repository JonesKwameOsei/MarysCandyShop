namespace MarysCandyShop;

internal static class Helpers
{
    internal static int GetDaysSinceOpening()
    {
        var openingDate = new DateTime(2025, 1, 1);
        var days = DateTime.Now - openingDate;
        return days.Days;
    }

    internal static int GetNextId()
    {
        var products = new ProductController().GetProducts();
        //var products = GetProducts();
        return products.Count > 0 ? products.Max(p => p.Id) + 1 : 1;
    }
}
