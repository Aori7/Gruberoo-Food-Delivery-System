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
        private List<OrderedFoodItem> orderedFoodItems;
        private Customer customer;
        private SpecialOffer specialOffer;
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

        //constructors
        //default constructor
        public Order()
        {
            OrderedFoodItems = new List<OrderedFoodItem>();
        }
        // parameterized constructor
        public Order(int id, DateTime odt, string stat, DateTime ddt, string add, string pym, bool op, Customer cust, Restaurant rest, SpecialOffer? so)
        {
            OrderId = id;
            OrderDateTime = odt;
            OrderTotal = 0;
            OrderStatus = stat;
            DeliveryDateTime = ddt;
            DeliveryAddress = add;
            OrderPaymentMethod = pym;
            OrderPaid = op;

            OrderedFoodItems = new List<OrderedFoodItem>();
            Customer = cust;
            Restaurant = rest;
            SpecialOffer = so;
        }

        // methods
        public double CalculateOrderTotal()
        {
            double Ordertotal = 0;
            foreach (OrderedFoodItem item in orderedFoodItems) 
            {
                Ordertotal += item.CalculateSubtotal();
            }
            return Ordertotal;
        }
        public void AddOrderedFoodItem(OrderedFoodItem orderedFoodItem) // ref method
        {
            orderedFoodItems.Add(orderedFoodItem);
        }
        public bool RemoveOrderedFoodItem(OrderedFoodItem orderedFoodItem) // ref method
        {
            if(OrderedFoodItems.Count == 0)
            {
                return false;
            }
            else
            {
                foreach(OrderedFoodItem ordFI  in OrderedFoodItems)
                {
                    if(ordFI == orderedFoodItem)
                    {
                        OrderedFoodItems.Remove(orderedFoodItem);
                        return true;
                    }
                }
            }
            return false;
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
