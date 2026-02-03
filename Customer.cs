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


        // constructor
        // default
        public Customer() { }

        // parameterized constructor
        public Customer(string email, string name)
        {
            emailAddress = email;
            customerName = name;
        }
        //other methods
        public void AddOrder()
        {

        }
        public void DisplayAllOrders()
        {

        }
        public bool RemoveOrder()
        {

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
