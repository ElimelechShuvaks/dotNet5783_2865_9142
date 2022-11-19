using BO;
using DalApi;
namespace BlImplementation;
internal class Product : BlApi.IProduct
{
    private IDal dal = new Dal.DalList();
    private DO.Product product = new();
    public IEnumerable<BO.ProductForList> ProductListRequest()
    {
        IEnumerable<DO.Product> products = dal.Product.GetList();
        List<BO.ProductForList> newProductForList = new List<BO.ProductForList>(products.Count());

        foreach (DO.Product p in products)
        {
            BO.ProductForList productForList = new BO.ProductForList();
            productForList.ID = p.ID;
            productForList.Name = p.Name;
            productForList.Category = (BO.Enums.Categories)p.Category;
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
        }
        BO.Product newProduct = new();
        newProduct.ID = product.ID;
        newProduct.Name = product.Name;
        newProduct.Price = product.Price;
        newProduct.Category = (BO.Enums.Categories)product.Category;
        newProduct.InStock = product.InStock;

        return newProduct;
    }
    public BO.ProductItem ProductDetailsClient(BO.Cart newCart, int idProduct)
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

        BO.ProductItem newProductItem = new();
        newProductItem.ID = product.ID;
        newProductItem.Name = product.Name;
        newProductItem.Price = product.Price;
        newProductItem.Category = (BO.Enums.Categories)product.Category;

        if (product.InStock > 0)
        {
            newProductItem.InStock = true;
        }
        BO.OrderItem orderItem = newCart.Items.FirstOrDefault(ProductItem => newProductItem.ID == idProduct);

        if (orderItem is not null)
        {
            newProductItem.Amount = orderItem.Amount;
        }
        return newProductItem;
    }
    public void Addproduct(BO.Product newproduct)
    {
        if (newproduct.ID > 0 && newproduct.Price > 0 && newproduct.Name!= string.Empty && newproduct.InStock > 0)
        {
            product.ID = newproduct.ID;
            product.Name = newproduct.Name;
            product.Price = newproduct.Price;   
            product.InStock = newproduct.InStock;
            product.Category=(DO.Categories)newproduct.Category;   

            dal.Product.Add(product);
            return;
        }
        throw new Exception();
    }
    public void RemoveProduct(int idProduct)// i meed to understand.
    {
        IEnumerable<DO.Product> products = dal.Product.GetList();
    }

    public void Updateproduct(BO.Product newproduct)
    {
        if (newproduct.ID > 0 && newproduct.Price > 0 && newproduct.Name != string.Empty && newproduct.InStock > 0)
        {
            product.ID = newproduct.ID;
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

