using static MarysCandyShop.Enums;

namespace MarysCandyShop;

internal abstract class Product
{
    internal int Id { get; }
    internal string? Name { get; set; }
    internal decimal Price { get; set; }
    internal ProductType Type { get; set; }

    internal Product()
    {

    }

    internal Product(int id)
    {
        Id = id;
    }

    internal abstract string GetProductsForCsv(int Id);

    internal abstract string GetProductForPanel();

    //internal string Name
    //{
    //    get
    //    {
    //        return name.ToUpper();
    //    }
    //    set
    //    {
    //        if (string.IsNullOrWhiteSpace(value))
    //        {
    //            throw new ArgumentException("Product name cannot be empty.");
    //        }
    //        else
    //        {
    //            name = value;
    //        }
    //    }
    //}

    //internal string GetName()
    //{
    //    return name;
    //}

    //internal void SetName(string value)
    //{
    //    name = value;
    //}
}

internal class ChocolateBar : Product
{
    internal int CocoaPercentage { get; set; }

    internal ChocolateBar()
    {
        Type = ProductType.ChocolateBar;
    }

    internal ChocolateBar(int id) : base(id)
    {
        Type = ProductType.ChocolateBar;
    }

    internal override string GetProductsForCsv(int Id)
    {
        return $"{Id}, {(int)Type}, {Name}, {Price}, {CocoaPercentage}";
    }

    internal override string GetProductForPanel()
    {
        return $@"id: {Id}
            Type: {Type}
            Name: {Name}
            Price: {Price:C2}
            Cocoa Percentage: {CocoaPercentage}%";
    }
}

internal class Lollipop : Product
{
    internal string? Shape { get; set; }

    internal Lollipop()
    {
        Type = ProductType.Lollipop;
    }

    internal Lollipop(int id) : base(id)
    {
        Type = ProductType.Lollipop;
    }

    internal override string GetProductsForCsv(int Id)
    {
        return $"{Id}, {(int)Type}, {Name}, {Price}, {Shape}";
    }

    internal override string GetProductForPanel()
    {
        return $@"id: {Id}
            Type: {Type}
            Name: {Name}
            Price: {Price:C2}
            Shape: {Shape}";
    }
}
