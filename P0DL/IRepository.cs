using System.Collections.Generic;
using P0Models;

namespace P0DL
{
    public interface IRepository
    {
        //Allows customer addition
        Customers AddCustomer(Customers p_cust);

        //List Customers
        List<Customers> GetAllCustomers();

        //List Store Fronts
        List<StoreFronts> GetAllStoreFronts();
        List<Products> GetAllProducts();

        /// <summary>
        /// this will hopefully get all the products from a store
        /// will probably only get the products i seeded for know
        /// </summary>
        /// <returns>will return a list of products</returns>
        List<Products> GetAllProducts(Products p_prod);

        /// <summary>
        /// Will give all orders from a customer
        /// </summary>
        /// <param name="p_cust"></param>
        /// <returns>returns a list of orders</returns>
        List<Orders> GetAllOrders(Customers p_cust); //You are not read for this in the slightest

        /// <summary>
        /// Returns a specific customer based on ID
        /// </summary>
        /// <param name="p_Id">ID that it will check</param>
        /// <returns>Returns customer</returns>
        Customers GetCustomersById(int p_Id);

        List<LineItems> GetAllLineItems();
    }
}