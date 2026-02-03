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
        private List<OrderedFoodItem> orderedFoodItems;
        //customer
        private Customer customer;
        // special offer
        private SpecialOffer specialOffer;
        //restaurant
        private Restaurant restaurant;

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
            get { return orderStatus; }
            set { orderStatus = value; }
        }
        public DateTime DeliveryDateTime
        {
            get { return deliveryDateTime; }
            set { deliveryDateTime = value; }
        }
        public string DeliveryAddress
        {
            get { return deliveryAddress; }
            set { deliveryAddress = value; }
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
        public List<OrderedFoodItem> OrderedFoodItems
        {
            get { return  orderedFoodItems; }
            set { orderedFoodItems = value; }
        }
        public Customer Customer
        {
            get { return customer; }
            set {  customer = value; }
        }
        public SpecialOffer SpecialOffer
        {
            get { return specialOffer; }
            set { specialOffer = value; }
        }
        public Restaurant Restaurant
        {
            get { return restaurant; }
            set { restaurant = value; }
        }

        //constructor
        //default constructor
        public Order()
        {
            orderedFoodItems = new List<OrderedFoodItem>();
        }
        // parameterized constructor
        public Order(int id, DateTime odt, string stat, DateTime ddt, string add, string pym, bool op, Customer cust, Restaurant rest)
        {
            OrderId = id;
            OrderDateTime = odt;
            OrderTotal = 0.0;
            OrderStatus = stat;
            DeliveryDateTime = ddt;
            DeliveryAddress = add;
            OrderPaymentMethod = pym;
            OrderPaid = op;
            orderedFoodItems = new List<OrderedFoodItem>();

            customer = cust;
            restaurant = rest;
        }

        //other methods
        public double CalculateOrderTotal()
        {
            double ordertotal = 0.0;
            foreach (OrderedFoodItem item in orderedFoodItems) 
            {
                ordertotal += item.CalculateSubtotal();
            }
            orderTotal = ordertotal;
            return ordertotal;
        }
        public void AddOrderedFoodItem(OrderedFoodItem orderedFoodItem) // ref method
        {
            orderedFoodItems.Add(orderedFoodItem);
            CalculateOrderTotal();
        }
        public bool RemoveOrderedFoodItem(OrderedFoodItem orderedFoodItem) // ref method
        {
            // return true if removed
            bool orderremoved = orderedFoodItems.Remove(orderedFoodItem);
            // calc new total
            CalculateOrderTotal();
            return orderremoved;
        }
        public void DisplayOrderedFoodItems()
        {
            foreach(OrderedFoodItem item in orderedFoodItems)
            {
                Console.WriteLine(item);
            }
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
