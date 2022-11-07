using DO;
using Dal;
#nullable disable

int choice;
bool b;
//------------------functions---------------------------------------------

//the func printArray,Product and Order and orderItem.
void printArray<T>(T[] items)
{
    foreach (var item in items)
        Console.WriteLine(item);
}

//the func checkTryParse for int.
int checkTryParse1(ref int Cin)
{
    bool succes = false;
    while (!succes)
    {
        succes = int.TryParse(Console.ReadLine(), out Cin);
        if (!succes)
            Console.WriteLine("invail Cin, Please try again");
    }
    return Cin;
}

//the func checkTryParse for DateTime.
DateTime checkTryParse2(ref DateTime Cin)
{
    bool succes = false;
    while (!succes)
    {
        succes = DateTime.TryParse(Console.ReadLine(), out Cin);
        if (!succes)
            Console.WriteLine("invail Cin, Please try again");
    }
    return Cin;
}

//the func Cinproduct for func add and updata.
Product Cinproduct(Product product)
{

    int id = 0;
    Console.WriteLine("cin id");
    product.ID = checkTryParse1(ref id);
    Console.WriteLine("cin name");
    product.Name = Console.ReadLine();

    double price;
    Console.WriteLine("cin price");
    double.TryParse(Console.ReadLine(), out price);
    product.Price = price;

    int category = 0;
    Console.WriteLine("cin category");
    product.Category = (Categories)checkTryParse1(ref category);

    int instock = 0;
    Console.WriteLine("cin instock");
    product.InStock = checkTryParse1(ref instock);


    return product;
}

//the func Cinorder for func add and updata.
Order Cinorder(Order order)
{
    int id = 0;
    DateTime dateTime = new DateTime();

    Console.WriteLine("cin id");
    order.Id = checkTryParse1(ref id);

    Console.WriteLine("cin name");
    order.CustomerName = Console.ReadLine();

    Console.WriteLine("cin Email");
    order.CustomerEmail = Console.ReadLine();

    Console.WriteLine("cin Adress");
    order.CustomerAdress = Console.ReadLine();

    Console.WriteLine("cin OrderDate");
    order.OrderDate = checkTryParse2(ref dateTime);

    Console.WriteLine("cin ShipDate");
    order.ShipDate = checkTryParse2(ref dateTime);

    Console.WriteLine("cin DeliveryDate");
    order.DeliveryDate = checkTryParse2(ref dateTime);


    return order;
}
//--------------main------------------------------------------------
while (true)
{
    Console.WriteLine(@" for which struct you want a test?
 press 0 to Exsit.
 press 1 to check a Product.
 press 2 to check an Order.
 press 3 to check an Order Item.
"
);
 
    b = int.TryParse(Console.ReadLine(), out choice);
    if (!b)
    {
        Console.WriteLine("invail choise");
        continue;
    }

    switch ((Menu)choice)
    {
        case Menu.Product:
            DalProduct dalProduct = new DalProduct();
            choises_Product(dalProduct);
            break;

        case Menu.Order:
            DalOrder dalOrder = new DalOrder();
            choises_Order(dalOrder);
            break;

        case Menu.OrderItem:
            DalOrderitem dalOrderitem = new DalOrderitem();
            choises_Orderitem(dalOrderitem);
            break;

        case Menu.Exsit:
            return;
    }
}

//if you choose Product.
void choises_Product(DalProduct dalProduct)
{
    int chice_product = 0;

    while (true)
    {
        Console.WriteLine(@" for which struct you want a test?
 press 0 to Exsit.
 press 1 to add a Product.
 press 2 to get a Product specific.
 press 3  togetArray an Product  Item.
 press 4 toDal an Product  Item.
 press 5 for to toUpdata an Product  Item.
"
    );
        chice_product = checkTryParse1(ref chice_product);

        switch ((Functions)chice_product)
        {
            case Functions.toAdd:

                Product product = new Product();
                try
                {
                    int product_id = dalProduct.add(Cinproduct(product));
                    Console.WriteLine(product_id);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
                break;

            case Functions.toGet:
                int idproduct = 0;
                try
                {
                    Console.WriteLine(dalProduct.get(checkTryParse1(ref idproduct)));
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
                break;

            case Functions.togetArray:
                Product[] newarray = dalProduct.getArray();
                printArray<Product>(newarray);
                break;

            case Functions.toDal:
                int del = 0;
                try
                {
                    Console.WriteLine("cin id for delit");
                    dalProduct.del(checkTryParse1(ref del));
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
                break;

            case Functions.toUpdata:
                try
                {
                    Product product1 = new Product();
                    dalProduct.update(Cinproduct(product1));
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
                break;
            case Functions.Exsit:
                return;
            default:
                Console.WriteLine("Invalid selection");
                return;
        }
    }

}


//if you choose Order.
void choises_Order(DalOrder dalOrder)
{
    int chice_Order = 0;

    while (true)
    {
        Console.WriteLine(@" for which struct you want a test?
 press 0 to Exsit.
 press 1 to add a Order.
 press 2 to get a Order specific.
 press 3  togetArray an Order .
 press 4 toDal an Order .
 press 5 for to toUpdata an Order.
"
    );
        chice_Order = checkTryParse1(ref chice_Order);

        switch ((Functions)chice_Order)
        {
            case Functions.toAdd:

                Order order = new Order();
                try
                {
                    int Order_id = dalOrder.add(Cinorder(order));
                    Console.WriteLine(Order_id);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
                break;

            case Functions.toGet:
                int idOrder = 0;
                try
                {
                    Console.WriteLine(dalOrder.get(checkTryParse1(ref idOrder)));
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
                break;

            case Functions.togetArray:
                Order[] newarray = dalOrder.getArray();
                printArray<Order>(newarray);
                break;

            case Functions.toDal:
                int del = 0;
                try
                {
                    Console.WriteLine("cin id for delit");
                    dalOrder.delete(checkTryParse1(ref del));
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
                break;

            case Functions.toUpdata:
                try
                {
                    Order order1 = new Order();
                    dalOrder.update(Cinorder(order1));
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
                break;
            case Functions.Exsit:
                return;
            default:
                Console.WriteLine("Invalid selection");
                return;
        }
    }


}
void choises_Orderitem(DalOrderitem dalOrderitem)
{

}

//-------------enum--------------------------i 
enum Menu
{
    Exsit,
    Product, //Product,
    Order, //Order,
    OrderItem //Order Item.
}
enum Functions
{
    Exsit,
    toAdd,
    toGet,
    togetArray,
    toDal,
    toUpdata
}