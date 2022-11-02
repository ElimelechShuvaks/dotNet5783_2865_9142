using DO;

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



    }


}
