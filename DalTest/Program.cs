
using DO;
using Dal;

#nullable disable

int choice;
bool b;

void printArray<T>(T[] items)
{
    foreach (var item in items)
        Console.WriteLine(item);
}
int checkTryParse(ref int Cin)
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
        Console.WriteLine("invail choise");

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
        chice_product = checkTryParse(ref chice_product);

        switch ((Functions)chice_product)
        {
            case Functions.toAdd:

                Product product = new Product();
                int id = 0;
                product.ID = checkTryParse(ref id);

                product.Name = Console.ReadLine();

                double price;
                double.TryParse(Console.ReadLine(), out price);
                product.Price = price;

                int category = 0;
                product.Category = (Categories)checkTryParse(ref category);

                int instock = 0;
                product.InStock = checkTryParse(ref instock);

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
                    Console.WriteLine(dalProduct.get(checkTryParse(ref idproduct)));
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
                    dalProduct.del(checkTryParse(ref del));
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

}
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