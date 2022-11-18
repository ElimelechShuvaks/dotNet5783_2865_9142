using BO;
namespace BlApi;

/// <summary>
/// interface for Product.
/// </summary>
public interface IProduct
{
    /// <summary>
    /// Get list of "ProductForList" for manger and client.
    /// </summary>
    /// <returns></returns>
    public IEnumerable<ProductForList> ProductListRequest();

    /// <summary>
    /// Get list of ProductForList for manger.
    /// </summary>
    /// <param name="IdProduct"></param>
    /// <returns></returns>
    public Product ProductDetailsManger(int idProduct);

    /// <summary>
    /// Get list of ProductForList for Client
    /// </summary>
    /// <param name="IdProduct"></param>
    /// <returns></returns>
    public ProductItem ProductDetailsClient(int idProduct);

    /// <summary>
    /// Add product.
    /// </summary>
    /// <param name="product"></param>
    public void Addproduct(Product product);

    /// <summary>
    /// Remove product.
    /// </summary>
    /// <param name="product"></param>
    public void RemoveProduct(Product product);

    /// <summary>
    ///  Update product.
    /// </summary>
    /// <param name="product"></param>
    public void Updateproduct(Product product);
}
