using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace PL.PO;

public class ProductItem : INotifyPropertyChanged
{
    private int id;
    private string? name;
    private double price;
    private Categories? category;
    private int amount;
    private bool inStock;

    /// <summary>
    /// Name of product.
    /// </summary>
    public int Id { get => id; set { id = value; if (PropertyChanged != null) PropertyChanged(this, new PropertyChangedEventArgs("Id")); } }

    /// <summary>
    /// Name of product.
    /// </summary>
    public string? Name { get => name; set { name = value; if (PropertyChanged != null) PropertyChanged(this, new PropertyChangedEventArgs("Name")); } }

    /// <summary>
    /// Price of product.
    /// </summary>
    public double Price { get => price; set { price = value; if (PropertyChanged != null) PropertyChanged(this, new PropertyChangedEventArgs("Price")); } }

    /// <summary>
    /// Category of product.
    /// </summary>
    public Categories? Category { get => category; set { category = value; if (PropertyChanged != null) PropertyChanged(this, new PropertyChangedEventArgs("Category")); } }

    /// <summary>
    /// Amount of Product Item
    /// </summary>
    public int Amount { get => amount; set { amount = value; if (PropertyChanged != null) PropertyChanged(this, new PropertyChangedEventArgs("Amount")); } }

    /// <summary>
    /// Checks whether there is enough in stock or not.
    /// </summary>
    public bool InStock { get => inStock; set { inStock = value; if (PropertyChanged != null) PropertyChanged(this, new PropertyChangedEventArgs("InStock")); } }

    public static ProductItem BoToPo(BO.ProductItem BoProductItem)
    {
        return new ProductItem
        {
            Id = BoProductItem.Id,
            Name = BoProductItem.Name,
            Price = BoProductItem.Price,
            Category = (Categories)BoProductItem.Category!,
            Amount = BoProductItem.Amount,
            InStock = BoProductItem.InStock,
        };
    }

    public static BO.ProductItem PoToBo(ProductItem BoProductItem)
    {
        return new BO.ProductItem
        {
            Id = BoProductItem.Id,
            Name = BoProductItem.Name,
            Price = BoProductItem.Price,
            Category = (BO.Categories)BoProductItem.Category!,
            Amount = BoProductItem.Amount,
            InStock = BoProductItem.InStock,
        };
    }

    public event PropertyChangedEventHandler? PropertyChanged;
}
