// Student Number: S10275270A
// Student Name: Adawiyah
// Partner Name: Rui Min

using PRG2_ASG_Gruberoo_Del_System;

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

// STAGE 1
// feature 1 ====================================
// load files (customers and orders)

List<Customer> customerList = new List<Customer>();
List<Restaurant> restaurantList = new List<Restaurant>();


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
        int orderid = Convert.ToInt32(orderInfo[0]);
        string custemail = orderInfo[1];
        string restid = orderInfo[2];
        DateTime deldatetime = Convert.ToDateTime(orderInfo[3] + " " +  orderInfo[4]);
        string deladdr = orderInfo[5];
        DateTime orderdatetime = Convert.ToDateTime(orderInfo[6]);
        double ordertotal = Convert.ToDouble(orderInfo[7]);
        string stat = orderInfo[8];
        string[] items = orderInfo[9].Split("|");
        List<OrderedFoodItem> orderitems = new List<OrderedFoodItem>();
        foreach (string item in items)
        {
            string[] name_qty = item.Split(",");
            string name = name_qty[0].Trim();
            int qty = Convert.ToInt32(name_qty[1].Trim());
            orderitems.Add(new OrderedFoodItem(name,"", qty));
        }

        Order order = new Order();
        foreach(Customer cust in customerList)
        {
            cust.AddOrder( order);
        }

    }
}








// load,read, and create objects - order
//method to find customers by email (used for loadorder method)
//Customer findCustByEmail(string custemail)
//{
//    foreach (Customer custe in customerList)
//    {
//        if (custe.EmailAddress == custemail)
//            // return the customer if email matches
//            return custe;
//    }
//    return null;
//}
//Restaurant findRestById(string restId)
//{
//    foreach (Restaurant rest in restaurantList)
//    {
//        if (rest.RestaurantId == restId)
//            return rest;
//    }
//    return null;
//}
//void loadOrder()
//{
//    // read file
//    string[] csvLines = File.ReadAllLines("orders.csv");
//    // skip line 1
//    for (int i = 1; i < csvLines.Length; i++)
//    {
//        string[] orderInfo = csvLines[i].Split(",");

//        int orderId = Convert.ToInt32(orderInfo[0]);
//        string custemail = orderInfo[1];
//        string restId = orderInfo[2];
//        //DateTime delDate = Convert.ToDateTime(orderInfo[3]);
//        //DateTime delTime = Convert.ToDateTime(orderInfo[4]);
//        DateTime deldatetime = Convert.ToDateTime(orderInfo[3] + " " + orderInfo[4]);    // combine deldate + deltime tgt, order deliveryDateTime
//        string delAddr = orderInfo[5]; //order deliveryAddress
//        DateTime orderdatetime = Convert.ToDateTime(orderInfo[6]); //order orderDateTime
//        //double totalamt = Convert.ToDouble(orderInfo[7]); //order orderTotal - excluding total amount as it can be calculated with method?
//        string stat = orderInfo[8]; //order orderPaid bool

//        // find customer thru email
//        foreach(Customer cust in customerList)
//        {
//            if(cust.EmailAddress == custemail)
//            {
//                cust.AddOrder(order);
//            }
//        }
//        // using method
//        //Customer cust = findCustByEmail(custemail);
//        //if (cust == null)
//        //{
//        //    continue; // skip if customer not found
//        //}
//        //// find restaurant using method
//        //Restaurant rest = findRestById(restId);
//        //if (rest == null)
//        //{
//        //    continue; // skip if not found
//        //}

//        // item column of order.csv is delimited by |
//        //eg. "Chicken Katsu Bento, 1|Vegetable Tempura Bento, 1"
//        string itemtrim = orderInfo[9].Trim('"');         // trim the "" frst
//        string[] items = itemtrim.Split("|");         // split the items with |

//        // initialise order 
//        Order order = new Order(orderId,orderdatetime,stat,deldatetime,delAddr,"",false,cust,rest);

//        // now split each items into name and qty
//        foreach (string item in items) 
//        {
//            string[] itemparts = item.Split(',', 2);
           
//            if (itemparts.Length < 2)
//                continue;
//            string foodname = itemparts[0].Trim();
//            int qty = Convert.ToInt32(itemparts[1].Trim());
//            // initialise and set unknown to empty
//            OrderedFoodItem fooditem = new OrderedFoodItem(foodname,"",0.0,"",qty);

//            //add object to order
//            order.AddOrderedFoodItem(fooditem);
//        }
//        // add order to the customer
//        cust.AddOrder(order);
//        // add the order to the order queue
//        rest.OrderQueue.Enqueue(order);
//    }
//}
//// order.csv has 10 cols, last col split items by |
//loadOrder();


//// testing
//foreach(Customer cust in customerList)
//{
//    Console.WriteLine(cust);
//    cust.DisplayAllOrders();
//}