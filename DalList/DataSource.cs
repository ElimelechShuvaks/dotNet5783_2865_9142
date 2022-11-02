﻿using DO;

namespace Dal;

internal static class DataSource
{
    //Because its Random its class we need to make a =new();.
    public static readonly Random _random_ = new Random(); 

    //the arrys.
    internal static Product[] Products = new Product[50];   

    internal static Order[] Orders = new Order[100];    

    internal static OrderItem[] OrderItems = new OrderItem[200];  
 
    //class Config.
    internal static class Config
    {
        //Counter for amount.
        internal static int counterProduct=0;   

        internal static int counterOrders=0;    

        internal static int counterOrderitem=0;
        //Counter for amount in number run.
        private static int num_runOrder=0;

        private static int num_runOrderitem = 0;

        //get for Counter for amount in number run.
        internal static int getnum_runOrder() { return num_runOrder++; }
        internal static int getnum_runOrderitem() { return num_runOrderitem++; }
    }
    private static void add_Product(Product product) { Products[Config.counterProduct++] = product;  }

    private static void add_Orders(Order orders) { Orders[Config.counterOrders++] = orders; }

    private static void add_Orderitem(OrderItem orderItems) { OrderItems[Config.counterOrderitem++] = orderItems; } 
}
