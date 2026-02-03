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

// stage 1
// load files (customers and orders)

// load, read, and create objects - customer
List<Customer> customerList = new List<Customer>();


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

