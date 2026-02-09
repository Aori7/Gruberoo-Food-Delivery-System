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
        // ref
        private Order ord;

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
        public Order Ord
        {
            get { return ord; }
            set { ord = value; }
        }

        //default constructor
        // empty parameter
        public OrderedFoodItem(Order ord) : base()
        {
            QtyOrdered = 0;
            SubTotal = 0;
            Ord = ord;
        }
        // parameterized constructor
        public OrderedFoodItem(string itemName, string itemDesc, double itemPrice, string? customise, int qtyOrdered)
            : base(itemName, itemDesc, itemPrice, customise)
        {
            this.qtyOrdered = qtyOrdered;
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
