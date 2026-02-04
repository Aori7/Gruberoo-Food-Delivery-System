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
        private List<FoodItem> foodItems;
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

        //constructors
        //default constructor   
        public Menu()
        {
            FoodItems = new List<FoodItem>();
        }
        //parameterized constructor
        public Menu(string menuId, string menuName)
        {
            this.menuId = menuId;
            this.menuName = menuName;
            FoodItems = new List<FoodItem>();
        }

        //other methods
        public void AddFoodItem(FoodItem foodItem)
        {
            FoodItems.Add(foodItem);
        }
        public bool RemoveFoodItem(FoodItem foodItem)
        {
            return FoodItems.Remove(foodItem);
        }
        public void DisplayFoodItems()
        {
            foreach (FoodItem item in FoodItems) 
            {
                Console.WriteLine(item);
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
