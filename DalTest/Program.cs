using DO;
using Dal;
#nullable disable

int choice = 0;

//------------------functions---------------------------------------------

//the func printArray,Product and Order and orderItem.
void printArray<T>(T[] items)
{
    foreach (var item in items)
    {
        Console.WriteLine(item);
        Console.WriteLine();    
    }
}

//the func TryParse for int.
int intTryParse(ref int Cin)
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

double doubleTryParse(ref double Cin)
{
    bool succes = false;
    while (!succes)
    {
        succes = double.TryParse(Console.ReadLine(), out Cin);
        if (!succes)
            Console.WriteLine("invail Cin, Please try again");
    }
    return Cin;
}

//the func checkTryParse for DateTime.
DateTime dateTimeTryParse(ref DateTime Cin)
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
    Console.WriteLine("cin id:");
    product.ID = intTryParse(ref id);

    Console.WriteLine("cin name:");
    product.Name = Console.ReadLine();

    double price = 0;
    Console.WriteLine("cin price:");
    product.Price = doubleTryParse(ref price);

    int category = 0;
    Console.WriteLine("cin category:");
    product.Category = (Categories)intTryParse(ref category);



    int instock = 0;
    Console.WriteLine("cin instock:");
    product.InStock = intTryParse(ref instock);


    return product;
}

//the func Cinorder for func add and updata.
Order Cinorder(Order order)
{
    int id = 0;
    DateTime dateTime = new DateTime();

    Console.WriteLine("cin id:");
    order.Id = intTryParse(ref id);

    Console.WriteLine("cin name:");
    order.CustomerName = Console.ReadLine();

    Console.WriteLine("cin Email:");
    order.CustomerEmail = Console.ReadLine();

    Console.WriteLine("cin Adress:");
    order.CustomerAdress = Console.ReadLine();

    Console.WriteLine("cin OrderDate:");
    order.OrderDate = dateTimeTryParse(ref dateTime);

    Console.WriteLine("cin ShipDate:");
    order.ShipDate = dateTimeTryParse(ref dateTime);

    Console.WriteLine("cin DeliveryDate:");
    order.DeliveryDate = dateTimeTryParse(ref dateTime);


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
    choice = intTryParse(ref choice);
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

        default:
            Console.WriteLine("Invalid selection");
            break;
    }
}

//if you choose Product.
void choises_Product(DalProduct dalProduct)
{
    int chice_product = 0;

    while (true)
    {
        Console.WriteLine(@" what function you want to test?
 press 0 to Exsit.
 press 1 to add a Product.
 press 2 to get a specific Product.
 press 3 to get an Array of Product Item.
 press 4 to ealete an Product Item.
 press 5 to Updata a Product Item.
"
    );
        chice_product = intTryParse(ref chice_product);

        switch ((Functions)chice_product)
        {
            case Functions.ToAdd:

                Product product = new Product();
                try
                {
                    int product_id = dalProduct.add(Cinproduct(product));
                    Console.WriteLine("Prints the id of the new product");
                    Console.WriteLine(product_id);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
                break;

            case Functions.ToGet:
                int idproduct = 0;
                try
                {
                    Console.WriteLine("cin the id thet you want to get:");
                    Console.WriteLine(dalProduct.get(intTryParse(ref idproduct)));
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
                break;

            case Functions.TogetArray:
                Product[] newArray = dalProduct.getArray();
                printArray<Product>(newArray);
                break;

            case Functions.ToDal:
                int del = 0;
                try
                {
                    Console.WriteLine("cin id for product thet you want to delete:");
                    dalProduct.delete(intTryParse(ref del));
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
                break;

            case Functions.ToUpdata:
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
                break;
        }
    }

}



//if you choose Order.
void choises_Order(DalOrder dalOrder)
{
    int chice_Order = 0;

    while (true)
    {
        Console.WriteLine(@" what function you want to test?
 press 0 to Exsit.
 press 1 to add an Order.
 press 2 to get a specific Order.
 press 3 to get an Array of Orders .
 press 4 to Delete an Order .
 press 5 to Updata an Order.
"
    );
        chice_Order = intTryParse(ref chice_Order);

        switch ((Functions)chice_Order)
        {
            case Functions.ToAdd:

                Order order = new Order();
                try
                {
                    int Order_id = dalOrder.add(Cinorder(order));
                    Console.WriteLine("Prints the id of the new order");
                    Console.WriteLine(Order_id);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
                break;

            case Functions.ToGet:
                int idOrder = 0;
                try
                {
                    Console.WriteLine("cin the id order thet you want to get:");
                    Console.WriteLine(dalOrder.get(intTryParse(ref idOrder)));
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
                break;

            case Functions.TogetArray:
                Order[] newarray = dalOrder.getArray();
                printArray<Order>(newarray);
                break;

            case Functions.ToDal:
                int del = 0;
                try
                {
                    Console.WriteLine("cin id for order thet you want to delete:");
                    dalOrder.delete(intTryParse(ref del));
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
                break;

            case Functions.ToUpdata:
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
                break;
        }
    }


}

void choises_Orderitem(DalOrderitem dalOrderitem)
{
    int orderChice = 0;
    int num = 0;
    int num1 = 0;
    while (true)
    {
        Console.WriteLine(@" what function you want to test?
 press 0 to Exsit.
 press 1 to add an Order Item.
 press 2 to get a specific Order Item.
 press 3 to get a specific Order Item by product ID and order ID.
 press 4 to get an Array of specific Order.
 press 5 to get an Array of all Order Items.
 press 6 to Delete an Order Item.
 press 7 to Updata an Order Item.
"
);

        switch ((OrderItemFunctions)intTryParse(ref orderChice))
        {
            case OrderItemFunctions.Exsit:
                return;

            case OrderItemFunctions.Add:
                Console.WriteLine(dalOrderitem.add(receiveOrderItemData()));
                break;

            case OrderItemFunctions.Get:
                try
                {
                    Console.WriteLine("type an Order Item ID");
                    Console.WriteLine(dalOrderitem.getById(intTryParse(ref num)));
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
                break;

            case OrderItemFunctions.GetBy_2Id:
                try
                {
                    Console.WriteLine("type an Order ID");
                    num = intTryParse(ref num);
                    Console.WriteLine("type an product ID");
                    num1 = intTryParse(ref num1);
                    Console.WriteLine(dalOrderitem.getBy_2Id(oId: num, pId: num1));
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
                break;

            case OrderItemFunctions.GetItemArray:
                Console.WriteLine("type an Order ID");
                printArray(dalOrderitem.getItemArray(intTryParse(ref num)));
                break;

            case OrderItemFunctions.getArray:
                printArray(dalOrderitem.getarry_all());
                break;

            case OrderItemFunctions.Dal:
                try
                {
                    Console.WriteLine("type an Order Item ID to delete");
                    dalOrderitem.delete(intTryParse(ref num));
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
                break;

            case OrderItemFunctions.Updata:
                try
                {
                    dalOrderitem.update(receiveOrderItemData());
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
                break;

            default:
                Console.WriteLine("Invalid selection");
                break;
        }
    }
}

//function that receive an Order Item from the user.
OrderItem receiveOrderItemData()
{
    OrderItem ret = new OrderItem();
    int num = 0;
    double numD = 0;
    Console.WriteLine("type an Order ID");
    ret.Id = intTryParse(ref num);
    Console.WriteLine("type a product ID");
    ret.ProductId = intTryParse(ref num);
    Console.WriteLine("type an Order ID");
    ret.OrderId = intTryParse(ref num);
    Console.WriteLine("type an amount of the product");
    ret.Amount = intTryParse(ref num);
    Console.WriteLine("type the price of a product");
    ret.Price = doubleTryParse(ref numD);
    return ret;
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
    ToDal,
    ToUpdata
}

enum OrderItemFunctions
{
    Exsit,
    Add,
    Get,
    GetBy_2Id,
    GetItemArray,
    getArray,
    Dal,
    Updata
}