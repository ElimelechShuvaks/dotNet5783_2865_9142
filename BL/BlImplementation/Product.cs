using DalApi;
namespace BlImplementation;

/// <summary>
/// This class implements the functions of a main logical entity of a product.
/// </summary>
internal class Product : BlApi.IProduct
{
    private IDal dal = new Dal.DalList();
    private DO.Product product = new();

    /// <summary>
    /// This function transfers a list of products from the data layer to a list
    /// of products for an order request in the logical layer.
    /// </summary>
    /// <returns></returns>
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

    /// <summary>
    /// Product details request for admin screen.
    /// </summary>
    /// <param name="idProduct"></param>
    /// <returns></returns>
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
        newProduct.Id = product.ID;
        newProduct.Name = product.Name;
        newProduct.Price = product.Price;
        newProduct.Category = (BO.Enums.Categories)product.Category;
        newProduct.InStock = product.InStock;

        return newProduct;
    }

    /// <summary>
    /// Product details request (for buyer screen - from the catalog).
    /// </summary>
    /// <param name="newCart"></param>
    /// <param name="idProduct"></param>
    /// <returns></returns>
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

    /// <summary>
    /// Adding a product (for admin screen).
    /// </summary>
    /// <param name="newproduct"></param>
    /// <exception cref="Exception"></exception>
    public void Addproduct(BO.Product newproduct)
    {
        if (newproduct.Id > 0 && newproduct.Price > 0 && newproduct.Name != string.Empty && newproduct.InStock > 0)
        {
            product.ID = newproduct.Id;
            product.Name = newproduct.Name;
            product.Price = newproduct.Price;
            product.InStock = newproduct.InStock;
            product.Category = (DO.Categories)newproduct.Category;

            dal.Product.Add(product);
            return;
        }
        throw new Exception();
    }

    /// <summary>
    /// Product deletion (for admin screen).
    /// </summary>
    /// <param name="idProduct"></param>
    public void RemoveProduct(int idProduct)// i meed to understand.
    {
        if (dal.OrderItem.GetOrderItemsWithPredicate(orderItem => orderItem.Id == idProduct).Any())
        {
            throw new Exception();
        }
       dal.Product.Delete(idProduct);


    }

    /// <summary>
    /// Update product data (for admin screen).
    /// </summary>
    /// <param name="newproduct"></param>
    /// <exception cref="Exception"></exception>
    public void Updateproduct(BO.Product newproduct)
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

