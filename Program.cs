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

// ===================================================== todo 1 =====================================================
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

// checking
//foreach (Restaurant restaurant in restaurants)
//{
//    Console.WriteLine(restaurant);
//}
//foreach (Restaurant restaurant in restaurants)
//{
//    foreach (Menu menu in restaurant.Menus)
//    {
//        menu.DisplayFoodItems();
//    }
//}
// ===================================================== todo 2 (partner) =====================================================
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
        Customer cust = new Customer(custemail, custname);
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
        DateTime deldt = Convert.ToDateTime(orderInfo[3] + " " + orderInfo[4]);
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
        Order order = new Order(id, orderdt, stat, deldt, deladdr, null, false, custObj, restObj, null);
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
// ===================================================== todo 3 (partner) =====================================================
// ===================================================== todo 4 =====================================================

void ListAllOrders()
{
    List<Order> allOrders = new List<Order>();
    foreach (Restaurant rest in restaurants)
    {
        foreach (Order order in rest.OrderQueue)
        {
            allOrders.Add(order);
        }
    }

    allOrders = allOrders.OrderBy(o => o.OrderId).ToList();         // is this allowed?

    Console.WriteLine("All Orders");
    Console.WriteLine("==========");
    Console.WriteLine($"{"Order ID",-8}\t{"Customer",-10}\t{"Restaurant",-13}\t{"Delivery Date/Time",-18}\t{"Amount",-10}\t{"Status",-9}");
    Console.WriteLine($"{"--------",-8}\t{"----------",-10}\t{"-------------",-13}\t{"------------------",-18}\t{"------",-10}\t{"---------",-9}");

    foreach (Order order in allOrders)
    {
        string restName = order.Restaurant.RestaurantName;
        int ordID = order.OrderId;
        string custName = order.Customer.CustomerName;
        string ordTime = order.OrderDateTime.ToString("HH:mm");
        double amt = order.OrderTotal;
        string status = order.OrderStatus;

        Console.WriteLine($"{ordID,-8}\t{custName,-10}\t{restName,-13}\t{ordTime,-18}\t{amt,-10:C}\t{status,-9}");
    }
}



//Console.WriteLine("All Orders");                  // sorted by rest name, not ordid
//Console.WriteLine("==========");
//Console.WriteLine($"{"Order ID",-8}\t{"Customer",-10}\t{"Restaurant",-13}\t{"Delivery Date/Time",-18}\t{"Amount",-6}\t{"Status",-9}");
//Console.WriteLine($"{"--------",-8}\t{"----------",-10}\t{"-------------",-13}\t{"------------------",-18}\t{"------",-6}\t{"---------",-9}");
//foreach (Restaurant rest in restaurants)
//{
//    int ordID = 0;
//    string custName = "";
//    string restName = "";
//    DateTime ordTime = DateTime.MinValue;
//    double amt = 0;
//    string status = "";

//    restName = rest.RestaurantName;
//    foreach (Order order in rest.OrderQueue)
//    {
//        ordID = order.OrderId;
//        custName = order.Customer.CustomerName;
//        ordTime = order.OrderDateTime;
//        string formatOrdTime = ordTime.ToString("HH:mm");
//        status = order.OrderStatus;
//        Console.WriteLine($"{ordID,-8}\t{custName,-10}\t{restName,-13}\t{formatOrdTime,-18}\t{amt,-6:C}\t{status,-9}");
//    }
//}

// ===================================================== todo 6 =====================================================
void ProcessOrder()
{
    Console.WriteLine("Process Order");
    Console.WriteLine("=============");
    Console.Write("Enter Restaurant ID: ");
    string inputRestID = Console.ReadLine();
    Console.WriteLine();
    Restaurant? restFound = null;
    while (restFound == null)
    {
        foreach (Restaurant rest in restaurants)
        {
            if (rest.RestaurantId == inputRestID)
            {
                restFound = rest;
            }
        }
        if(restFound == null)
        {
            Console.WriteLine("Invalid Restaurant ID");
        }
    }
    foreach (Order order in restFound.OrderQueue)
    {
        Console.WriteLine($"Order {order.OrderId}");
        Console.WriteLine($"Customer: {order.Customer.CustomerName}");
        Console.WriteLine($"Ordered Items:");
        int i = 1;
        foreach (OrderedFoodItem ordFI in order.OrderedFoodItems)
        {
            Console.WriteLine($"{i}. {ordFI.ItemName} – {ordFI.QtyOrdered}");
            i++;
        }
        string time = order.OrderDateTime.ToString("HH:mm");
        Console.WriteLine($"Delivery date/time: {time}");
        Console.WriteLine($"Total Amount: {order.OrderTotal}");
        Console.WriteLine($"Order Status: {order.OrderStatus}");


        while (true)
        {
            Console.WriteLine();
            Console.Write("[C]onfirm / [R]eject / [S]kip / [D]eliver: ");
            string ordStage = Console.ReadLine().ToUpper();
            if (ordStage == "C" && order.OrderStatus == "Pending")
            {

                order.OrderStatus = "Preparing";
                Console.WriteLine($"Order {order.OrderId} confirmed. Status: {order.OrderStatus}");
                Console.WriteLine();
                break;

            }
            else if (ordStage == "R" && order.OrderStatus == "Pending")
            {
                order.OrderStatus = "Rejected";
                order.Restaurant.RefundOrders.Push(order);
                Console.WriteLine($"Order {order.OrderId} rejected. Status: {order.OrderStatus}");
                Console.WriteLine();
                break;
            }
            else if (ordStage == "S" && order.OrderStatus == "Cancelled")
            {
                Console.WriteLine($"Order {order.OrderId} skipped. Status: {order.OrderStatus}");
                Console.WriteLine();
                break;
            }
            else if (ordStage == "D" && order.OrderStatus == "Preparing")
            {
                order.OrderStatus = "Delivered";
                Console.WriteLine($"Order {order.OrderId} delivered. Status: {order.OrderStatus}");
                Console.WriteLine();
                break;
            }
            else
            {
                Console.WriteLine("Invalid choice. Please choose again.");
                continue;
            }
        }
    }
}

// ===================================================== todo 7 (partner) =====================================================

// ===================================================== todo 8 =====================================================
void DeleteOrder()
{
    Console.WriteLine("Delete Order");
    Console.WriteLine("============");
    // valid email
    Customer? custFound = null ;
    while (custFound == null)
    {
        Console.Write("Enter Customer Email: ");
        string inputCustEmail = Console.ReadLine();

        foreach (Customer cust in customerList)
        {
            if (cust.EmailAddress == inputCustEmail)
            {
                custFound = cust;
                Console.WriteLine("Pending Orders: ");
                foreach(Order ord in cust.Orders)
                {
                    if(ord.OrderStatus == "Pending")
                    {
                        Console.WriteLine(ord.OrderId);
                    }
                }
            
            }
        }
        if (custFound == null)
        {
            Console.WriteLine("Invalid Email.");
        }
    }
    // valid orderid==============================================
    Order? orderFound = null;
    while (orderFound == null)
    {
        Console.Write("Enter Order ID: ");
        int inputOrdID = Convert.ToInt32(Console.ReadLine());

        foreach (Order ord in custFound.Orders)
        {
            if (ord.OrderId == inputOrdID && ord.OrderStatus == "Pending")
            {
                orderFound = ord;
                Console.WriteLine();                                            // display cust details
                Console.WriteLine($"Customer: {ord.Customer.CustomerName}");
                Console.WriteLine("Ordered Items:");
                int i = 1;
                foreach (OrderedFoodItem ordFI in ord.OrderedFoodItems)
                {
                    Console.WriteLine($"{i}. {ordFI.ItemName} – {ordFI.QtyOrdered}");
                    i++;
                }
                string time = ord.OrderDateTime.ToString("HH:mm");
                Console.WriteLine($"Delivery date/time: {time}");
                Console.WriteLine($"Total Amount: {ord.CalculateOrderTotal():C}");
                Console.WriteLine($"Order Status: {ord.OrderStatus}");
            }
        }
        if (orderFound == null)
        {
            Console.WriteLine("Invalid Order ID.");
        }
    }
    // valid option======================================================
    Console.WriteLine();
    bool validOption = false;
    while (validOption == false)
    {
        Console.Write("Confirm deletion? [Y/N]: ");
        string inputOption = Console.ReadLine().ToUpper();

        if(inputOption == "N")
        {
            Console.WriteLine();
            validOption = true;
            Console.WriteLine($"Order {orderFound.OrderId} not deleted.");  
        }
        else if(inputOption == "Y")
        {
            Console.WriteLine();
            validOption = true;
            orderFound.Restaurant.RefundOrders.Push(orderFound);
            orderFound.OrderStatus = "Cancelled";
            Console.WriteLine($"Order {orderFound.OrderId} cancelled. Refund of {orderFound.CalculateOrderTotal():C} processed.");
        }
        
        else
        {
            Console.WriteLine("Invalid Option.");
        }
    }
}


//Delete Order
//============
//Enter Customer Email: alice.tan@email.com
//Pending Orders:
//1004
//1008
//Enter Order ID: 1004
//Customer: Alice Tan
//Ordered Items:
//1. Chicken Rice - 2
//2. Beef Burger – 1
//Delivery date/time: 15/02/2026 14:00
//Total Amount: $25.80
//Order Status: Pending
//Confirm deletion? [Y/N]: Y
//Order 1004 cancelled. Refund of $25.80 processed.

// ===================================================== advanced feature a) =====================================================

void TodaysUnprocessedOrders()
{
    List<Order> allOrders = new List<Order>();
    foreach (Restaurant rest in restaurants)
    {
        foreach (Order order in rest.OrderQueue)
        {
            allOrders.Add(order);
        }
    }
    int ordersPending = 0;
    int ordersRejected = 0;
    int ordersPreparing = 0;
    DateTime today = DateTime.Now;
    foreach(Order order in allOrders)
    {
        if(order.OrderStatus == "Pending" && today == order.DeliveryDateTime)
        {
            ordersPending += 1;
            //int timeDiff = Convert.ToInt32(order.DeliveryDateTime.ToString()) - Convert.ToInt32(order.OrderDateTime.ToString());
            TimeSpan timeDiff = order.DeliveryDateTime - order.OrderDateTime;
            if (timeDiff.TotalHours < 1)
            {
                order.OrderStatus = "Rejected";
                ordersRejected += 1;
            }
            else
            {
                order.OrderStatus = "Preparing";
                ordersPreparing += 1;
            }
        }
    }

    Console.WriteLine("Unprocessed Orders");
    Console.WriteLine("==================");
    Console.WriteLine();
    Console.WriteLine($"Number of Orders Processed: {ordersPending}");
    Console.WriteLine($"Number of Orders being prepared: {ordersPreparing}");
    Console.WriteLine($"Number of Orders being rejected: {ordersRejected}");
    double percentage = Convert.ToDouble(ordersPending) / Convert.ToDouble(allOrders.Count) * 100;
    Console.WriteLine($"% of automatically processed orders against all orders: {percentage:F2}%");
}








// ===================================================== while loop =====================================================

    //int totalRestaurants = 0;
    //int totalFoodItems = 0;
    //int totalCustomers = 0;
    //int totalOrders = 0;
    //foreach (Restaurant rest in restaurants)
    //{
    //    totalRestaurants += 1;
    //    foreach (Order ord in rest.OrderQueue)
    //    {
    //        totalOrders += 1;
    //        totalCustomers += 1;
    //        //foreach(Customer cust in ord.Customer)
    //        //{
    //        //    totalCustomers += 1;
    //        //}
    //        foreach (FoodItem fi in ord.OrderedFoodItems)
    //        {
    //            totalFoodItems += 1;
    //        }
    //    }

    //}
    //Console.WriteLine("Welcome to the Gruberoo Food Delivery System");
    //Console.WriteLine($"{totalRestaurants} restaurants loaded!");
    //Console.WriteLine($"{totalFoodItems} food items loaded!");
    //Console.WriteLine($"{totalCustomers} customers loaded!");
    //Console.WriteLine($"{totalOrders} orders loaded!");