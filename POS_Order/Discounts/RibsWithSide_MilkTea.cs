using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POS_Order.Discounts
{
    internal class RibsWithSide_MilkTea : Discount
    {
        public RibsWithSide_MilkTea(List<Item> items) : base(items)
        {

        }
        public override void GetResult(List<Item> items)
        {
            var buyItems = items.Where(x => x.name == "排骨飯").ToList();
            var buysideItems = items.Where(x => x.name == "薯條" || x.name == "薯球" || x.name == "雞塊").ToList();
            if (buyItems.Count > 0 && buysideItems.Count > 0)
                items.Add(new Item("(贈送)排骨飯搭配任一種配餐就送奶茶", 0, Math.Min(buysideItems.Sum(x => x.amount), buyItems[0].amount)));
        }
    }
}
