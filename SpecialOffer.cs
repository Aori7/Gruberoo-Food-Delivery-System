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


        // default constructor
        public SpecialOffer() 
        {
        }
        // parameterized constructor
        public SpecialOffer(string oc, string od, double d) 
        {
            offerCode = oc;
            offerDesc = od;
            discount = d;
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
