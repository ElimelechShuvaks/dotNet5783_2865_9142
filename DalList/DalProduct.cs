using DalApi;
using DO;

namespace Dal;

internal class DalProduct : IProduct
{
    public int Add(Product newProduct)
    {
        int index = existProduct(newProduct.ProductId);

        if (index != -1)
        {
            OtherFunctions.exceptionFound("product", newProduct.ProductId);
        }

        DataSource._products.Add(newProduct);
        return newProduct.ProductId;
    }

    public void Delete(int idNum)
    {
        DataSource._products.Remove(Get(productFunc => productFunc?.ProductId == idNum));
    }

    public void Update(Product product)
    {
        int index = existProduct(product.ProductId);

        if (index != -1)
        {
            DataSource._products[index] = product;
            return;
        }

        OtherFunctions.exceptionNotFound("product", product.ProductId);
    }

    private int existProduct(int id)
    {
        return DataSource._products.FindIndex(product => product?.ProductId == id);
    }

    public Product Get(Func<Product?, bool>? func)
    {
        Product? product = DataSource._products.FirstOrDefault(func!);
        if (product is not null)
        {
            return (Product)product;
        }
        throw new EntityNotExistException("There is no product that meets these conditions in the database");
    }

    public IEnumerable<Product?> GetList(Func<Product?, bool>? func = null)
    {
        bool check = func is null;
        return check ? DataSource._products.Select(product => product) : DataSource._products.Where(func!);
    }

}