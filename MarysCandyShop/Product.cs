using Microsoft.Data.Sqlite;
using static MarysCandyShop.Enums;

namespace MarysCandyShop;

public abstract class Product
{
    public int Id { get; }
    public string? Name { get; set; }
    public decimal Price { get; set; }
    public ProductType Type { get; set; }

    public Product()
    {

    }

    public Product(int id)
    {
        Id = id;
    }

    public abstract string[] GetColumnsArray(Product product);

    public abstract string GetProductForPanel();

    public abstract string GetInsertQuery();
    public abstract string GetUpdateQuery();

    public abstract void AddParameters(SqliteCommand cmd);

    //public string Name
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

    //public string GetName()
    //{
    //    return name;
    //}

    //public void SetName(string value)
    //{
    //    name = value;
    //}
}

public class ChocolateBar : Product
{
    public int CocoaPercentage { get; set; }

    public ChocolateBar()
    {
        Type = ProductType.ChocolateBar;
    }

    public ChocolateBar(int id) : base(id)
    {
        Type = ProductType.ChocolateBar;
    }

    public override string[] GetColumnsArray(Product product)
    {
        return new string[]
        {
            Id.ToString(),
            Type.ToString(),
            Name ?? string.Empty,
            Price.ToString(),
            CocoaPercentage.ToString(),
            "",
        };
    }

    public override string GetProductForPanel()
    {
        return $@"id: {Id}
            Type: {Type}
            Name: {Name}
            Price: {Price:C2}
            Cocoa Percentage: {CocoaPercentage}%";
    }

    public override string GetInsertQuery()
    {
        return $@"INSERT INTO Products (name, price, type, cocoaPercentage)
                  VALUES (@Name, @Price, @Type, @CocoaPercentage)";
    }

    public override void AddParameters(SqliteCommand cmd)
    {
        cmd.Parameters.AddWithValue("@Name", Name);
        cmd.Parameters.AddWithValue("@Price", Price);
        cmd.Parameters.AddWithValue("@Type", (int)Type);
        cmd.Parameters.AddWithValue("@CocoaPercentage", CocoaPercentage);
    }

    public override string GetUpdateQuery()
    {
        return $"UPDATE Products SET name = @Name, price = @Price, type = 0, cocoaPercentage = @CocoaPercentage WHERE Id = {Id}";
    }
}

public class Lollipop : Product
{
    public string? Shape { get; set; }

    public Lollipop()
    {
        Type = ProductType.Lollipop;
    }

    public Lollipop(int id) : base(id)
    {
        Type = ProductType.Lollipop;
    }

    public override string[] GetColumnsArray(Product product)
    {
        return new string[]
        {
            Id.ToString(),
            Type.ToString(),
            Name ?? string.Empty,
            Price.ToString(),
            "",
           Shape ?? string.Empty,
        };
    }

    public override string GetProductForPanel()
    {
        return $@"id: {Id}
            Type: {Type}
            Name: {Name}
            Price: {Price:C2}
            Shape: {Shape}";
    }

    public override string GetInsertQuery()
    {
        return $@"INSERT INTO Products (name, price, type, shape)
                  VALUES (@Name, @Price, @Type, @Shape)";
    }

    public override void AddParameters(SqliteCommand cmd)
    {
        cmd.Parameters.AddWithValue("@Name", Name);
        cmd.Parameters.AddWithValue("@Price", Price);
        cmd.Parameters.AddWithValue("@Type", (int)Type);
        cmd.Parameters.AddWithValue("@Shape", Shape ?? string.Empty);
    }

    public override string GetUpdateQuery()
    {
        return $"UPDATE Products SET name = @Name, price = @Price, type = 1, shape = @Shape WHERE Id = {Id}";
    }
}
