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

        // default constructors
        public Restaurant()
        {
            menus = new List<Menu>();
        }
        // parameterized constructor
        public Restaurant(string rid, string rname, string remail)
        {
            restaurantId = rid;
            restaurantName = rname;
            restaurantEmail = remail;
            menus = new List<Menu>();
        }

        // other methods
        public void DisplayOrders()
        {

        }
        public void ShowSpecialOffers()
        {

        }
        public void DisplayMenu()
        {

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
