using DO;

namespace Dal;

internal static class DataSource
{
    static DataSource()
    {
        s_Initialize();
    }
    //Because its Random it's class we need to make a = new();. 
    public static readonly Random _random_ = new Random();

    //the arrys.
    internal static Product[] Products = new Product[50];
    internal static Order[] Orders = new Order[100];
    internal static OrderItem[] OrderItems = new OrderItem[200];

    //class "Config".
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
        internal static int getNum_runOrderItem() { return ++num_runOrderitem; }
    }

    private static void add_Product(Product product) { Products[Config.counterProduct++] = product; }
    private static void add_Order(Order order) { Orders[Config.counterOrders++] = order; }
    private static void add_Orderitem(OrderItem orderItem) { OrderItems[Config.counterOrderitem++] = orderItem; }
    private static void add_OrderItem(OrderItem orderItem) { OrderItems[Config.counterOrderitem++] = orderItem; } 

    private static void s_Initialize()
    {
        s_Initialize_product();
        s_Initialize_order();
        s_Initialize_orderitem();
    }
    public static void s_Initialize_product()
    {

        string[] productNane = { };

        
        int[] ProductPrice = { 450000, 670000, 890000, 900000, 510000, 270000, 320000, 250000, 780000, 420000 };
        

        int[] productInStock = { 20, 25, 30, 20, 25, 30, 20, 25, 30, 20 };
        for (int i = 0; i < 10; i++)
        {
            Products[i].ID = i+100000;
            Products[i].Name = productNane[i];
            Products[i].Price = ProductPrice[i];
            Products[i].InStock = productInStock[i];
        }
    }
    public static void s_Initialize_order()
    {
        string[] customer_Name =
{
            "Augustine Fiddeman","Richardo McPeice","Stanton Bilton","Joela Prandoni",
            "Michal Nodin","Caty Rannigan","Gaylor Drewes","Guillemette Purtell",
            "Analiese Prosser","Nestor Oakeby","Farlay Kaas","Evvie Hartell",
            "Evvie Hartell","Margarette Parfrey","Spenser Erdis","Jaymie Wilds",
            "Percival Hardes","Maria Tommis","Andrea Brooksbank","Kellyann Peckett",
            "Wynnie Broadbent","Nadia Leibold","Averell Aizlewood"
 };
        string[] customer_Email =
{
            "gpurtell2w@pcworld.com","aprosser2x@wisc.edu","jprandoni2s@sciencedaily.com","mnodin2t@usgs.gov",
            "crannigan2u@forbes.com","gdrewes2v@walmart.com","gpurtell2w@pcworld.com","gpurtell2w@pcworld.com",
            "fkaas2z@mtv.comr","ehartell30@dion.ne.jp","mparfrey31@seesaa.net","serdis32@flickr.com",
            "jwilds33@meetup.com","phardes34@narod.ru","mtommis35@dyndns.org","abrooksbank36@de.vu",
            "kpeckett37@csmonitor.com","wbroadbent38@tiny.cc","holagen39@yellowbook.com","kchastneyqq@seattletimes.com",
            "jprandoni2s@sciencedaily.com","aashe2m@com.com","mdonalsonqt@arstechnica.com"
 };
        string[] customer_Adress =
{
            "8 Elgar Road","925 Kedzie Court","5614 Derek Lane","0476 Gateway Avenue",
            "05 Blue Bill Park Park","51426 Sutteridge Lane","9186 Schiller Drive","864 Colorado Circle",
            "51426 Sutteridge Lane","3534 Carey Street","8735 Cambridge Circle","1 Thackeray Circlem",
            "199 Manitowish Place","5614 Derek Lane","954 5th Hill","2949 Oak Valley Pass",
            "4906 Service Hill","2756 Main Point","783 Butterfield Circle","5 International Alley",
            "13191 Kim Drive","6282 Calypso Place","7736 Jenna Center"
 };
        for (int i = 0; i < 20; i++)
        {
            Orders[i].Id = Config.counterOrders;//?
            Orders[i].CustomerName = customer_Name[i];
            Orders[i].CustomerEmail = customer_Email[i];
            Orders[i].CustomerAdress = customer_Adress[i];
            //datatime.
        }
    }
    public static void s_Initialize_orderitem()
    {
        for(int i = 0; i < 20; i++)
        {
            int rundom_Order = _random_.Next(0, 20);
            OrderItems[i].Id=Config.counterOrderitem;//?
            OrderItems[i].ProductId= Products[rundom_Order].ID;
            OrderItems[i].OrderId = Orders[rundom_Order].Id;
            OrderItems[i].Amount= _random_.Next(2,4);
            OrderItems[i].Price = Products[rundom_Order].Price;
        }

    }
}
