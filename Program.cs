// Student Number: S10275270A
// Student Name: Adawiyah
// Partner Name: Rui Min

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

//=========================================================================
// RUI MIN'S RESTAURANT LOADING FEATURE, REMOVE AFTER FINALISED!!! ========================
// FOR TESTING PURPOSES
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
        FoodItem fi = new FoodItem(name, desc, price, null);
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
// ===============================================================================

// STAGE 1
// feature 1 ====================================
// load files (customers and orders)

List<Customer> customerList = new List<Customer>();

// load, read, and create objects - customer
void loadCust()
{
    // read file
    string[] csvLines = File.ReadAllLines("customers.csv");
    // line 1 is the header of file, skip
    for (int i = 1; i < csvLines.Length; i++) // start from row 2 (row 1 is header)
    {
        string[] custInfo = csvLines[i].Split(",");
        string custname = custInfo[0];
        string custemail = custInfo[1];

        //add new cust objects
        Customer cust = new Customer(custemail,custname);
        customerList.Add(cust);
    }
}
loadCust();

void loadOrder()
{
    string[] csvLines = File.ReadAllLines("orders.csv");
    for (int i = 1; i < csvLines.Length; i++)
    {
        string[] orderInfo = csvLines[i].Split(",");
        int id = Convert.ToInt32(orderInfo[0]);
        string custemail = orderInfo[1];
        string restid = orderInfo[2];
        DateTime deldt = Convert.ToDateTime(orderInfo[3] + " " +  orderInfo[4]);
        string deladdr = orderInfo[5];
        DateTime orderdt = Convert.ToDateTime(orderInfo[6]);
        double ordertotal = Convert.ToDouble(orderInfo[7]);
        string stat = orderInfo[8];
        string itemsStr = orderInfo[9];
        for (int j = 10; j < orderInfo.Length; j++)
        {
            itemsStr += "," + orderInfo[j];
        }
        itemsStr = itemsStr.Trim('"');
        Customer custObj = null;
        foreach (Customer c in customerList)
        {
            if (c.EmailAddress == custemail)
            {
                custObj = c;
                break;
            }
        }
        Restaurant restObj = null;
        foreach (Restaurant rest in restaurants)
        {
            if (rest.RestaurantId == restid)
            {
                restObj = rest;
                break;
            }
        }
        Order order = new Order(id, orderdt, stat, deldt, deladdr, null, false, custObj, restObj,null);
        order.OrderTotal = ordertotal; // set the order's total from the csv
        string[] items = itemsStr.Split("|");
        foreach (string item in items)
        {
            string[] name_qty = item.Split(",");

            string name = name_qty[0].Trim();
            int qty = Convert.ToInt32(name_qty[1].Trim());

            OrderedFoodItem ordereditem =
                new OrderedFoodItem(name, "", 0, "", qty);

            order.AddOrderedFoodItem(ordereditem);
        }
        restObj.OrderQueue.Enqueue(order);
        custObj.AddOrder(order);
    }
}
loadOrder();

// feature 2
// todo 3: list all restaurants and menu items
void displayRestMenu()
{
    Console.WriteLine("All Restaurant and Menu Items");
    Console.WriteLine("=============================");
    foreach(Restaurant restaurant in restaurants)
    {
        Console.WriteLine($"Restaurant: {restaurant.RestaurantName} ({restaurant.RestaurantId})");
        foreach(Menu menu in restaurant.Menus)
        {
            foreach(FoodItem item in menu.FoodItems)
            {
                Console.WriteLine($"    - {item.ItemName}: {item.ItemDesc} - ${item.ItemPrice}");
            }
        }
    }
}
displayRestMenu();