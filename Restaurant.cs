using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PRG2_ASG_Gruberoo_Del_System
{
    class Restaurant
    {
        // attributes
        private string restaurantId;
        private string restaurantName;
        private string restaurantEmail;
        // ref
        private List<Menu> menus;
        private Queue<Order> orderQueue;
        private List<FoodItem> foodItems = new List<FoodItem>();

        // properties
        public string RestaurantId
        {
            get { return restaurantId; }
            set { restaurantId = value; }
        }
        public string RestaurantName
        {
            get { return restaurantName; }
            set { restaurantName = value; }
        }
        public string RestaurantEmail
        {
            get { return restaurantEmail; }
            set { restaurantEmail = value; }
        }
        //ref
        public List<Menu> Menus
        {
            get { return menus; }
            set { menus = value; }
        }
        public Queue<Order> OrderQueue
        {
            get { return orderQueue; }
            set {  orderQueue = value; }
        }
        public List<FoodItem> FoodItems
        {
            get { return foodItems; }
            set { foodItems = value; }
        }

        // default constructors
        public Restaurant()
        {
            menus = new List<Menu>();
            orderQueue = new Queue<Order>();
        }
        // parameterized constructor
        public Restaurant(string rid, string rname, string remail)
        {
            RestaurantId = rid;
            RestaurantName = rname;
            RestaurantEmail = remail;
            menus = new List<Menu>();
            orderQueue = new Queue<Order>();
            FoodItems = new List<FoodItem>();
        }

        // other methods
        public void DisplayOrders()
        {
            foreach (Order order in orderQueue)
            {
                Console.WriteLine(order);
            }
        }
        public void ShowSpecialOffers()
        {
            foreach(Order order in orderQueue)
            {
                // check if the order has a special offer - if not null
                if (order.SpecialOffer != null)
                {
                    Console.WriteLine(order.SpecialOffer);
                }
            }
        }
        public void DisplayMenu()
        {
            foreach (Menu menu in menus) 
            {
                Console.WriteLine(menu);
                menu.DisplayFoodItems();
            }
        }
        public void AddMenu(Menu menu)
        {
            menus.Add(menu);
        }
        public bool RemoveMenu(Menu menu) 
        {
            return menus.Remove(menu);
        }

        //override tostring
        public override string ToString()
        {
            return "Restaurant ID: " + restaurantId + "\n"
                + "Restaurant Name: " + restaurantName + "\n"
                + "Restaurant Email: " + restaurantEmail + "\n";
        }
    }
}
