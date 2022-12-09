using DalApi;
using System.Drawing;
//using Windows.UI.Xaml.Media.Imaging;
//using Xamarin.Forms.Internals;

namespace BlImplementation;

internal class Product : BlApi.IProduct
{
    private IDal dal = new Dal.DalList();
    private DO.Product product = new();

    public IEnumerable<BO.ProductForList> ProductListRequest(Func<BO.ProductForList?, bool>? func = null)
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
        if (func != null)
        {

            return newProductForList.Where(func);
        }

        return newProductForList;
    }

    public BO.Product ProductDetailsManger(int idProduct)
    {
        try
        {
            if (idProduct >= 100000 && idProduct < 1000000)
            {
                product = dal.Product.Get(productFunc => productFunc?.ProductId == idProduct);

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
        catch (DO.EntityNotExistException ex)
        {
            throw new BO.EntityNotExistException(ex.Message, ex);
        }
    }

    public BO.ProductItem ProductDetailsClient(BO.Cart newCart, int idProduct)
    {
        BO.ProductItem newProductItem = new();
        try
        {
            if (idProduct >= 100000)
            {
                product = dal.Product.Get(productFunc => productFunc?.ProductId == idProduct);
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
        catch (DO.EntityNotExistException ex)
        {
            throw new BO.EntityNotExistException(ex.Message, ex);
        }

        return newProductItem;
    }

    public void AddProduct(BO.Product newProduct)
    {
        try
        {
            if (newProduct.Id < 100000)
                throw new BO.IdNotValidException("The id is too short.");
            if (newProduct.Price <= 0)
                throw new BO.PriceNotValidException("The price is not valid.");
            if (newProduct.Name == string.Empty)
                throw new BO.NameNotValidException("The name is empty.");
            if (newProduct.InStock <= 0)
                throw new BO.InStockNotValidException("The amount in stock is not valid.");

            product.ProductId = newProduct.Id;
            product.Name = newProduct.Name;
            product.Price = newProduct.Price;
            product.InStock = newProduct.InStock;
            product.Category = (DO.Categories)newProduct.Category!;

            dal.Product.Add(product);
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
        catch (DO.EntityNotExistException ex)
        {
            throw new BO.EntityNotExistException(ex.Message, ex);
        }
    }

    public void UpdateProduct(BO.Product newProduct)
    {
        try
        {
            if (newProduct.Id < 100000)
                throw new BO.IdNotValidException("The id is too short.");
            if (newProduct.Price <= 0)
                throw new BO.PriceNotValidException("The price is not valid.");
            if (newProduct.Name is null)
                throw new BO.NameNotValidException("The name is empty.");
            if (newProduct.InStock <= 0)
                throw new BO.InStockNotValidException("The amount in stock is not valid.");

            product.ProductId = newProduct.Id;
            product.Name = newProduct.Name;
            product.Price = newProduct.Price;
            product.InStock = newProduct.InStock;
            product.Category = (DO.Categories)newProduct.Category!;

            dal.Product.Update(product);
        }
        catch (BO.BlExceptions ex)
        {
            throw ex;
        }
        catch (DO.EntityNotExistException ex)
        {
            throw new BO.EntityNotExistException(ex.Message, ex);
        }
    }
}

