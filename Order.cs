using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace PRG2_ASG_Gruberoo_Del_System
{
    class Order
    {
        // attributes
        private int orderId;
        private DateTime orderDateTime;
        private double orderTotal;
        private string orderStatus;
        private DateTime deliveryDateTime;
        private string deliveryAddress;
        private string orderPaymentMethod;
        private bool orderPaid;
        //ref
        // orderedfooditem
        // special offer

        //properties
        public int OrderId
        {
            get { return orderId; }
            set { orderId = value; }
        }
        public DateTime OrderDateTime
        {
            get { return orderDateTime; }
            set { orderDateTime = value; }
        }
        public double OrderTotal
        {
            get { return orderTotal; }
            set { orderTotal = value; }
        }
        public string OrderStatus
        {
            get { return OrderStatus; }
            set { OrderStatus = value; }
        }
        public DateTime DeliveryDateTime
        {
            get { return  deliveryDateTime; }
            set { deliveryDateTime = value; }
        }
        public string OrderPaymentMethod
        {
            get { return orderPaymentMethod; }
            set { orderPaymentMethod = value; }
        }
        public bool OrderPaid
        {
            get { return orderPaid; }
            set { orderPaid = value; }
        }
        // ref

        //constructor
        //default constructor
        public Order()
        {

        }
        // parameterized constructor
        public Order(int id, DateTime odt, double ot, string stat, DateTime ddt, string add, string pym, bool op)
        {
            orderId = id;
            orderDateTime = odt;
            orderTotal = ot;
            orderStatus = stat;
            deliveryDateTime = ddt;
            orderPaymentMethod = add;
            orderPaid = op;
        }

        //other methods
        public double CalculateOrderTotal()
        {

        }
        public void AddOrderedFoodItem() // ref method
        {

        }
        public bool RemoveOrderedFoodItem() // ref method
        {

        }
        public void DisplayOrderedFoodItems()
        {

        }
        // override tostring
        public override string ToString()
        {
            return "OrderID: " + orderId + "\n"
                + "Order Date Time: " + orderDateTime + "\n"
                + "Order Total :$ " + orderTotal + "\n"
                + "Order Status: " + orderStatus + "\n"
                + "Delivery Date Time: " + deliveryDateTime + "\n"
                + "Delivery Address: " + deliveryAddress + "\n"
                + "Order Payment Method: " + orderPaymentMethod + "\n"
                + "Order Paid: " + orderPaid + "\n";
        }
    }
}
