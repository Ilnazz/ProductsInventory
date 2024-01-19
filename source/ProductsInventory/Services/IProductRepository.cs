using System.Collections.Generic;
using ProductsInventory.Models;

namespace ProductsInventory.Services;

/// <summary>
/// Represents a repository for managing products.
/// </summary>
public interface IProductRepository
{
    /// <summary>
    /// Gets all products.
    /// </summary>
    IEnumerable<Product> Products { get; }
    
    /// <summary>
    /// Creates a new product with the specified name, quantity, and price.
    /// </summary>
    /// <param name="name">The name of the product.</param>
    /// <param name="quantity">The quantity of the product.</param>
    /// <param name="price">The price of the product.</param>
    void CreateProduct(string name, int quantity, int price);

    /// <summary>
    /// Gets a product with the specified name.
    /// </summary>
    /// <param name="name">The name of the product.</param>
    /// <returns>The product with the specified name, or null if not found.</returns>
    Product GetProductByName(string name);

    /// <summary>
    /// Updates the quantity of a product with the specified name.
    /// </summary>
    /// <param name="name">The name of the product.</param>
    /// <param name="newQuantity">The new quantity of the product.</param>
    void UpdateProductQuantity(string name, int newQuantity);

    /// <summary>
    /// Deletes a product with the specified name.
    /// </summary>
    /// <param name="name">The name of the product.</param>
    void DeleteProduct(string name);
}