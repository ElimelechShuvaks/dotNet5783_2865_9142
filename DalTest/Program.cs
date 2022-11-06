using DO;
using Dal;

int x;
bool b;

while (true)
{
    Console.WriteLine(@" for which struct you want a test?
 press 1 to check a Product.
 press 2 to check an Order.
 press 3 to check an Order Item.
"
);

    b = int.TryParse(Console.ReadLine(), out x);
    if (!b)
    {
        Console.WriteLine("invail choise");
        continue;
    }

    switch ((Menu)x)
    {
        case Menu.Product:

        case Menu.Order:

        case Menu.OrderItem:

        case Menu.Exsit:
            return;
    }
}
