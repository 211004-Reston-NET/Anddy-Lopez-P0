using System;
using System.Collections.Generic;
using P0BL;
using P0Models;

namespace P0UI
{
    public class ReplenishInventory : IMenu
    {
        private static List<LineItems> _itemList = new List<LineItems>();
        private ILineItemBL _itemBL;
        public ReplenishInventory(ILineItemBL p_itemBL)
        {
            _itemBL = p_itemBL;
            _itemList = _itemBL.GetAllLineItems(ShowProducts._findProd);
        }
        public static int _addAmount;
        
        public void Menu()
        {
            Console.WriteLine("Welcome to Inventory Replenishor!");
            foreach (LineItems item in _itemList)
            {
                Console.WriteLine("--------------------");
                Console.WriteLine(item);
                Console.WriteLine("--------------------");
            }

            Console.WriteLine("What would you like to do?");
            Console.WriteLine("[a] - Set new inventory amount");
            Console.WriteLine("[x] - Go back to Main Menu");
        }

        public MenuType YourChoice()
        {
            string userChoice = Console.ReadLine();
            switch (userChoice)
            {
                case "a":
                    int _currentItem = _itemList.Count-1;
                    Console.WriteLine("Type in new value for Quantity");
                    _addAmount = Int32.Parse(Console.ReadLine());
                    _itemBL.UpdateLineItem(_itemList[_currentItem].Id , _addAmount);
                    return MenuType.ReplenishInventory;
                case "x":
                    return MenuType.MainMenu;
                default:
                    Console.WriteLine("Please input a valid response!");
                    Console.WriteLine("Press Enter to continue");
                    Console.ReadLine();
                    return MenuType.AddCustomer;
            }
        }
    }
}