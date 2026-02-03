using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PRG2_ASG_Gruberoo_Del_System
{
    class OrderedFoodItem : FoodItem
    {
        // attributes
        private int qtyOrdered;
        private double subTotal;
        // properties
        public int QtyOrdered
        {
            get { return qtyOrdered; }
            set { qtyOrdered = value; }
        }
        public double SubTotal
        {
            get { return subTotal; }
            set { subTotal = value; }
        }
        //default constructor
        // empty parameter
        public OrderedFoodItem() : base()
        {
            qtyOrdered = 0;
            subTotal = 0;
        }
        // parameterized constructor
        public OrderedFoodItem(string itemName, string itemDesc, double itemPrice, string customise, int qtyOrdered)
            : base(itemName, itemDesc, itemPrice, customise)
        {
            this.qtyOrdered = qtyOrdered;
            // call method
            this.subTotal = CalculateSubtotal();
        }
        // other methods
        public double CalculateSubtotal()
        {
            // calculate subtotal and return value
            subTotal = ItemPrice * qtyOrdered;
            return subTotal;
        }

        // overide tostring method?
        public override string ToString()
        {
            return base.ToString() 
                + "qty ordered: " + qtyOrdered 
                + "\n" + "subtotal: $" + subTotal;
        }
    }
}
