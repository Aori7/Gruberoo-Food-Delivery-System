using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PRG2_ASG_Gruberoo_Del_System
{
    class Customer
    {
        //att
        private string emailAddress;
        private string customerName;
        //ref
        private List<Order> orders;

        // properties
        public string EmailAddress
        {
            get {  return emailAddress; }
            set { emailAddress = value; }
        }
        public string CustomerName
        {
            get { return customerName; }
            set { customerName = value; }
        }
        //ref
        public List<Order> Orders
        {
            get { return orders; }
            set { orders = value; }
        }

        // constructor
        // default
        public Customer()
        {
            orders = new List<Order>();
        }

        // parameterized constructor
        public Customer(string email, string name)
        {
            EmailAddress = email;
            CustomerName = name;
            orders = new List<Order>();
        }
        //other methods
        public void AddOrder(Order order)
        {
            orders.Add(order);
        }
        public void DisplayAllOrders()
        {
            foreach (Order order in orders)
            {
                Console.WriteLine(order);
            }
        }
        public bool RemoveOrder(Order order)
        {
            return orders.Remove(order);
        }
        // to string
        public override string ToString()
        {
            return "Customer Email: " + emailAddress
                + "\n"
                + "Customer Name: " + customerName + "\n";
        }

    }
}
