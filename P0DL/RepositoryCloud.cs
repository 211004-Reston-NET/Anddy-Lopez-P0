using System.Collections.Generic;
using System.Linq;
using P0Models;
using Entity = P0DL.Entities; 
using Model = P0Models;

namespace P0DL
{
    public class RepositoryCloud : IRepository
    {
        //Dependency Injection for variables
        private Entity.P0DatabaseContext _context;
        public RepositoryCloud(Entity.P0DatabaseContext p_context)
        {
            _context = p_context;
        }
        
        // Adds customers
        public Model.Customers AddCustomer(Model.Customers p_cust)
        {
            _context.Customers.Add
            (
                new Entity.Customer()
                {
                    CustName = p_cust.Name,
                    CustAddres = p_cust.Address,
                    CustEmail = p_cust.Email,
                    CustPhonenumber = p_cust.PhoneNumber
                }
            );

            //This method wil save the changes made to the database
            _context.SaveChanges();
            return p_cust;
        }

        // Converts from Entity to Model for Customers
        public List<Model.Customers> GetAllCustomers()
        {
            // Method Syntax
            return _context.Customers.Select(cust =>
                new Model.Customers()
                {
                    Name = cust.CustName,
                    Address = cust.CustAddres, 
                    Email = cust.CustEmail,
                    PhoneNumber = cust.CustPhonenumber,
                    Id = cust.CustId
                }
            ).ToList();

            //Query Syntax for Inner Joins
            // var result = (from cust in _context.Customers
            //             select cust);
            
            // List<Model.Customers> listOfCust = new List<Model.Customers>();
            // foreach (var cust in result)
            // {
            //     listOfCust.Add(new Model.Customers(){
            //         Name = cust.CustName,
            //         Address = cust.CustAddres,
            //         Email = cust.CustEmail,
            //         PhoneNumber = cust.CustPhonenumber,
            //         Id = cust.CustId
            //     });
            // }

            // return listOfCust;
        }

        // Converts from Entity to Model for Stores
        public List<Model.StoreFronts> GetAllStoreFronts()
        {
            // Method Syntax
            return _context.StoreFronts.Select(store =>
                new Model.StoreFronts()
                {
                    SName = store.StoreName,
                    SAddress = store.StoreAddres,
                    Id = store.StoreId
                }
            ).ToList();
        }
        
        // Will hopefully converts from Entity to Model for Order... one day
        public List<Model.Orders> GetAllOrders()//Try again later
        {
            // Method Syntax
            return _context.MyOrders.Select(ord =>
                new Model.Orders()
                {
                    SLocation = ord.OrderAddress,
                    TotalPrice = ord.OrderPrice,
                    CustId = ord.CustId,
                    StoreId = ord.StoreId,
                    Id = ord.OrderId
                }
            ).ToList();
        }

        // Converts from Entity to Model
        public List<Model.Products> GetAllProducts()
        {
            // Method Syntax
            return _context.Products.Select(prod =>
                new Model.Products()
                {
                    PName = prod.ProdName,
                    Price = prod.ProdPrice,
                    InvId = prod.InvId,
                    LiId = prod.LiId,
                    Id = prod.ProdId
                }
            ).ToList();
        }

        // Finds customer by Id? - Not necessary?
        public Model.Customers GetCustomersById(int p_Id)
        {
            Entity.Customer custToFind = _context.Customers.Find(p_Id);

            return new Model.Customers(){
                Id = custToFind.CustId,
                Name = custToFind.CustName,
                Address = custToFind.CustAddres,
                Email = custToFind.CustEmail,
                PhoneNumber = custToFind.CustPhonenumber
            };
        }

        // Converts from Entity to Model for Line Items
        public List<LineItems> GetAllLineItems()
        {
            // Method Syntax
            return _context.LineItems.Select(item =>
                new Model.LineItems()
                {
                    Product = item.LiProduct,
                    Quantity = item.LiQuantity,
                    OrderId = item.OrderId,
                    Id = item.LiId
                }
            ).ToList();
        }

        // Converts from Entity to Model for Inventory?
        public List<Inventory> GetAllInventory()
        {
            // Method Syntax
            return _context.Inventories.Select(inv =>
                new Model.Inventory()
                {
                    Product = inv.InvProduct,
                    Quantity = inv.InvQuantity,
                    StoreId = inv.StoreId,
                    Id = inv.InvId
                }
            ).ToList();
        }
    }
}