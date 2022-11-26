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
        try
        {
            if (idProduct >= 100000 && idProduct < 1000000)
            {
                product = dal.Product.Get(idProduct);

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
                throw new BO.IdNotValidException("not valid id for product");
            }
        }
        catch (BO.BlExceptions ex)
        {
            throw ex;
        }
        catch (DO.IdNotExistException ex)
        {
            throw new BO.IdNotExistException(ex.Message);
        }

    }

    public BO.ProductItem ProductDetailsClient(BO.Cart newCart, int idProduct)
    {
        BO.ProductItem newProductItem = new();
        try
        {
            if (idProduct >= 100000)
            {
                product = dal.Product.Get(idProduct);
                newProductItem.Id = product.ProductId;
                newProductItem.Name = product.Name;
                newProductItem.Price = product.Price;
                newProductItem.Category = (BO.Categories)product.Category;

                if (product.InStock > 0)
                    newProductItem.InStock = true;
                else
                    newProductItem.InStock = false;

                BO.OrderItem orderItem = newCart.Items.FirstOrDefault(ProductItem => ProductItem.ProductId == idProduct);

                if (orderItem is null)
                    throw new BO.IdNotExistException($"order item with product id: {idProduct} doesn't exsist in data source");

                newProductItem.Amount = orderItem.Amount;
            }
            else
            {
                throw new BO.IdNotValidException("not valid id for product");
            }
        }
        catch (BO.BlExceptions ex)
        {
            throw ex;
        }
        catch (DO.IdNotExistException ex)
        {
            throw new BO.IdNotExistException(ex.Message);
        }

        return newProductItem;
    }

    public void AddProduct(BO.Product newProduct)
    {
        try
        {
            if (newProduct.Id > 99999 && newProduct.Price > 0 && newProduct.Name != string.Empty && newProduct.InStock > 0)
            {
                product.ProductId = newProduct.Id;
                product.Name = newProduct.Name;
                product.Price = newProduct.Price;
                product.InStock = newProduct.InStock;
                product.Category = (DO.Categories)newProduct.Category;

                dal.Product.Add(product);
            }
            else
                throw new BO.NotValidProductException("Invalid product details");
        }
        catch (BO.BlExceptions ex)
        {
            throw ex;
        }
        catch (DO.IdExistException ex)
        {
            throw new BO.IdExistException(ex.Message);
        }
    }

    public void RemoveProduct(int idProduct)
    {
        try
        {
            if (dal.OrderItem.GetOrderItemsWithPredicate(orderItem => orderItem.ProductId == idProduct).Any())
                throw new BO.CanNotRemoveProductException("can't remove the product becouse he is found in exsist ordes.");

            else
                dal.Product.Delete(idProduct);
        }
        catch (BO.BlExceptions ex)
        {
            throw ex;
        }
        catch (DO.IdNotExistException ex)
        {
            throw new BO.IdNotExistException(ex.Message);
        }
    }

    public void UpdateProduct(BO.Product newProduct)
    {
        try
        {
            if (newProduct.Id > 99999 && newProduct.Price > 0 && newProduct.Name != string.Empty && newProduct.InStock > 0)
            {
                product.ProductId = newProduct.Id;
                product.Name = newProduct.Name;
                product.Price = newProduct.Price;
                product.InStock = newProduct.InStock;
                product.Category = (DO.Categories)newProduct.Category;

                dal.Product.Update(product);
            }
            else
                throw new BO.NotValidProductException("Invalid product details");
        }
        catch (BO.BlExceptions ex)
        {
            throw ex;
        }
        catch (DO.IdNotExistException ex)
        {
            throw new BO.IdNotExistException(ex.Message);
        }
    }
}

