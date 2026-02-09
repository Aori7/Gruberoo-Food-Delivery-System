using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PRG2_ASG_Gruberoo_Del_System
{
    class FoodItem
    {
        // private attributes
        private string itemName;
        private string itemDesc;
        private double itemPrice;
        private string? customise;
        // properties
        public string ItemName
        {
            get { return itemName; }
            set { itemName = value; }
        }
        public string ItemDesc
        {
            get { return itemDesc; }
            set { itemDesc = value; }
        }
        public double ItemPrice
        {
            get { return itemPrice; }
            set { itemPrice = value; }
        }
        public string? Customise
        {
            get { return customise; }
            set { customise = value; }
        }

        // default constructor
        public FoodItem()
        {
        }
        // parameterized constructor
        public FoodItem(string itemName, string itemDesc, double itemPrice, string? customise)
        {
            ItemName = itemName;
            ItemDesc = itemDesc;
            ItemPrice = itemPrice;
            Customise = customise;
        }

        // to string
        public override string ToString()
        {
            return "Item Name: " + itemName + "\n" +
                   "Item Description: " + itemDesc + "\n" +
                   "Item Price: $" + itemPrice + "\n" +
                   "Customisation: " + customise + "\n";
        }

    }
}
