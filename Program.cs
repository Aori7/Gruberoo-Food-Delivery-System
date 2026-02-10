// Student Number:
// Student Name:
// Partner Name:

using PRG2_ASG_Gruberoo_Del_System;
using System.Diagnostics.Metrics;
using System.IO;
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

// collections
// initialise a list to hold all restaurants's details
// todo 1
List<Restaurant> restaurants = new List<Restaurant>();
// todo 2
List<Customer> customerList = new List<Customer>();
// todo 5
string[] lines = File.ReadAllLines("orders.csv");
string[] lastRow = lines[lines.Length - 1].Split(',');
int newOrderId = Convert.ToInt32(lastRow[0]) + 1; // generate a new order id 
// data to add to queue and stack
Queue<Order> orderQueue = new Queue<Order>();
Stack<Order> refundStack = new Stack<Order>();

// loading files
LoadRestaurants();
LoadFoodItems();
loadCust();
loadOrder();

int totalRestaurants = restaurants.Count;
int totalFoodItems = 0;
int totalCustomers = customerList.Count;
int totalOrders = 0;
foreach (Restaurant rest in restaurants)
{
    foreach (Menu menu in rest.Menus)
    {
        foreach (FoodItem fi in menu.FoodItems)
        {
            totalFoodItems += 1;
        }
    }
    foreach (Order ord in rest.OrderQueue)
    {
        totalOrders += 1;
    }

}
Console.WriteLine("Welcome to the Gruberoo Food Delivery System");
Console.WriteLine($"{totalRestaurants} restaurants loaded!");
Console.WriteLine($"{totalFoodItems} food items loaded!");
Console.WriteLine($"{totalCustomers} customers loaded!");
Console.WriteLine($"{totalOrders} orders loaded!");

while (true)
{
    Console.WriteLine();
    Console.WriteLine("===== Gruberoo Food Delivery System =====");
    Console.WriteLine("1.List all restaurants and menu items");     // basic features
    Console.WriteLine("2.List all orders");
    Console.WriteLine("3.Create a new order");
    Console.WriteLine("4.Process an order");
    Console.WriteLine("5.Modify an existing order");
    Console.WriteLine("6.Delete an existing order");
    Console.WriteLine("7.Bulk process unprocessed orders for today");        // advanced features
    Console.WriteLine("8.Display total order amount");
    Console.WriteLine("0.Exit");
    Console.Write("Enter your choice: ");
    string choice = Console.ReadLine();
    if (choice == "1")
    {
        displayRestMenu();
        continue;
    }
    else if (choice == "2")
    {
        Console.WriteLine();
        ListAllOrders();
        continue;
    }
    else if (choice == "3")
    {
        Console.WriteLine();
        createnewOrder();
        continue;
    }
    else if (choice == "4")
    {
        Console.WriteLine();
        ProcessOrder();
        continue;
    }
    else if (choice == "5")
    {
        Console.WriteLine();
        modifyOrder();
        continue;
    }
    else if (choice == "6")
    {
        Console.WriteLine();
        DeleteOrder();
        continue;
    }
    else if (choice == "7")
    {
        Console.WriteLine();
        TodaysUnprocessedOrders();
        continue;
    }
    else if (choice == "8")
    {
        Console.WriteLine();
        displayTotalOrderAmt();
        continue;
    }
    else if (choice == "0")
    {
        string headers = "OrderId,CustomerEmail,RestaurantId,DeliveryDate,DeliveryTime,DeliveryAddress,CreatedDateTime,TotalAmount,Status,Items";
        File.WriteAllText("queue.csv", headers + "\n");
        File.WriteAllText("stack.csv", headers + "\n");
        foreach (Restaurant rest in restaurants)
        {
            foreach (Order order in rest.OrderQueue)
            {
                if (order.OrderStatus == "Pending" || order.OrderStatus == "Preparing")
                {
                    orderQueue.Enqueue(order);
                }
            }
            foreach (Order order in rest.OrderQueue)
            {
                if (order.OrderStatus == "Rejected" || order.OrderStatus == "Cancelled")
                {
                    refundStack.Push(order);
                }
            }
        }
        foreach (Order o in orderQueue)
        {
            string date = o.DeliveryDateTime.ToString("yyyy-MM-dd");
            string time = o.DeliveryDateTime.ToString("HH:mm");
            string line = $"{o.OrderId},{o.Customer.EmailAddress},{o.Restaurant.RestaurantId},{date},{time},{o.DeliveryAddress},{o.OrderDateTime},{o.OrderTotal},{o.OrderStatus},{o.OrderedFoodItems}";
            File.AppendAllText("queue.csv", line + "\n");

        }
        foreach (Order o in orderQueue)
        {
            string date = o.DeliveryDateTime.ToString("yyyy-MM-dd");
            string time = o.DeliveryDateTime.ToString("HH:mm");
            string line = $"{o.OrderId},{o.Customer.EmailAddress},{o.Restaurant.RestaurantId},{date},{time},{o.DeliveryAddress},{o.OrderDateTime},{o.OrderTotal},{o.OrderStatus},{o.OrderedFoodItems}";
            File.AppendAllText("stack.csv", line + "\n");
        }
        break;
    }
    else
    {
        Console.WriteLine("Invalid Choice.");
    }
}



// ===================================================== todo 1 (Rui Min) =====================================================
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
// ===================================================== todo 2 (Adawiyah) =====================================================
// load files (customers and orders)

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
// ===================================================== todo 3 (Adawiyah) =====================================================
void displayRestMenu()
{
    Console.WriteLine("All Restaurant and Menu Items");
    Console.WriteLine("=============================");
    foreach (Restaurant restaurant in restaurants)
    {
        Console.WriteLine($"Restaurant: {restaurant.RestaurantName} ({restaurant.RestaurantId})");
        foreach (Menu menu in restaurant.Menus)
        {
            foreach (FoodItem item in menu.FoodItems)
            {
                Console.WriteLine($"    - {item.ItemName}: {item.ItemDesc} - ${item.ItemPrice}");
            }
        }
    }
}

// ===================================================== todo 4 (Rui Min) =====================================================

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

    allOrders = allOrders.OrderBy(o => o.OrderId).ToList();

    Console.WriteLine("All Orders");
    Console.WriteLine("==========");
    Console.WriteLine($"{"Order ID",-8}\t{"Customer",-10}\t{"Restaurant",-13}\t{"Delivery Date/Time",-18}\t{"Amount",-10}\t{"Status",-9}");
    Console.WriteLine($"{"--------",-8}\t{"----------",-10}\t{"-------------",-13}\t{"------------------",-18}\t{"------",-10}\t{"---------",-9}");

    foreach (Order order in allOrders)
    {
        string restName = order.Restaurant.RestaurantName;
        int ordID = order.OrderId;
        string custName = order.Customer.CustomerName;
        string delTime = order.DeliveryDateTime.ToString("HH:mm");
        double amt = order.OrderTotal;
        string status = order.OrderStatus;

        Console.WriteLine($"{ordID,-8}\t{custName,-10}\t{restName,-13}\t{delTime,-18}\t{amt,-10:C}\t{status,-9}");
    }
}

// ===================================================== todo 5 (Adawiyah) =====================================================

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
    else if (request == "Y")
    {
        Console.WriteLine("Enter request: ");
        specialRequest = Console.ReadLine();
    }
    foreach (OrderedFoodItem item in orderedfood) // calculate the order total
    {
        total += item.CalculateSubtotal();
    }
    Console.WriteLine($"Order Total: ${total - 5} + $5.00(delivery) = ${total}");
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
    Order order = new Order(newOrderId, DateTime.Now, "Pending", deldt, delAddr, paymentmtd, true, cust, rest, null);
    cust.AddOrder(order); // add to cust's order
    rest.OrderQueue.Enqueue(order); // add to order queue
    Console.WriteLine($"Order {order.OrderId} created successfully! Status: {order.OrderStatus}");
    // Console.WriteLine($"Orders in queue for {rest.RestaurantId}: {rest.OrderQueue.Count}"); // test, remove after
}

// ===================================================== todo 6 (Rui Min) =====================================================
void ProcessOrder()
{
    Console.WriteLine("Process Order");
    Console.WriteLine("=============");
    Restaurant? restFound = null;
    while (restFound == null)
    {
        Console.Write("Enter Restaurant ID: ");
        string inputRestID = Console.ReadLine();
        Console.WriteLine();
        foreach (Restaurant rest in restaurants)
        {
            if (rest.RestaurantId == inputRestID)
            {
                restFound = rest;
            }
        }
        if (restFound == null)
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
        string time = order.DeliveryDateTime.ToString("HH:mm");
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
                Console.WriteLine("Invalid Choice.");
                continue;
            }
        }
    }
}

// ===================================================== todo 7 (Adawiyah) =====================================================
void modifyOrder()
{
    Console.WriteLine("Process Order");
    Console.WriteLine("-------------");
    Console.WriteLine("Enter customer email: ");
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
    bool pendingAvail = false;
    Console.WriteLine("Pending Orders:");
    foreach (Order order in cust.Orders)
    {
        if (order.OrderStatus == "Pending")
        {
            Console.WriteLine(order.OrderId);
            pendingAvail = true;
        }
    }
    if (pendingAvail == false)
    {
        Console.WriteLine("No pending orders");
        return;
    }
    Order selectOrder = null;
    while (true)
    {
        Console.WriteLine("Enter order ID: ");
        int orderid = Convert.ToInt32(Console.ReadLine());
        foreach (Order order in cust.Orders)
        {
            if (order.OrderId == orderid)
            {
                selectOrder = order;
                break;
            }
        }
        if (selectOrder == null)
        {
            Console.WriteLine("Invalid order ID.");
            continue;
        }
        break;
    }
    foreach (OrderedFoodItem ofi in selectOrder.OrderedFoodItems)
    {
        foreach (Menu m in selectOrder.Restaurant.Menus)
        {
            foreach (FoodItem fi in m.FoodItems)
            {
                if (fi.ItemName == ofi.ItemName)
                {
                    ofi.ItemPrice = fi.ItemPrice;
                    ofi.ItemDesc = fi.ItemDesc;
                    ofi.Customise = fi.Customise;
                    break;
                }
            }
        }
    }
    Console.WriteLine("Order Items");
    int i = 1;
    foreach (OrderedFoodItem item in selectOrder.OrderedFoodItems)
    {
        Console.WriteLine($"{i}. {item.ItemName} - {item.QtyOrdered}");
        i++;
    }
    Console.WriteLine("Address:" + "\n" + $"{selectOrder.DeliveryAddress}");
    Console.WriteLine("Delivery Date/Time:" + "\n" + $"{selectOrder.DeliveryDateTime:dd/MM/yyyy}," + $"{selectOrder.DeliveryDateTime:HH:mm}");
    Console.WriteLine("\n" + "Modify: [1]Items  [2]Address  [3]Delivery Time: ");
    int useroption = Convert.ToInt32(Console.ReadLine());
    while (true)
    {
        double oldTotal = selectOrder.OrderTotal;

        if (useroption == 1)
        {
            Console.WriteLine("Items:");
            int j = 1;
            foreach (OrderedFoodItem item in selectOrder.OrderedFoodItems)
            {
                Console.WriteLine($"{j}. {item.ItemName} - Qty: {item.QtyOrdered}");
                j++;
            }
            int modifyitemno;
            while (true)
            {
                Console.WriteLine("Select item number to modify: ");
                modifyitemno = Convert.ToInt32(Console.ReadLine());
                if (modifyitemno < 1 || modifyitemno > selectOrder.OrderedFoodItems.Count) //validation
                {
                    Console.WriteLine("Enter a valid item number!");
                    continue;
                }
                break;
            }
            OrderedFoodItem selecteditem = selectOrder.OrderedFoodItems[modifyitemno - 1];
            Console.WriteLine($"Item: {selecteditem.ItemName}" + "\n" + $"Qty: {selecteditem.QtyOrdered}");
            Console.WriteLine("Modify" + "\n" + "[1]Quantity [2]Remove this item [3]Add customisation: ");
            int choice = Convert.ToInt32(Console.ReadLine());

            while (true)
            {
                if (choice == 1)
                {
                    Console.WriteLine("Update quantity to: ");
                    int newqty = Convert.ToInt32(Console.ReadLine());
                    selecteditem.QtyOrdered = newqty;
                    Console.WriteLine("quantity updated!");
                    break;
                }
                else if (choice == 2)
                {
                    selectOrder.RemoveOrderedFoodItem(selecteditem);
                    Console.WriteLine("Item removed");
                    break;
                }
                else if (choice == 3)
                {
                    Console.WriteLine("Enter customisation: ");
                    selecteditem.Customise = Console.ReadLine();
                    break;
                }
                else
                {
                    Console.WriteLine("Invalid output");
                    continue;
                }
            }
            double newTotal = 5; //update order total
            foreach (OrderedFoodItem item in selectOrder.OrderedFoodItems)
            {
                newTotal += item.CalculateSubtotal();
            }
            selectOrder.OrderTotal = newTotal;
            if (selectOrder.OrderTotal > oldTotal)
            {
                Console.WriteLine($"Order total increased from ${oldTotal} to ${selectOrder.OrderTotal}");
                Console.WriteLine("Proceed to payment? [Y/N]: ");
                string pay = Console.ReadLine().ToUpper();
                if (pay == "Y")
                {
                    while (true)
                    {
                        Console.WriteLine("Payment Method:");
                        Console.WriteLine("[CC] Credit Card / [PP] PayPal / [CD] Cash on Delivery");
                        string method = Console.ReadLine().ToUpper();

                        if (method == "CC" || method == "PP" || method == "CD")
                        {
                            selectOrder.OrderPaymentMethod = method;
                            Console.WriteLine("Payment successful.");
                            break;
                        }
                        else
                        {
                            Console.WriteLine("Invalid payment method.");
                        }
                    }
                }
                else
                {
                    Console.WriteLine("Modification cancelled");
                    selectOrder.OrderTotal = oldTotal;
                    return;
                }
            }
            else
            {
                Console.WriteLine("Order updated successfully");
            }
            break;
        }
        else if (useroption == 2)
        {
            Console.WriteLine("Enter new address: ");
            string newaddr = Console.ReadLine();
            selectOrder.DeliveryAddress = newaddr;
            Console.WriteLine($"Address updated. New address: {selectOrder.DeliveryAddress}");
        }
        else if (useroption == 3)
        {
            Console.WriteLine("Enter new delivery time (hh:mm): ");
            DateTime newdeltime = Convert.ToDateTime(Console.ReadLine());
            DateTime updatedtime = new DateTime(selectOrder.DeliveryDateTime.Year, selectOrder.DeliveryDateTime.Month, selectOrder.DeliveryDateTime.Day, newdeltime.Hour, newdeltime.Minute, 0);
            selectOrder.DeliveryDateTime = updatedtime;
            Console.WriteLine($"Order {selectOrder.OrderId} updated. New delivery time: {selectOrder.DeliveryDateTime:HH:mm}");
        }
        else
        {
            Console.WriteLine("Invalid output");
            continue;
        }
        break;
    }
}

// ===================================================== todo 8 (Rui Min) =====================================================
void DeleteOrder()
{
    Console.WriteLine("Delete Order");
    Console.WriteLine("============");
    // valid email
    Customer? custFound = null;
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
                foreach (Order ord in cust.Orders)
                {
                    if (ord.OrderStatus == "Pending")
                    {
                        Console.WriteLine(ord.OrderId);
                    }
                }

            }
        }
        if (custFound == null)
        {
            Console.WriteLine("Invalid Email.");
            Console.WriteLine();
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
                string time = ord.DeliveryDateTime.ToString("HH:mm");
                Console.WriteLine($"Delivery date/time: {time}");
                Console.WriteLine($"Total Amount: {ord.CalculateOrderTotal():C}");
                Console.WriteLine($"Order Status: {ord.OrderStatus}");
            }
        }
        if (orderFound == null)
        {
            Console.WriteLine();
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

        if (inputOption == "N")
        {
            Console.WriteLine();
            validOption = true;
            Console.WriteLine($"Order {orderFound.OrderId} not deleted.");
        }
        else if (inputOption == "Y")
        {
            Console.WriteLine();
            validOption = true;
            orderFound.Restaurant.RefundOrders.Push(orderFound);
            orderFound.OrderStatus = "Cancelled";
            Console.WriteLine($"Order {orderFound.OrderId} cancelled. Refund of {orderFound.CalculateOrderTotal():C} processed.");
        }

        else
        {
            Console.WriteLine();
            Console.WriteLine("Invalid Option.");
        }
    }
}

// ===================================================== advanced feature a) (Rui Min) =====================================================

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
    foreach (Order order in allOrders)
    {
        if (order.OrderStatus == "Pending" && order.DeliveryDateTime.Date == DateTime.Today.Date)
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

// ===================================================== advanced feature b) (Adawiyah) =====================================================
void displayTotalOrderAmt()
{
    double totalorderAmt = 0;
    double totalRefund = 0;
    double delFee = 5;
    foreach (Restaurant rest in restaurants)
    {
        double restTotalAmt = 0;
        double restRefundAmt = 0;
        foreach (Order order in rest.OrderQueue)
        {
            if (order.OrderStatus == "Delivered")
            {
                restTotalAmt += order.OrderTotal - delFee;
            }
            else if (order.OrderStatus == "Rejected" || order.OrderStatus == "Cancelled")
            {
                restRefundAmt += order.OrderTotal;
            }
        }
        Console.WriteLine($"Restaurant: {rest.RestaurantName}");
        Console.WriteLine($"Total Order Amount: {restTotalAmt}");
        Console.WriteLine($"Total Refund Amount: {restRefundAmt}");
        Console.WriteLine();
        totalorderAmt += restTotalAmt;
        totalRefund += restRefundAmt;
    }
    double finalearning = totalorderAmt - totalRefund;
    Console.WriteLine($"Total Order Amount: {totalorderAmt}");
    Console.WriteLine($"Total Refund Amount: {totalRefund}");
    Console.WriteLine($"Final Amount Gruberoo Earning: {finalearning}");
}
