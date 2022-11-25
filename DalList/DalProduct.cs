using DO;
using DalApi;
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

    public Product Get(int idNum)
    {
        int index = existProduct(idNum);

        if (index == -1)
        {
            OtherFunctions.exceptionNotFound("product", idNum);
        }

        return DataSource._products[index];
    }

    public IEnumerable<Product> GetList()
    {
        return DataSource._products.Select(product => product);
    }

    public void Delete(int idNum)
    {
        Product? product = Get(idNum);

        if (product != null)
        {
            DataSource._products.Remove(product.Value);
            return;
        }
        OtherFunctions.exceptionNotFound("product", idNum);
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
        return DataSource._products.FindIndex(product => product.ProductId == id);
    }
}