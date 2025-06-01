using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POS_Order.Discounts
{
    internal class RibsWithFries_100 : Discount
    {
        public RibsWithFries_100(List<Item> items) : base(items)
        {

        }
        public override void GetResult(List<Item> items)
        {
            var buyItems = items.Where(x => x.name == "排骨飯" || x.name == "薯條").ToList();
            if (buyItems.Count == 2)
                items.Add(new Item("(折扣)排骨飯搭配薯條100元", -10, buyItems.Min(x => x.amount)));
        }
    }
}
