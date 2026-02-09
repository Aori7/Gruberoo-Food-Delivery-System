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
        private List<Menu> menus = new List<Menu>();
        private Queue<Order> orderQueue;
        private List<SpecialOffer> specialOfferList;

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
        public List<SpecialOffer> SpecialOfferList
        {
            get { return specialOfferList; }
            set { specialOfferList = value; }
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
            if (Menus.Count == 0)
            {
                Menus.Add(new Menu()); 
            }
            OrderQueue = new Queue<Order>();
            SpecialOfferList = new List<SpecialOffer>();
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
            if(SpecialOfferList.Count == 0)
            {
                Console.WriteLine("No special offers");
            }
            else
            {
                foreach(SpecialOffer so in SpecialOfferList)
                {
                    so.ToString();
                }
            }
        }
        public void DisplayMenu()
        {
            foreach (Menu menu in Menus) 
            {
                Console.WriteLine(menu);
                menu.DisplayFoodItems();
            }
        }
        public void AddMenu(Menu menu)
        {
            Menus.Add(menu);
        }
        public bool RemoveMenu(Menu menu) 
        {
            if(Menus.Count == 0)
            {
                Console.WriteLine("No menus to remove");
                return false;
            }
            else
            {
                foreach(Menu m in Menus)
                {
                    if(m == menu)
                    {
                        Menus.Remove(menu);
                        return true;
                    }
                }
            }
            return false;
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
