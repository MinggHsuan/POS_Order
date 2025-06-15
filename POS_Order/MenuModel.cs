using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POS_Order
{
    internal class MenuModel
    {
        public Menu[] Menus { get; set; }
        public Discount[] Discounts { get; set; }

        public class Menu
        {
            public string Title { get; set; }
            public Food[] Foods { get; set; }
        }

        public class Food
        {
            public string Name { get; set; }
            public int Price { get; set; }
        }

        public class Discount
        {
            public string Name { get; set; }
            public string Strategy { get; set; }
            public Condition[] Conditions { get; set; }
            public Reward[] Rewards { get; set; }
        }

        public class Condition
        {
            public string Name { get; set; }
            public int RequirAmount { get; set; }
            public int RequirPrice { get; set; }
        }

        public class Reward
        {
            public string Name { get; set; }
            public object RewardType { get; set; }
            public int RewardsAmount { get; set; }
            public int RewardsPrice { get; set; }
            public float RewardsOff { get; set; }
        }

    }
}
