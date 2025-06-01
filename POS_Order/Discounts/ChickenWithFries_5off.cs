using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POS_Order.Discounts
{
    internal class ChickenWithFries_5off : Discount
    {
        public ChickenWithFries_5off(List<Item> items) : base(items)
        {

        }
        public override void GetResult(List<Item> items)
        {
            var buyItems = items.Where(x => x.name == "雞排飯" || x.name == "薯條").ToList();
            if (buyItems.Count == 2)
                items.Add(new Item("(折扣)雞排飯搭配薯條打95折", -(int)(double)(buyItems[0].price * 0.05 +
                buyItems[1].price * 0.05), buyItems.Min(x => x.amount)));
        }
    }
}
