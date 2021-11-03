using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
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
        //Modifies Customer?
        public Model.Customers UpdateCustomer(Model.Customers p_update)
        {
            // var query = 
            //     from cust in _context.Customers
            //     where cust.CustId == p_update.Id
            //     select cust; 
            // foreach (Entity.Customer cust in query)
            // {
            //     cust.CustName = p_update.Name;
            //     cust.CustAddres = p_update.Address;
            //     cust.CustEmail = p_update.Email;
            //     cust.CustPhonenumber = p_update.PhoneNumber;
            // }
            _context.Customers.Update
            (
                new Entity.Customer()
                {
                    CustName = p_update.Name,
                    CustAddres = p_update.Address,
                    CustEmail = p_update.Email,
                    CustPhonenumber = p_update.PhoneNumber
                }
            );
            _context.SaveChanges();
            return p_update;
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
        // Finds customer by Id
        public Model.Customers GetCustomersById(int p_Id)
        {
            Entity.Customer custToFind = _context.Customers
                                            .AsNoTracking()
                                            .FirstOrDefault(cust => cust.CustId == p_Id);

            return new Model.Customers(){
                Id = custToFind.CustId,
                Name = custToFind.CustName,
                Address = custToFind.CustAddres,
                Email = custToFind.CustEmail,
                PhoneNumber = custToFind.CustPhonenumber
            };
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
        

        //Allows order addition
        public Model.Orders AddOrder(Model.Customers p_cust, Model.Orders p_ord) //placing an order, hopefully
        {
            var cust = _context.Customers
                .First<Entity.Customer>(cust => cust.CustId == p_cust.Id);
            cust.MyOrders.Add(new Entity.MyOrder()
            {
                OrderAddress = p_ord.SLocation,
                OrderPrice = p_ord.TotalPrice,
                StoreId = p_ord.StoreId,
                CustId = p_ord.CustId
            });

            //This method wil save the changes made to the database
            _context.SaveChanges();
            return p_ord;
        }
        // Will hopefully converts from Entity to Model for Order and matches to customer Id
        public List<Model.Orders> GetAllOrders(Model.Customers p_cust)
        {
            // Method Syntax - looks cleaner
            return _context.MyOrders
                .Where(ord => ord.CustId == p_cust.Id) //we find the orders that match customer ID (entity == model)
                .Select(ord => new Model.Orders() //convert to model.order
                {
                    //model = entity
                    SLocation = ord.OrderAddress,
                    TotalPrice = ord.OrderPrice,
                    CustId = ord.CustId,
                    StoreId = ord.StoreId,
                    Id = ord.OrderId
                }
            ).ToList();
        }
        // Will hopefully converts from Entity to Model for Order and matches to store Id
        public List<Model.Orders> GetAllStoreOrders(Model.StoreFronts p_store)
        {
            // Method Syntax - looks cleaner
            return _context.MyOrders
                .Where(ord => ord.StoreId == p_store.Id) //we find the orders that match customer ID (entity == model)
                .Select(ord => new Model.Orders() //convert to model.order
                {
                    //model = entity
                    SLocation = ord.OrderAddress,
                    TotalPrice = ord.OrderPrice,
                    CustId = ord.CustId,
                    StoreId = ord.StoreId,
                    Id = ord.OrderId
                }
            ).ToList();
        }


        // Converts from Entity to Model for products
        public List<Model.Products> GetAllProducts()
        {
            // Method Syntax
            return _context.Products
                .Select(prod => new Model.Products()
                {
                    PName = prod.ProdName,
                    Price = prod.ProdPrice,
                    InvId = prod.InvId,
                    LiId = prod.LiId,
                    Id = prod.ProdId
                }
            ).ToList();
        }


        // Converts from Entity to Model for Line Items
        public List<Model.LineItems> GetAllLineItems(Model.LineItems p_item)
        {
            // Method Syntax
            return _context.LineItems
                .Where(item => item.LiId == p_item.Id)
                .Select(item => new Model.LineItems()
                {
                    Product = item.LiProduct,
                    Quantity = item.LiQuantity,
                    //OrderId = item.OrderId,
                    Id = item.LiId
                }
            ).ToList();
        }
        //line item update
        void IRepository.UpdateLineItem(int p_itemID, int p_quan)
        {
            var query = _context.LineItems
                .FirstOrDefault<Entity.LineItem>(item => item.LiId == p_itemID);
            query.LiQuantity = p_quan;
            _context.SaveChanges();
        }
        Model.LineItems IRepository.UpdateItemQuantity(Model.LineItems p_li)
        {
            Entity.LineItem itemUpdated = new Entity.LineItem()
            {
                LiId = p_li.Id,
                LiProduct = p_li.Product,
                LiQuantity = p_li.Quantity,
                OrderId = p_li.OrderId
            };
            _context.LineItems.Update(itemUpdated);
            _context.SaveChanges();
            return p_li;
        }
        public Model.LineItems GetItemsById(int p_itemId)
        {
            Entity.LineItem itemToFind = _context.LineItems
                                            .AsNoTracking()
                                            .FirstOrDefault(item => item.LiId == p_itemId);

            return new Model.LineItems(){
                Id = itemToFind.LiId,
                Product = itemToFind.LiProduct,
                OrderId = itemToFind.OrderId,
                Quantity = itemToFind.LiQuantity
            };
        }
        // List<Model.LineItems> GetLineItems(int p_Id)
        // {
        //     return _context.LineItems
        //         .Where(item => item.OrderId == p_Id.) 
        //         .Select(ord => new Model.Orders() 
        //         {
        //             //model = entity
        //             SLocation = ord.OrderAddress,
        //             TotalPrice = ord.OrderPrice,
        //             CustId = ord.CustId,
        //             StoreId = ord.StoreId,
        //             Id = ord.OrderId
        //         }
        //     ).ToList();
        // }


        // Converts from Entity to Model for Inventory?
        public List<Model.Inventory> GetAllInventory()
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