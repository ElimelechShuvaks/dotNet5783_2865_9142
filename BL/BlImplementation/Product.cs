using DalApi;
using BlApi;
namespace BlImplementation;
internal class Product : IProduct
{

    private IDal dal = new Dal.DalList();
    private DO.Product product = new();
    public IEnumerable<BO.ProductForList> ProductListRequest()
    {
        IEnumerable<DO.Product> products = dal.Product.GetList();
        IEnumerable<BO.ProductForList> newProductForList = new List<BO.ProductForList>(products.Count());

        foreach (DO.Product p in products)
        {
            foreach (BO.ProductForList L in newProductForList)
            {
                L.ID = p.ID;
                L.Name = p.Name;
                L.Category = (BO.Enums.Categories)p.Category;
                L.Price = p.Price;
            }
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
        newProduct.Category= (BO.Enums.Categories)product.Category;
        newProduct.InStock = product.InStock;

        return newProduct;
    }

    public BO.ProductItem ProductDetailsClient(/*BO.Cart newCart,*/ int idProduct)
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
       // newProductItem.Amount = product.?
        newProductItem.InStock = product.InStock;

        return newProductItem;



      
    }


}

