namespace P0UI
{
    public enum MenuType
    {
        MainMenu,
        AddCustomer,
        ShowCustomers,
        CurrentCustomer,
        ShowStoreFronts,
        CurrentStoreFront,
        ShowProducts,
        CurrentProduct,
        PlaceOrder,
        OrderMenu,
        StoreInventory,
        ReplenishInventory,
        ItemMenu,
        Exit
    }

    public interface IMenu
    {
        void Menu();


        MenuType YourChoice();
    }
}