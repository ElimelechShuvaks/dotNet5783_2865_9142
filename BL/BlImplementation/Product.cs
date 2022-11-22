using BO;
using DalApi;
namespace BlImplementation;

internal class Product : BlApi.IProduct
{
    private IDal dal = new Dal.DalList();
    private DO.Product product = new();

    public IEnumerable<ProductForList> ProductListRequest()
    {
        IEnumerable<DO.Product> products = dal.Product.GetList();
        List<ProductForList> newProductForList = new List<ProductForList>(products.Count());

        foreach (DO.Product p in products)
        {
            ProductForList productForList = new ProductForList();
            productForList.ID = p.ID;
            productForList.Name = p.Name;
            productForList.Category = (Categories)p.Category;
            productForList.Price = p.Price;

            newProductForList.Add(productForList);
        }
        return newProductForList;
    }
    public BO.Product ProductDetailsManger(int idProduct)
    {
        if (idProduct > 0)
        {
            try
            {
                product = dal.Product.Get(idProduct);

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            BO.Product newProduct = new();
            newProduct.Id = product.ID;
            newProduct.Name = product.Name;
            newProduct.Price = product.Price;
            newProduct.Category = (Categories)product.Category;
            newProduct.InStock = product.InStock;

            return newProduct;
        }
        else
        {
            throw new Exception();
        }

    }
    public ProductItem ProductDetailsClient(BO.Cart newCart, int idProduct)
    {
        if (idProduct > 0)
        {
            try
            {
                product = dal.Product.Get(idProduct);

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        ProductItem newProductItem = new();
        newProductItem.Id = product.ID;
        newProductItem.Name = product.Name;
        newProductItem.Price = product.Price;
        newProductItem.Category = (Categories)product.Category;

        if (product.InStock > 0)
        {
            newProductItem.InStock = true;
        }
        OrderItem orderItem = newCart.Items.FirstOrDefault(ProductItem => ProductItem.Id == idProduct);

        if (orderItem is not null)
        {
            newProductItem.Amount = orderItem.Amount;
        }
        return newProductItem;
    }
    public void AddProduct(BO.Product newProduct)
    {
        if (newProduct.Id > 0 && newProduct.Price > 0 && newProduct.Name!= string.Empty && newProduct.InStock > 0)
        {
            product.ID = newProduct.Id;
            product.Name = newProduct.Name;
            product.Price = newProduct.Price;   
            product.InStock = newProduct.InStock;
            product.Category=(DO.Categories)newProduct.Category;   

            dal.Product.Add(product);
            return;
        }
        throw new Exception();
    }

    public void RemoveProduct(int idProduct)// i meed to understand.
    {
        IEnumerable<DO.Product> products = dal.Product.GetList();
    }

    public void UpdateProduct(BO.Product newproduct)
    {
        if (newproduct.Id > 0 && newproduct.Price > 0 && newproduct.Name != string.Empty && newproduct.InStock > 0)
        {
            product.ID = newproduct.Id;
            product.Name = newproduct.Name;
            product.Price = newproduct.Price;
            product.InStock = newproduct.InStock;
            product.Category = (DO.Categories)newproduct.Category;

            dal.Product.Update(product);
            return;
        }
        throw new Exception();
    }
}

