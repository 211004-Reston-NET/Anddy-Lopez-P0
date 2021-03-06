using System.Collections.Generic;
using P0Models;

namespace P0BL
{
    public interface IStoreFrontsBL
    {
        //List store fronts
        List<StoreFronts> GetAllStoreFronts();

        //Allows store fronts search by name
        List<StoreFronts> GetStoreFronts(string p_sname);

        //list the orders from stores
        List<Orders> GetAllStoreOrders(StoreFronts p_store);
    }
}