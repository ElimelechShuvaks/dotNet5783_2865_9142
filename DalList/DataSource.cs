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
    public static readonly Random random = new Random(DateTime.Now.Millisecond);

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
        DalProduct dalProduct = new DalProduct();
        Product product = new Product();
        int[] IDarray = new int[10];

        string[][] productNane = new string[5][] {
            new string[] { "Toyota Corolla","Toyota Yaris","Hyundai i40" ,"Hyundai Ioniq","Mazda 3","SEAT Ibiza","SEAT Leon","Skoda Rapid","Skoda Octavia","Ford Focus"},
            new string[] { "Geely Geometric","MG ZS EV", "Hyundai buys","Tesla Model 3" ,"Tesla Model y","Tesla Model s","Tesla Model x","Aiways U5","Kia Niro Plus","Hyundai Ioniq 5"},
            new string[] { "Toyota Rav 4","Toyota Highlander","Mercedes GLB Class","Jeep Compass","Jeep Wrangler" ,"Cadillac Escalade","Volvo XC90","Volvo XC60","Porsche Macan","Porsche Cayenne"},
            new string[] { "Chevrolet Camaro", "Ford Mustang", "Audi A3" ,"Audi TT","Volkswagen - Polo","Nissan GT-R","Alfa Romeo Giulia","mini cooper","Cupra Leon","Maserati MC20"},
            new string[] { "Audi A8", "Volvo S90", "Genesis G80", "BMW 5 Series", "Bentley Flying Spur", "Cadillac CT5", "BYD here", "BMW 7 Series", "Mercedes S", "Audi A7" }
        };

        int[,] ProductPrice = new int[5, 10]{
        { 155138,114527, 150000, 162800, 148500, 103900, 134900, 110000, 178990,144900 },
        { 142900, 144981, 152000, 299469, 371946, 825418, 848590, 162500, 135000, 199900 },
        { 220000, 289900, 349900, 189900, 289000, 899990, 770900, 540000, 577900,715000},
        { 430000, 320000, 368000, 602900,189000, 880000, 699900, 195900, 239000,1880000 },
        { 925000, 469900, 349000,588000, 1745000,359900,299000, 895000, 1450000, 634400 }
        };
        int[] productInStock = { 0, 0, 0, 125, 9, 178, 150, 209, 248, 39, 88, 63, 91, 63, 49, 76, 18 };

        for (int i = 0; i < 10; i++)
        {
            int num = random.Next(4);
            int tempID = random.Next(100000, 999999);
            if (IDarray.Contains(tempID))
                continue;
            IDarray[i] = tempID;
            product.ID = tempID;
            int temprandom = random.Next(11);
            product.Name = productNane[num][temprandom];
            product.Price = ProductPrice[num, temprandom];
            product.Category = (Categories)num;
            product.Image = ".png";
            product.InStock = productInStock[random.Next(productInStock.Length)];

            dalProduct.add(product);
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

        DalOrder dalOrder = new DalOrder();
        Order order = new Order();
        TimeSpan timeSpan;
        for (int i = 0; i < 12; i++) // orders with ship date and delivery date
        {
            order.Id = 0;//garbech, becouse in the add function it receive a ran number
            int temprandom = random.Next(customer_Name.Length);
            order.CustomerName = customer_Name[temprandom];
            order.CustomerEmail = customer_Email[temprandom];
            order.CustomerAdress = customer_Adress[temprandom];
            timeSpan = new TimeSpan(random.Next(2, 30), random.Next(23), random.Next(59));
            order.OrderDate = DateTime.Now - timeSpan;
            timeSpan = new TimeSpan(random.Next(2, 60), random.Next(23), random.Next(59));
            order.ShipDate = order.OrderDate + timeSpan;
            timeSpan = new TimeSpan(random.Next(5), random.Next(23), random.Next(59));
            order.DeliveryDate = order.ShipDate - timeSpan;

            dalOrder.add(order);
        }

        for (int i = 0; i < 4; i++) // orders with ship date and without delivery date
        {
            order.Id = 0;//garbech, becouse in the add function it receive a ran number
            int temprandom = random.Next(customer_Name.Length);
            order.CustomerName = customer_Name[temprandom];
            order.CustomerEmail = customer_Email[temprandom];
            order.CustomerAdress = customer_Adress[temprandom];
            timeSpan = new TimeSpan(random.Next(2, 30), random.Next(23), random.Next(59));
            order.OrderDate = DateTime.Now - timeSpan;
            timeSpan = new TimeSpan(random.Next(2, 60), random.Next(23), random.Next(59));
            order.ShipDate = order.OrderDate + timeSpan;
            order.DeliveryDate = DateTime.MinValue;

            dalOrder.add(order);
        }

        for (int i = 0; i < 4; i++) // orders without ship date and without delivery date
        {
            order.Id = 0;//garbech, becouse in the add function it receive a ran number
            int temprandom = random.Next(customer_Name.Length);
            order.CustomerName = customer_Name[temprandom];
            order.CustomerEmail = customer_Email[temprandom];
            order.CustomerAdress = customer_Adress[temprandom];
            timeSpan = new TimeSpan(random.Next(2, 30), random.Next(24), random.Next(60));
            order.OrderDate = DateTime.Now - timeSpan;
            timeSpan = new TimeSpan(random.Next(2, 60), random.Next(24), random.Next(60));
            order.ShipDate = order.OrderDate + timeSpan;
            timeSpan = new TimeSpan(random.Next(5), random.Next(24), random.Next(60));
            order.DeliveryDate = order.ShipDate - timeSpan;

            dalOrder.add(order);
        }
    }

    public static void s_Initialize_orderitem()
    {
        int indx;
        DalOrderitem dalOrderitem = new DalOrderitem();
        OrderItem orderItem = new OrderItem();
        for (int i = 0; i < 20; i++)
        {
            for (int j = 0; j < random.Next(1, 5); j++)
            {
                indx = random.Next(10);
                orderItem.Id = 0;//garbech, becouse in the add function it receive a ran number
                orderItem.OrderId = Orders[i].Id;
                orderItem.ProductId = Products[indx].ID;
                orderItem.Price = Products[indx].Price;
                orderItem.Amount = random.Next(10);

                dalOrderitem.add(orderItem);
            }
        }
    }
}
