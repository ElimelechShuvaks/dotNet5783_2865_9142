//using BO;
//using System;
//using System.Collections.Generic;
//using System.ComponentModel;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

//namespace PL.PO;

//public class Cart : INotifyPropertyChanged
//{
//    /// <summary>
//    /// CustomerName of order
//    /// </summary>
//    public string? CustomerName { get; set; }

//    /// <summary>
//    /// CustomerEmail of order
//    /// </summary>
//    public string? CustomerEmail { get; set; }

//    /// <summary>
//    /// CustomerAdress of order
//    /// </summary>
//    public string? CustomerAdress { get; set; }

//    private List<BO.OrderItem>? items;
//    /// <summary>
//    /// Items of Cart
//    /// </summary>
//    public List<BO.OrderItem>? Items
//    {
//        get => items;
//        set { items = value; if (PropertyChanged != null) PropertyChanged(this, new PropertyChangedEventArgs("Items")); }
//    }

//    /// <summary>
//    /// TotalPrice of order item
//    /// </summary>
//    public double TotalPrice { get; set; }

//    public static Cart BoToPo(BO.Cart BoCart)
//    {
//        return new Cart
//        {
//            CustomerName = BoCart.CustomerName,
//            CustomerEmail = BoCart.CustomerEmail,
//            CustomerAdress = BoCart.CustomerAdress,
//            TotalPrice = BoCart.TotalPrice,
//            Items = BoCart.Items
//        };
//    }

//    public static BO.Cart PoToBo(Cart BoCart)
//    {
//        return new BO.Cart
//        {
//            CustomerName = BoCart.CustomerName,
//            CustomerEmail = BoCart.CustomerEmail,
//            CustomerAdress = BoCart.CustomerAdress,
//            TotalPrice = BoCart.TotalPrice,
//            Items = BoCart.Items
//        };
//    }

//    public event PropertyChangedEventHandler? PropertyChanged;
//}
