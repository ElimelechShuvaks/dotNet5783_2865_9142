using DO;
namespace Dal;

/// <summary>
/// Data source of Products, Orders, and Order Items.
/// </summary>
internal static class DataSource
{
    static DataSource()
    {
        s_Initialize();
    }
    /// <summary>
    /// a random variable to use when it's needed.
    /// </summary>
    public static readonly Random random = new Random();

    /// <summary>
    /// Products array in Data source.
    /// </summary>
    internal static Product[] Products = new Product[50];
    /// <summary>
    /// Orders array in Data source.
    /// </summary>
    internal static Order[] Orders = new Order[100];
    /// <summary>
    /// Order Items array in Data source.
    /// </summary>
    internal static OrderItem[] OrderItems = new OrderItem[200];

    //class "Config".
    internal static class Config
    {
        //Counter for amount.
        private static int counterProduct = 0;
        private static int counterOrders = 0;
        private static int counterOrderitem = 0;

        internal static int CounterProduct { get => counterProduct; set => counterProduct = value; }
        internal static int CounterOrders { get => counterOrders; set => counterOrders = value; }
        internal static int CounterOrderitem { get => counterOrderitem; set => counterOrderitem = value; }

        //Counter for amount in number run.
        private static int num_runOrder = 0;
        private static int num_runOrderitem = 0;

        public static int Num_runOrder { get => ++num_runOrder; }
        public static int Num_runOrderitem { get => ++num_runOrderitem; }
    }

    private static void s_Initialize()
    {
        s_Initialize_product();
        s_Initialize_order();
        s_Initialize_orderitem();
    }

    public static void s_Initialize_product()
    {
        Product product = new Product();   
        int[] IDarray = new int[10];

        string[][] productNane = new string[5][];

        int[,] ProductPrice = new int[5, 10]{
        { 450000, 670000, 890000, 900000, 510000, 270000, 320000, 250000, 780000, 420000 },
        { 450000, 670000, 890000, 900000, 510000, 270000, 320000, 250000, 780000, 420000 },
        { 450000, 670000, 890000, 900000, 510000, 270000, 320000, 250000, 780000, 420000 },
        { 450000, 670000, 890000, 900000, 510000, 270000, 320000, 250000, 780000, 420000 },
        { 450000, 670000, 890000, 900000, 510000, 270000, 320000, 250000, 780000, 420000 }
        };
        int[] productInStock = { 0, 0, 0, 125, 9, 178, 150, 209, 248, 39, 88, 63, 91, 63, 49, 76, 18 };

        int num = random.Next(4);
        for (int i = 0; i < 10; i++)
        {
            int tempID = random.Next(100000, 999999);
            if (IDarray.Contains(tempID))
                continue;
            IDarray[i] = tempID;
            product.ID = tempID;
            product.Name = productNane[num][random.Next(10)];//???????????צריך לראות שהמספר ברנדום מעודכן
            product.Price = ProductPrice[num, random.Next(10)];//???????????צריך לראות שהמספר ברנדום מעודכן
            product.Category = (Categories)num;
            product.Image = ".png";
            product.InStock = productInStock[random.Next(productInStock.Length)];
            
            Products[i] = product;
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

        Order order = new Order();
        TimeSpan timeSpan;
        for (int i = 0; i < 12; i++) // orders with ship date and delivery date
        {
            order.Id = Config.Num_runOrder;
            order.CustomerName = customer_Name[random.Next(customer_Name.Length)];
            order.CustomerEmail = customer_Email[random.Next(customer_Email.Length)];
            order.CustomerAdress = customer_Adress[random.Next(customer_Adress.Length)];
            timeSpan = new TimeSpan(random.Next(2, 30), random.Next(23), random.Next(59));
            order.OrderDate = DateTime.Now - timeSpan;
            timeSpan = new TimeSpan(random.Next(2, 60), random.Next(23), random.Next(59));
            order.ShipDate = order.OrderDate + timeSpan;
            timeSpan = new TimeSpan(random.Next(5), random.Next(23), random.Next(59));
            order.DeliveryDate = order.ShipDate - timeSpan;

            Orders[i] = order;
        }

        for (int i = 0; i < 4; i++) // orders with ship date and without delivery date
        {
            order.Id = Config.Num_runOrder;
            order.CustomerName = customer_Name[random.Next(customer_Name.Length)];
            order.CustomerEmail = customer_Email[random.Next(customer_Email.Length)];
            order.CustomerAdress = customer_Adress[random.Next(customer_Adress.Length)];
            timeSpan = new TimeSpan(random.Next(2, 30), random.Next(23), random.Next(59));
            order.OrderDate = DateTime.Now - timeSpan;
            timeSpan = new TimeSpan(random.Next(2, 60), random.Next(23), random.Next(59));
            order.ShipDate = order.OrderDate + timeSpan;
            order.DeliveryDate = DateTime.MinValue;

            Orders[i] = order;
        }

        for (int i = 0; i < 4; i++) // orders without ship date and without delivery date
        {
            order.Id = Config.Num_runOrder;
            order.CustomerName = customer_Name[random.Next(customer_Name.Length)];
            order.CustomerEmail = customer_Email[random.Next(customer_Email.Length)];
            order.CustomerAdress = customer_Adress[random.Next(customer_Adress.Length)];
            timeSpan = new TimeSpan(random.Next(2, 30), random.Next(24), random.Next(60));
            order.OrderDate = DateTime.Now - timeSpan;
            timeSpan = new TimeSpan(random.Next(2, 60), random.Next(24), random.Next(60));
            order.ShipDate = order.OrderDate + timeSpan;
            timeSpan = new TimeSpan(random.Next(5), random.Next(24), random.Next(60));
            order.DeliveryDate = order.ShipDate - timeSpan;

            Orders[i] = order;
        }
    }

    public static void s_Initialize_orderitem()
    {
        int indx;
        OrderItem orderItem = new OrderItem();  
        for(int i = 0; i < 20; i++)
        {
            for(int j = 0; j < random.Next(4); j++)
            {
                indx = random.Next(10);
                orderItem.Id = Config.Num_runOrderitem;
                orderItem.OrderId = Orders[i].Id;
                orderItem.ProductId = Products[indx].ID;
                orderItem.Price = Products[indx].Price;
                orderItem.Amount = random.Next(10);
            }
        }
    }
}
