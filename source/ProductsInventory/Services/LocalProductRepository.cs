using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using ProductsInventory.Exceptions;
using ProductsInventory.Models;

namespace ProductsInventory.Services;

public class LocalProductRepository(int maxNameLength) : IProductRepository
{
    public IEnumerable<Product> Products => _products;

    private readonly IList<Product> _products = new List<Product>
    {
        new("Apple", 1000, 100),
        new("Banana", 500, 150),
        new("Orange", 250, 175),
    };

    #region Public methods
    public void CreateProduct(string name, int quantity, int price)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(name, nameof(name));

        name = name.Trim();
        if (name.Length > maxNameLength)
            throw new ArgumentException($"The name length should be less than or equal to {maxNameLength}", nameof(name));
        
        if (price <= 0)
            throw new ArgumentException("The price should be greater than 0.", nameof(name));
        
        if (TryGetProductByName(name, out _))
            throw new ProductRepositoryException($"The product {name} already exists.");
        
        _products.Add(new Product(name, quantity, price));
    }

    public Product GetProductByName(string name)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(name, nameof(name));
        
        if (!TryGetProductByName(name, out var product))
            throw new ProductRepositoryException($"The product {name} not found.");

        return product;
    }
    
    public void UpdateProductQuantity(string name, int newQuantity)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(name, nameof(name));
        ArgumentOutOfRangeException.ThrowIfNegative(newQuantity);
        
        if (!TryGetProductByName(name, out var good))
            throw new ProductRepositoryException($"The product {name} not found.");

        good.Quantity = newQuantity;
    }

    public void DeleteProduct(string name)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(name, nameof(name));
        
        if (!TryGetProductByName(name, out var good))
            throw new ProductRepositoryException($"The product {name} not found.");

        _products.Remove(good);
    }
    #endregion

    private bool TryGetProductByName(string name, [NotNullWhen(true)] out Product? good)
    {
        good = _products.FirstOrDefault(x => x.Name.Equals(name.Trim(), StringComparison.InvariantCultureIgnoreCase));
        return good is not null;
    }
}