using DalApi;
namespace BlImplementation;

internal class Product : BlApi.IProduct
{
    private IDal dal = new Dal.DalList();
    private DO.Product product = new();

    public IEnumerable<BO.ProductForList> ProductListRequest()
    {
        IEnumerable<DO.Product?> products = dal.Product.GetList();

        List<BO.ProductForList> newProductForList = new List<BO.ProductForList>(products.Count());

        foreach (DO.Product p in products)
        {
            newProductForList.Add(new BO.ProductForList
            {
                Id = p.ProductId,
                Name = p.Name,
                Category = (BO.Categories)p.Category!,
                Price = p.Price,
            });
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

                BO.Product newProduct = new BO.Product
                {
                    Id = product.ProductId,
                    Name = product.Name,
                    Price = product.Price,
                    Category = (BO.Categories)product.Category!,
                    InStock = product.InStock,
                };

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
            throw new BO.IdNotExistException(ex.Message, ex);
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
                newProductItem.Category = (BO.Categories)product.Category!;

                if (product.InStock > 0)
                    newProductItem.InStock = true;
                else
                    newProductItem.InStock = false;

                BO.OrderItem orderItem = newCart.Items!.FirstOrDefault(ProductItem => ProductItem.ProductId == idProduct)!;

                if (orderItem is null)
                {
                    newProductItem.Amount = 0;
                }
                else
                {
                    newProductItem.Amount = orderItem.Amount;
                }
            }
            else // product id is invalid.
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
            throw new BO.IdNotExistException(ex.Message, ex);
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
                product.Category = (DO.Categories)newProduct.Category!;

                dal.Product.Add(product);
            }
            else
                throw new BO.NotValidDetailsException("Invalid product details");
        }
        catch (BO.BlExceptions ex)
        {
            throw ex;
        }
        catch (DO.IdExistException ex)
        {
            throw new BO.IdExistException(ex.Message, ex);
        }
    }

    public void RemoveProduct(int idProduct)
    {
        try
        {
            if (dal.OrderItem.GetList(orderItem => orderItem?.ProductId == idProduct).Any())
                throw new BO.CanNotRemoveProductException("can't remove the product becouse he is found in exsist orders.");

            else
                dal.Product.Delete(idProduct);
        }
        catch (BO.BlExceptions ex)
        {
            throw ex;
        }
        catch (DO.IdNotExistException ex)
        {
            throw new BO.IdNotExistException(ex.Message, ex);
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
                product.Category = (DO.Categories)newProduct.Category!;

                dal.Product.Update(product);
            }
            else
                throw new BO.NotValidDetailsException("Invalid product details");
        }
        catch (BO.BlExceptions ex)
        {
            throw ex;
        }
        catch (DO.IdNotExistException ex)
        {
            throw new BO.IdNotExistException(ex.Message, ex);
        }
    }
}

