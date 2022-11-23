﻿using BlApi;
using BlImplementation;

namespace BlTest;

internal class Program
{
    static IBl bl = new Bl();


    static void Main(string[] args)
    {
        Console.WriteLine(@"Please select an entity to check.
press 1 to check a product entity,
press 2 to check an order entity,
press 3 to check a cart entity.
press 0 to exit.
");

        switch ((MainMenu)IntTryParse())
        {
            case MainMenu.ProductCheck:
                break;

            case MainMenu.OrderCheck:

                OrderChecking();

                break;

            case MainMenu.CartCheck:
                break;

            default:
                Console.WriteLine("invail input, Please try again");
                break;
        }
    } // End the main.

    //------------------------------- entity checking ---------------------------

    /// <summary>
    /// func to check the order entity functions.
    /// </summary>
    static void OrderChecking()
    {
        Console.WriteLine(@"
Please select a function to check.
press 1 to get an order by order id.
press 2 to update the shiping date.
press 3 to update the delivery date.
press 4 to check the order tracking.
press 5 to get a list of orders.
press 6 to update the change the order.
press 0 to exit.
");
        switch ((OrderMenu)IntTryParse())
        {

            case OrderMenu.Get:
                Console.WriteLine("please enter an order id.");
                Console.WriteLine(bl.Order.GetDetailsOrder(IntTryParse()));
                break;
            case OrderMenu.ShipingUpdate:
                break;
            case OrderMenu.DeliveryUpdate:
                break;
            case OrderMenu.OrderTracking:
                break;
            case OrderMenu.GetList:
                break;
            case OrderMenu.OrderUpdate:
                break;
            case OrderMenu.exit:
                break;
            default:
                break;
        }
    }

    //------------------------- help functions for TryParse ------------------------

    /// <summary>
    /// the func TryParse for int.
    /// </summary>
    /// <returns></returns>
    static int IntTryParse()
    {
        int result = 0;
        bool succes = false;
        while (!succes)
        {
            succes = int.TryParse(Console.ReadLine(), out result);
            if (!succes)
                Console.WriteLine("invail input, Please try again");
        }
        return result;
    }

    /// <summary>
    /// the func TryParse for double.
    /// </summary>
    /// <returns></returns>
    static double DoubleTryParse()
    {
        double result = 0;
        bool succes = false;
        while (!succes)
        {
            succes = double.TryParse(Console.ReadLine(), out result);
            if (!succes)
                Console.WriteLine("invail input, Please try again");
        }
        return result;
    }

    /// <summary>
    /// the func TryParse for DateTime.
    /// </summary>
    /// <returns></returns>
    static DateTime DateTimeTryParse()
    {
        DateTime result = DateTime.MinValue;
        bool succes = false;
        while (!succes)
        {
            succes = DateTime.TryParse(Console.ReadLine(), out result);
            if (!succes)
                Console.WriteLine("invail input, Please try again");
        }
        return result;
    }

    //---------------------- Enums -------------------------------

    enum MainMenu
    {
        exit,
        ProductCheck,
        OrderCheck,
        CartCheck,
    }

    enum ProductMenu
    {

    }

    enum OrderMenu
    {
        exit,
        Get,
        ShipingUpdate,
        DeliveryUpdate,
        OrderTracking,
        GetList,
        OrderUpdate,
    }

    enum CartMenu
    {

    }
}