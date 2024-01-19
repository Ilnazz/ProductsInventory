namespace ProductsInventory.Models;

/// <summary>
/// Represents a product in the inventory.
/// </summary>
public class Product(string name, int quantity, int price)
{
    /// <summary>
    /// Gets or sets the name of the product.
    /// </summary>
    public string Name { get; set; } = name;

    /// <summary>
    /// Gets or sets the quantity of the product.
    /// </summary>
    public int Quantity { get; set; } = quantity;

    /// <summary>
    /// Gets or sets the price of the product.
    /// </summary>
    public int Price { get; set; } = price;
}