// Student Number: S10275270A
// Student Name: Adawiyah
// Partner Name: Rui Min

using PRG2_ASG_Gruberoo_Del_System;
using System.Diagnostics.CodeAnalysis;
using System.Text;
using System.IO;

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
//program
List<Restaurant> restaurants = new List<Restaurant>();
List<Customer> customerList = new List<Customer>();
loadCust();
loadOrder();
displayRestMenu();
string[] lines = File.ReadAllLines("orders.csv");
string[] lastRow = lines[lines.Length - 1].Split(',');
int newOrderId = Convert.ToInt32(lastRow[0]) + 1; // generate a new order id 
createnewOrder();
modifyOrder();
displayTotalOrderAmt();

// Partner todo 1: load files (restaurants and food items) 
// feature 1 ====================================
// todo 2: load files (customers and orders)
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

// feature 2 ====================================
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

// Partner todo 4: list all orders with basic information
//feature 3 ====================================
// todo 5: create a new order
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
    DateTime deliveryDateTime = deldt.Date + deltime.TimeOfDay;
    Order order = new Order(newOrderId, DateTime.Now, "Pending", deliveryDateTime, delAddr,paymentmtd,true,cust,rest,null);
    order.OrderTotal = total;
    cust.AddOrder(order); // add to cust's order
    rest.OrderQueue.Enqueue(order); // add to order queue
    foreach (OrderedFoodItem item in orderedfood)
    {
        order.AddOrderedFoodItem(item);
    }
    Console.WriteLine($"Order {order.OrderId} created successfully! Status: {order.OrderStatus}");
    string itemStr = "";
    for (int i = 0; i < orderedfood.Count; i++) 
    {
        itemStr += orderedfood[i].ItemName + "," + orderedfood[i].QtyOrdered;
        if (i < orderedfood.Count - 1)
        {
            itemStr +="|";
        }
    }
    string csvline =
    $"{order.OrderId}," +
    $"{cust.EmailAddress}," +
    $"{rest.RestaurantId}," +
    $"{deldt:dd/MM/yyyy}," +
    $"{deltime:HH:mm}," +
    $"\"{delAddr}\"," +
    $"{order.OrderDateTime:dd/MM/yyyy HH:mm}," +
    $"{order.OrderTotal}," +
    $"{order.OrderStatus}," +
    $"\"{itemStr}\"";
    using (StreamWriter sw = new StreamWriter("orders.csv", true))
    {
        sw.WriteLine(csvline);
    }
}

// Parter todo 6: prompt an order
// feature 4 ====================================
// todo 7: modify an existing order
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
    foreach(Order order in cust.Orders)
    {
        if (order.OrderStatus == "Pending")
        {
            Console.WriteLine(order.OrderId);
            pendingAvail = true;
        }
    }
    if(pendingAvail == false)
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
    foreach(OrderedFoodItem item in selectOrder.OrderedFoodItems)
    {
        Console.WriteLine($"{i}. {item.ItemName} - {item.QtyOrdered}");
        i++;
    }
    Console.WriteLine("Address:" + "\n" + $"{selectOrder.DeliveryAddress}");
    Console.WriteLine("Delivery Date/Time:" + "\n" + $"{selectOrder.DeliveryDateTime:dd/MM/yyyy},"+$"{selectOrder.DeliveryDateTime:HH:mm}");
    Console.WriteLine("\n"+"Modify: [1]Items  [2]Address  [3]Delivery Time: ");
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
            OrderedFoodItem selecteditem = selectOrder.OrderedFoodItems[modifyitemno-1];
            Console.WriteLine($"Item: {selecteditem.ItemName}"+"\n"+$"Qty: {selecteditem.QtyOrdered}");
            Console.WriteLine("Modify"+"\n"+ "[1]Quantity [2]Remove this item [3]Add customisation: ");
            int choice = Convert.ToInt32(Console.ReadLine());
            
            while (true)
            {
                if(choice == 1)
                {
                    Console.WriteLine("Update quantity to: ");
                    int newqty = Convert.ToInt32(Console.ReadLine());
                    selecteditem.QtyOrdered = newqty;
                    Console.WriteLine("quantity updated!");
                    break;
                }
                else if(choice == 2)
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
        else if(useroption == 2)
        {
            Console.WriteLine("Enter new address: ");
            string newaddr = Console.ReadLine();
            selectOrder.DeliveryAddress = newaddr;
            Console.WriteLine($"Address updated. New address: {selectOrder.DeliveryAddress}");
        }
        else if(useroption == 3)
        {
            Console.WriteLine("Enter new delivery time (hh:mm): ");
            DateTime newdeltime = Convert.ToDateTime(Console.ReadLine());
            DateTime updatedtime = new DateTime(selectOrder.DeliveryDateTime.Year,selectOrder.DeliveryDateTime.Month,selectOrder.DeliveryDateTime.Day,newdeltime.Hour,newdeltime.Minute,0);
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
// Partner todo 8: delete an existing order
// Partner advanced feature (a) =================
// advanced feature (b) =========================
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
