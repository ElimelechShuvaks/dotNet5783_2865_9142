using Dal;
using DalApi;
using DO;

//--------------main------------------------------------------------

int choice = 0;
IDal dal = new DalList();

while (true)
{
    Console.WriteLine(@" for which struct you want a test?
 press 0 to Exsit.
 press 1 to check a Product.
 press 2 to check an Order.
 press 3 to check an Order Item.
"
);
    choice = IntTryParse(ref choice);
    switch ((Menu)choice)
    {
        case Menu.Product:
            choisesProduct(dal);
            break;

        case Menu.Order:
            choises_Order(dal);
            break;

        case Menu.OrderItem:
            choises_Orderitem(dal);
            break;

        case Menu.Exsit:
            return;

        default:
            Console.WriteLine("Invalid selection");
            break;
    }
}

//------------------functions---------------------------------------------


//the func PrintList,Product and Order and orderItem.
void PrintList<T>(IEnumerable<T?> items)
{
    foreach (var item in items)
    {
        Console.WriteLine(item);
        Console.WriteLine();
    }
}

//the func TryParse for int.
int IntTryParse(ref int cin)
{
    bool succes = false;
    while (!succes)
    {
        succes = int.TryParse(Console.ReadLine(), out cin);
        if (!succes)
            Console.WriteLine("invail Cin, Please try again");
    }
    return cin;
}

double DoubleTryParse(ref double cin)
{
    bool succes = false;
    while (!succes)
    {
        succes = double.TryParse(Console.ReadLine(), out cin);
        if (!succes)
            Console.WriteLine("invail Cin, Please try again");
    }
    return cin;
}

//the func checkTryParse for DateTime.
DateTime DateTimeTryParse(ref DateTime cin)
{
    bool succes = false;
    while (!succes)
    {
        succes = DateTime.TryParse(Console.ReadLine(), out cin);
        if (!succes)
            Console.WriteLine("invail Cin, Please try again");
    }
    return cin;
}

//the func CinProduct for func Add and updata.
Product CinProduct(Product product)
{

    int intNum = 0;
    double doubleNum = 0;

    Console.WriteLine("cin id:");
    product.ProductId = IntTryParse(ref intNum);

    Console.WriteLine("cin name:");
    product.Name = Console.ReadLine();

    Console.WriteLine("cin price:");
    product.Price = DoubleTryParse(ref doubleNum);

    Console.WriteLine("cin category:");
    product.Category = (Categories)IntTryParse(ref intNum);

    Console.WriteLine("cin instock:");
    product.InStock = IntTryParse(ref intNum);

    return product;
}

//the func CinOrder for func Add and updata.
Order CinOrder(Order order)
{
    int intNum = 0;
    DateTime dateTime = new DateTime();

    Console.WriteLine("cin id:");
    order.Id = IntTryParse(ref intNum);

    Console.WriteLine("cin name:");
    order.CustomerName = Console.ReadLine();

    Console.WriteLine("cin Email:");
    order.CustomerEmail = Console.ReadLine();

    Console.WriteLine("cin Adress:");
    order.CustomerAdress = Console.ReadLine();

    Console.WriteLine("cin OrderDate:");
    order.OrderDate = DateTimeTryParse(ref dateTime);

    Console.WriteLine("cin ShipDate:");
    order.ShipDate = DateTimeTryParse(ref dateTime);

    Console.WriteLine("cin DeliveryDate:");
    order.DeliveryDate = DateTimeTryParse(ref dateTime);

    return order;
}

//function that receive an Order Item from the user.
OrderItem CinOrderItem()
{
    OrderItem ret = new OrderItem();
    int intNum = 0;
    double doubleNum = 0;

    Console.WriteLine("type an Order ProductId");
    ret.ItemId = IntTryParse(ref intNum);

    Console.WriteLine("type a product ProductId");
    ret.ProductId = IntTryParse(ref intNum);

    Console.WriteLine("type an Order ProductId");
    ret.OrderId = IntTryParse(ref intNum);

    Console.WriteLine("type an amount of the product");
    ret.Amount = IntTryParse(ref intNum);

    Console.WriteLine("type the price of a product");
    ret.Price = DoubleTryParse(ref doubleNum);

    return ret;
}

//if you choose Product.
void choisesProduct(IDal dal)
{
    int choiseProduct = 0;

    while (true)
    {
        Console.WriteLine(@" what function you want to test?
 press 0 to Exsit.
 press 1 to Add a Product.
 press 2 to Get a specific Product.
 press 3 to Get an Array of Product Item.
 press 4 to ealete an Product Item.
 press 5 to Updata a Product Item.
"
    );
        choiseProduct = IntTryParse(ref choiseProduct);
        Product product = new Product(); // for add and update functions.
        int intNum = 0; // for add, getand delete functions.

        switch ((Functions)choiseProduct)
        {
            case Functions.ToAdd:

                try
                {
                    intNum = dal.Product.Add(CinProduct(product));
                    Console.WriteLine($"Prints the id :{intNum} of the new product");
                }
                catch (IdExistException ex)
                {
                    Console.WriteLine(ex.Message);
                }
                break;

            case Functions.ToGet:

                try
                {
                    Console.WriteLine("cin the id thet you want to Get:");
                    Console.WriteLine(dal.Product.Get(product=> product?.ProductId== IntTryParse(ref intNum)));
                }
                catch (IdNotExistException ex)
                {
                    Console.WriteLine(ex.Message);
                }
                break;

            case Functions.TogetArray:

                PrintList(dal.Product.GetList());

                break;

            case Functions.ToDel:

                try
                {
                    Console.WriteLine("cin id for product thet you want to Delete:");
                    dal.Product.Delete(IntTryParse(ref intNum));
                }
                catch (IdNotExistException ex)
                {
                    Console.WriteLine(ex.Message);
                }
                break;

            case Functions.ToUpdata:

                try
                {
                    dal.Product.Update(CinProduct(product));
                }
                catch (IdNotExistException ex)
                {
                    Console.WriteLine(ex.Message);
                }
                break;

            case Functions.Exsit:
                return;

            default:
                Console.WriteLine("Invalid selection");
                break;
        }
    }
}

//if you choose Order.
void choises_Order(IDal dal)
{
    int chice_Order = 0;

    while (true)
    {
        Console.WriteLine(@" what function you want to test?
 press 0 to Exsit.
 press 1 to Add an Order.
 press 2 to Get a specific Order.
 press 3 to Get an Array of _orders .
 press 4 to Delete an Order .
 press 5 to Updata an Order.
"
    );
        chice_Order = IntTryParse(ref chice_Order);
        Order order = new Order();// for add and update functions.
        int intNum = 0; // for add, getand delete functions.

        switch ((Functions)chice_Order)
        {
            case Functions.ToAdd:

                try
                {
                    intNum = dal.Order.Add(CinOrder(order));
                    Console.WriteLine("Prints the id of the new order");
                    Console.WriteLine(intNum);
                }
                catch (IdExistException ex)
                {
                    Console.WriteLine(ex.Message);
                }
                break;

            case Functions.ToGet:

                try
                {
                    Console.WriteLine("cin the id order thet you want to Get:");
                    Console.WriteLine(dal.Order.Get(order => order?.Id==IntTryParse(ref intNum)));
                }
                catch (IdNotExistException ex)
                {
                    Console.WriteLine(ex.Message);
                }
                break;

            case Functions.TogetArray:

                IEnumerable<Order?> newList = dal.Order.GetList();
                PrintList(newList);

                break;

            case Functions.ToDel:

                try
                {
                    Console.WriteLine("cin id for order thet you want to Delete:");
                    dal.Order.Delete(IntTryParse(ref intNum));
                }
                catch (IdNotExistException ex)
                {
                    Console.WriteLine(ex.Message);
                }
                break;

            case Functions.ToUpdata:

                try
                {
                    dal.Order.Update(CinOrder(order));
                }
                catch (IdNotExistException ex)
                {
                    Console.WriteLine(ex.Message);
                }
                break;

            case Functions.Exsit:
                return;

            default:
                Console.WriteLine("Invalid selection");
                break;
        }
    }
}

void choises_Orderitem(IDal dal)
{
    int orderChice = 0;
    int intNum = 0;
    int intNum2 = 0;
    while (true)
    {
        Console.WriteLine(@" what function you want to test?
 press 0 to Exsit.
 press 1 to Add an Order Item.
 press 2 to Get a specific Order Item.
 press 3 to Get a specific Order Item by product ProductId and order ProductId.
 press 4 to Get an Array of specific Order.
 press 5 to Get an Array of all Order Items.
 press 6 to Delete an Order Item.
 press 7 to Updata an Order Item.
"
);

        switch ((OrderItemFunctions)IntTryParse(ref orderChice))
        {
            case OrderItemFunctions.Add:

                try
                {
                    Console.WriteLine(dal.OrderItem.Add(CinOrderItem()));
                }
                catch (IdExistException ex)
                {
                    Console.WriteLine(ex.Message);
                }
                break;

            case OrderItemFunctions.Get:

                try
                {
                    Console.WriteLine("type an Order Item ProductId");
                    Console.WriteLine(dal.OrderItem.Get(orderItem => orderItem?.ProductId==IntTryParse(ref intNum)));
                }
                catch (IdNotExistException ex)
                {
                    Console.WriteLine(ex.Message);
                }
                break;

            case OrderItemFunctions.GetBy_2Id:
                try
                {
                    Console.WriteLine("type an Order ProductId");
                    intNum = IntTryParse(ref intNum);

                    Console.WriteLine("type an product ProductId");
                    intNum2 = IntTryParse(ref intNum2);

                    Console.WriteLine(dal.OrderItem.Get(o => o!.Value.ProductId == intNum2 && o.Value.OrderId == intNum));
                }
                catch (IdNotExistException ex)
                {
                    Console.WriteLine(ex.Message);
                }
                break;

            case OrderItemFunctions.GetItemArray:

                Console.WriteLine("type an Order ProductId");
                PrintList(dal.OrderItem.GetList(o => o!.Value.OrderId == IntTryParse(ref intNum)));

                break;

            case OrderItemFunctions.GetArray:

                PrintList(dal.OrderItem.GetList());

                break;

            case OrderItemFunctions.Del:
                try
                {
                    Console.WriteLine("type an Order Item ProductId to Delete");
                    dal.OrderItem.Delete(IntTryParse(ref intNum));
                }
                catch (IdNotExistException ex)
                {
                    Console.WriteLine(ex.Message);
                }
                break;

            case OrderItemFunctions.Updata:
                try
                {
                    dal.OrderItem.Update(CinOrderItem());
                }
                catch (IdNotExistException ex)
                {
                    Console.WriteLine(ex.Message);
                }
                break;

            case OrderItemFunctions.Exsit:
                return;

            default:
                Console.WriteLine("Invalid selection");
                break;
        }
    }
}

//-------------enum--------------------------i 
enum Menu
{
    Exsit,
    Product,
    Order,
    OrderItem
}

enum Functions
{
    Exsit,
    ToAdd,
    ToGet,
    TogetArray,
    ToDel,
    ToUpdata
}

enum OrderItemFunctions
{
    Exsit,
    Add,
    Get,
    GetBy_2Id,
    GetItemArray,
    GetArray,
    Del,
    Updata
}