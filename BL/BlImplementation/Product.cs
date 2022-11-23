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
            productForList.Id = p.ProductId;
            productForList.Name = p.Name;
            productForList.Category = (BO.Categories)p.Category;
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
            newProduct.Id = product.ProductId;
            newProduct.Name = product.Name;
            newProduct.Price = product.Price;
            newProduct.Category = (BO.Categories)product.Category;
            newProduct.InStock = product.InStock;

            return newProduct;
        }
        else
        {
            throw new Exception();
        }

    }
    public BO.ProductItem ProductDetailsClient(BO.Cart newCart, int idProduct)
    {
        BO.ProductItem newProductItem = new();
        try
        {
            if (idProduct > 0)
            {
                product = dal.Product.Get(idProduct);
                newProductItem.Id = product.ProductId;
                newProductItem.Name = product.Name;
                newProductItem.Price = product.Price;
                newProductItem.Category = (BO.Categories)product.Category;

                if (product.InStock > 0)
                {
                    newProductItem.InStock = true;
                }
                BO.OrderItem orderItem = newCart.Items.FirstOrDefault(ProductItem => ProductItem.OrderId == idProduct);

                if (orderItem is not null)
                {
                    newProductItem.Amount = orderItem.Amount;
                }
            }
        }
        catch (Exception)
        {

            throw;
        }
        return newProductItem;
    }
    public void AddProduct(BO.Product newProduct)
    {
        try
        {
            if (newProduct.Id > 0 && newProduct.Price > 0 && newProduct.Name != string.Empty && newProduct.InStock > 0)
            {
                product.ProductId = newProduct.Id;
                product.Name = newProduct.Name;
                product.Price = newProduct.Price;
                product.InStock = newProduct.InStock;
                product.Category = (DO.Categories)newProduct.Category;

                dal.Product.Add(product);
                return;
            }
            throw new Exception();
        }
        catch (Exception)
        {

            throw;
        }
    }

    public void RemoveProduct(int idProduct)
    {
        try
        {
            if (dal.OrderItem.GetOrderItemsWithPredicate(orderItem => orderItem.ProductId == idProduct).Any())
            {
                throw new Exception();
            }
            dal.Product.Delete(idProduct);
        }
        catch (Exception)
        {

            throw;
        }

    }

    public void UpdateProduct(BO.Product newproduct)
    {
        try
        {
            if (newproduct.Id > 0 && newproduct.Price > 0 && newproduct.Name != string.Empty && newproduct.InStock > 0)
            {
                product.ProductId = newproduct.Id;
                product.Name = newproduct.Name;
                product.Price = newproduct.Price;
                product.InStock = newproduct.InStock;
                product.Category = (DO.Categories)newproduct.Category;

                dal.Product.Update(product);
                return;
            }
            throw new Exception();
        }
        catch (Exception)
        {

            throw;
        }
    }
}

