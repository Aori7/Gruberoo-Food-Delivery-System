using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PRG2_ASG_Gruberoo_Del_System
{
    class SpecialOffer
    {
        //attributes
        private string offerCode;
        private string offerDesc;
        private double discount;
        // ref
        private Restaurant rest;
        private List<Order> orderList;

        //properties
        public string OfferCode 
        {   get { return offerCode; } 
            set { offerCode = value; } 
        }
        public string OfferDesc 
        {
            get { return offerDesc; } 
            set { offerDesc = value; } 
        }
        public double Discount 
        {
            get { return discount; } 
            set { discount = value; }
        }
        //ref
        public Restaurant Rest
        {
            get { return rest; }
            set { rest = value; }
        }
        public List<Order> OrderList
        {
            set {  orderList = value; }
            get { return orderList; }
        }

        // default constructor
        public SpecialOffer() 
        {
        }
        // parameterized constructor
        public SpecialOffer(string oc, string od, double d, Restaurant rest) 
        {
            OfferCode = oc;
            OfferDesc = od;
            Discount = d;
            Rest = rest;
            OrderList = new List<Order>();
        }

        // to string method
        public override string ToString()
        {
            return "Offer Code: " + offerCode + "\n" +
                "Offer Description: " + offerDesc + "\n" +
                "Discount: " + discount + "\n";
        }
    }
}
