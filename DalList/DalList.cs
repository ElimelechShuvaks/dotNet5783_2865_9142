using DalApi;

namespace Dal
{
    public class DalList : IDal
    {
        public IProduct Product => new DalProduct();
    }
}
