using DO;
using Dal;

#nullable disable

int choice = 0;

void printArray<T>(T[] items)
{
    foreach (var item in items)
    {
        Console.WriteLine(item);
        Console.WriteLine();
    }
}

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
            break;
    }
}

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
        chice_product = intTryParse(ref chice_product);

        switch ((Functions)chice_product)
        {
            case Functions.toAdd:

                Product product = new Product();
                int id = 0;
                Console.WriteLine("cin id");
                product.ID = intTryParse(ref id);
                Console.WriteLine("cin name");
                product.Name = Console.ReadLine();

                double price;
                Console.WriteLine("cin price");
                double.TryParse(Console.ReadLine(), out price);
                product.Price = price;

                int category = 0;
                Console.WriteLine("cin category");
                product.Category = (Categories)intTryParse(ref category);

                int instock = 0;
                Console.WriteLine("cin instock");
                product.InStock = intTryParse(ref instock);

                try
                {
                    int product_id = dalProduct.add(product);
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
                    Console.WriteLine(dalProduct.get(intTryParse(ref idproduct)));
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
                    dalProduct.del(intTryParse(ref del));
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
                    dalProduct.update(product1);

                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
                break;
            case Functions.Exsit:
                return;
            default:
                return;
        }
    }

}

void choises_Order(DalOrder dalOrder)
{

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
                    dalOrderitem.add(receiveOrderItemData());
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
                break;

            default:
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
    Console.WriteLine("type a temp Order ID");
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
    toAdd,
    toGet,
    togetArray,
    toDal,
    toUpdata
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