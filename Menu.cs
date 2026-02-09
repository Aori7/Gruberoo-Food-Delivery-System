using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PRG2_ASG_Gruberoo_Del_System
{
    class Menu
    {
        //private attributes
        private string menuId;
        private string menuName;
        //ref
        private List<FoodItem> foodItems;
        private Restaurant restaurant;

        // properties
        public string MenuId
        {
            get { return menuId; }
            set { menuId = value; }
        }
        public string MenuName 
        {   get { return menuName; }
            set { menuName = value; } 
        }
        //reference collection 
        public List<FoodItem> FoodItems
        {
            get { return foodItems; }
            set { foodItems = value; }
        }
        public Restaurant Restaurant
        {
            get { return restaurant;}
            set { restaurant = value; }
        }

        //constructors
        //default constructor   
        public Menu()
        {
            FoodItems = new List<FoodItem>();
            Restaurant = new Restaurant();
        }
        //parameterized constructor
        public Menu(string id, string name)
        {
            MenuId = id;
            MenuName = name;
            FoodItems = new List<FoodItem>();
            Restaurant = new Restaurant();
        }

        //other methods
        public void AddFoodItem(FoodItem foodItem)
        {
            FoodItems.Add(foodItem);
        }
        public bool RemoveFoodItem(FoodItem foodItem)
        {
            if (FoodItems.Count == 0)
            {
                Console.WriteLine("No food items to remove");
                return false;
            }
            else
            {
                foreach (FoodItem fi in FoodItems)
                {
                    if (fi == foodItem)
                    {
                        FoodItems.Remove(foodItem);
                        return true;
                    }
                }
            }
            return false;
        }
        public void DisplayFoodItems()
        {
            if(FoodItems.Count == 0)
            {
                Console.WriteLine("no food items in this menu");
            }
            else
            {
                foreach (FoodItem item in FoodItems)
                {
                    Console.WriteLine(item);
                }
            }    
        }
        //to string method
        public override string ToString()
        {
            return "Menu ID: " + menuId + "\n"
                + "Menu Name: " + menuName + "\n";
        }
    }
}
