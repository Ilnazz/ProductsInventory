using System;

namespace ProductsInventory.Exceptions;

/// <summary>
/// Represents an exception that occurs in the product repository.
/// </summary>
public class ProductRepositoryException : Exception
{
    public ProductRepositoryException() { }

    public ProductRepositoryException(string message) : base(message) { }

    public ProductRepositoryException(string message, Exception inner) : base(message, inner) { }
}