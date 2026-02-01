using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PRG2_ASG_Gruberoo_Del_System
{
    class FoodItem
    {
        // properties
        private string itemName;
        private string itemDesc;
        private double itemPrice;
        private string customise;

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
        public string Customise
        {
            get { return customise; }
            set { customise = value; }
        }

        // default constructor
        public FoodItem()
        {
            this.itemName = "";
            this.itemDesc = "";
            this.itemPrice = 0.0;
            this.customise = "";
        }
        // constructor
        public FoodItem(string itemName, string itemDesc, double itemPrice, string customise)
        {
            this.itemName = itemName;
            this.itemDesc = itemDesc;
            this.itemPrice = itemPrice;
            this.customise = customise;
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
