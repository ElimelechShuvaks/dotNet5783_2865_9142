using DO;

namespace Dal;

internal static class DataSource
{
    //Because its Random it's class we need to make a = new();.
    public static readonly Random _random_ = new Random(); 

    //the arrys.
    public static Product[] Products = new Product[50];   
    internal static Order[] Orders = new Order[100];    
    internal static OrderItem[] OrderItems = new OrderItem[200]; 

 
    //class Config.
    internal static class Config
    {
        //Counter for amount.
        internal static int counterProduct = 0;   
        internal static int counterOrders = 0;    
        internal static int counterOrderitem = 0;

        //Counter for amount in number run.
        private static int num_runOrder = 0;
        private static int num_runOrderitem = 0;

        //get for Counter for amount in number run.
        internal static int getNum_runOrder() { return ++num_runOrder; }
        internal static int getNum_runOrderitem() { return ++num_runOrderitem; }
    }

    private static void add_Product(Product product) { Products[Config.counterProduct++] = product;  }
    private static void add_Order(Order order) { Orders[Config.counterOrders++] = order; }
    private static void add_Orderitem(OrderItem orderItem) { OrderItems[Config.counterOrderitem++] = orderItem; } 

}
