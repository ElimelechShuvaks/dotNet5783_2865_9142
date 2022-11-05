
using DO;
namespace Dal;

public class DalProduct
{
    public int add(Product product)
    {
        foreach(Product p in DataSource.Products)
        {
            if (p.ID == product.ID)
                throw new Exception("Product already exsist in data source");
        }

        DataSource.Products[DataSource.Config.counterProduct++] = product;
        return product.ID;
    }

    public Product get(int idNum)
    {
        foreach (Product p in DataSource.Products)
        {
            if (p.ID == idNum)
                return p;
        }
        throw new Exception("Product doesn't exsist in data source");
    }

    public Product[] getArray()
    {
        Product[] ret = new Product[DataSource.Config.counterProduct];
        for (int i = 0; i < DataSource.Config.counterProduct; i++)
        {
            ret[i] = DataSource.Products[i];
        }
        return ret;
    }

    public void del(int idNum)
    {

        int indx;
        for(indx = 0; indx < DataSource.Config.counterProduct; indx++)
        {
            if (DataSource.Products[indx].ID == idNum)
                break;
        }

        if (indx == DataSource.Config.counterProduct)
            throw new Exception("Product doesn't exsist in data source");

        for (int i = indx; i < DataSource.Config.counterProduct - 1; i++)
        {
            DataSource.Products[i] = DataSource.Products[i + 1];
        }
        DataSource.Config.counterProduct--;
    }

    public void update(Product product)
    {
        int indx;
        for (indx = 0; indx < DataSource.Config.counterProduct; indx++)
        {
            if (DataSource.Products[indx].ID == product.ID)
            {
                DataSource.Products[indx] = product;
                break;
            }
        }

        if (indx == DataSource.Config.counterProduct)
            throw new Exception("Product doesn't exsist in data source");
    }

}