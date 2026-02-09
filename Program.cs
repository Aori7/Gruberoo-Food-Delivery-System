// Student Number: S10272389
// Student Name: Chi Rui Min
// Partner Name: Adawiyah

using PRG2_ASG_Gruberoo_Del_System;
using System.Text;

// =============== basic features ===============
// student 1: ada [TODO: 2,3,5,7 ]
// student 2: rui min [TODO: 1,4,6,8 ]

// todo 1: load files (restaurants and food items)
// todo 2: load files (customers and orders)
// todo 3: list all restaurants and menu items
// todo 4: list all orders with basic information
// todo 5: create a new order
// todo 6: prompt an order
// todo 7: modify an existing order
// todo 8: delete an existing order

// ==============================================

// initialise a list to hold all restaurants's details
List<Restaurant> restaurants = new List<Restaurant>();
// method to load the file into the collection
void LoadRestaurants()
{
    string[] lines = File.ReadAllLines("restaurants.csv");
    for (int i = 1; i < lines.Length; i++)
    {
        string[] data = lines[i].Split(',');
        string id = data[0];
        string name = data[1];
        string email = data[2];
        Restaurant rest = new Restaurant(id, name, email);
        restaurants.Add(rest);
    }
}
LoadRestaurants();

// method to load the file into the collection
void LoadFoodItems()
{
    string[] lines = File.ReadAllLines("fooditems.csv");
    for (int i = 1; i < lines.Length; i++)
    {
        string[] data = lines[i].Split(',');
        string restId = data[0];
        string name = data[1];
        string desc = data[2];
        double price = Convert.ToDouble(data[3]);
        FoodItem fi = new FoodItem(name, desc, price);
        foreach (Restaurant rest in restaurants)
        {
            if (rest.RestaurantId == restId)
            {
                foreach (Menu menu in rest.Menus)
                {
                    menu.AddFoodItem(fi);
                }
            }
            else
            {
                continue;
            }
        }
    }
}
LoadFoodItems();

// checking
foreach (Restaurant restaurant in restaurants)
{
    Console.WriteLine(restaurant);
}
foreach (Restaurant restaurant in restaurants)
{
    foreach (Menu menu in restaurant.Menus)
    {
        menu.DisplayFoodItems();
    }   
}


// ehehe