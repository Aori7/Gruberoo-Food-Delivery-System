// Student Number: S10275270A
// Student Name: Adawiyah
// Partner Name: Rui Min

using PRG2_ASG_Gruberoo_Del_System;
using System.Diagnostics.CodeAnalysis;
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
    string[] csvLines = File.ReadAllLines("customers.csv");
    for (int i = 1; i < csvLines.Length; i++) // start from row 2 (row 1 is header)
    {
        string[] custInfo = csvLines[i].Split(",");
        string custname = custInfo[0];
        string custemail = custInfo[1];
        Customer cust = new Customer(custemail,custname); //add new cust objects
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

//feature 3
// todo 5: create a new order
string[] lines = File.ReadAllLines("orders.csv");
string[] lastRow = lines[lines.Length - 1].Split(',');
int newOrderId = Convert.ToInt32(lastRow[0]) + 1; // generate a new order id 

void createnewOrder()
{
    Console.WriteLine("Create New Order"); //display contents
    Console.WriteLine("================");
    Console.WriteLine("Enter Customer Email: ");
    string emailInput = Console.ReadLine();
    Customer cust = null;
    foreach (Customer c in customerList)// find which customer making order
    {
        if (c.EmailAddress == emailInput)
        {
            cust = c;
            break;
        }
    }
    if (cust == null)
    {
        Console.WriteLine("Customer not found.");
        return;
    }
    Console.WriteLine("Enter Restaurant ID: ");
    string restIdinput = Console.ReadLine();
    Restaurant rest = null;
    foreach (Restaurant r in restaurants)//find rest ordering at
    {
        if (r.RestaurantId == restIdinput)
        {
            rest = r;
            foreach (Menu menu in r.Menus) //list the items
            {
                int i = 1;
                foreach (FoodItem foodItem in menu.FoodItems)
                {
                    Console.WriteLine($"{i}. {foodItem.ItemName} - ${foodItem.ItemPrice}");
                    i++;
                }
            }
            break;
        }
    }
    if (rest == null)
    {
        Console.WriteLine("Restaurant not found.");
        return;
    }
    Console.WriteLine("Enter Delivery Date (dd/mm/yyyy): ");
    DateTime deldt = Convert.ToDateTime(Console.ReadLine());
    Console.WriteLine("Enter Delivery Time (hh:mm): ");
    DateTime deltime = Convert.ToDateTime(Console.ReadLine());
    Console.WriteLine("Enter Delivery Adress: ");
    string delAddr = Console.ReadLine();
    Console.WriteLine("Available Food Items:");

    List<FoodItem> foodList = new List<FoodItem>();
    foreach (Menu m in rest.Menus)
    {
        foreach (FoodItem fi in m.FoodItems)
        {
            foodList.Add(fi); //combine list of fooditem
        }
    }
    List<OrderedFoodItem> orderedfood = new List<OrderedFoodItem>();
    double total = 5;
    string paymentmtd = "";
    while (true)
    {
        Console.WriteLine("Enter Item Number (0 to finish): ");
        int itemno = Convert.ToInt32(Console.ReadLine());
        if (itemno == 0)
            break;
        if (itemno < 1 || itemno > foodList.Count) // validation
        {
            Console.WriteLine("Invalid number, try again.");
            continue;
        }
        Console.WriteLine("Enter Quantity: ");
        int qty = Convert.ToInt32(Console.ReadLine());
        if (qty <= 0) { Console.WriteLine("Quantity must be more than 0"); continue; } //validation
        FoodItem foodItem = foodList[itemno - 1]; //select the item user input
        OrderedFoodItem ordereditem = new OrderedFoodItem(foodItem.ItemName, foodItem.ItemDesc, foodItem.ItemPrice, foodItem.Customise, qty);
        orderedfood.Add(ordereditem); //add to ordered list
    }
    string specialRequest = ""; 
    Console.WriteLine("Add special request? [Y/N]: ");
    string request = Console.ReadLine().ToUpper(); //come back later to continue
    if (request != "Y" && request != "N")
    {
        Console.WriteLine("Invalid input! no request added.");
    }
    else if(request == "Y") 
    {
        Console.WriteLine("Enter request: ");
        specialRequest = Console.ReadLine();
    }
    foreach (OrderedFoodItem item in orderedfood) // calculate the order total
    {
        total += item.CalculateSubtotal();
    }
    Console.WriteLine($"Order Total: ${total-5} + $5.00(delivery) = ${total}");
    while (true)
    {
        Console.WriteLine("Proceed to payment? [Y/N]: ");
        string makepayment = Console.ReadLine().ToUpper();
        if (makepayment == "N")
        {
            Console.WriteLine("Order Canceled.");
            return;
        }
        else if (makepayment == "Y")
        {
            while (true)
            {
                Console.WriteLine("Payment Method:" + "\n" + "[CC] Credit Card / [PP] PayPal / [CD] Cash on Delivery: ");
                string paymentmethod = Console.ReadLine().ToUpper();
                if (paymentmethod != "CC" && paymentmethod != "PP" && paymentmethod != "CD")
                {
                    Console.WriteLine("Invalid payment method");
                    continue;
                }
                if (paymentmethod == "CC") { paymentmtd = "Credit Card"; break; }
                if (paymentmethod == "PP") { paymentmtd = "PayPal"; break; }
                if (paymentmethod == "CD") { paymentmtd = "Cash on Delivery"; break; }
            }
        }
        else
        {
            Console.WriteLine("Invalid input!");
            continue;
        }
        break;
    }
    Order order = new Order(newOrderId, DateTime.Now, "Pending", deldt, delAddr,paymentmtd,true,cust,rest,null);
    cust.AddOrder(order); // add to cust's order
    rest.OrderQueue.Enqueue(order); // add to order queue
    Console.WriteLine($"Order {order.OrderId} created successfully! Status: {order.OrderStatus}");
    // Console.WriteLine($"Orders in queue for {rest.RestaurantId}: {rest.OrderQueue.Count}"); // test, remove after
}
createnewOrder();
