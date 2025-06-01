using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POS_Order.Discounts
{
    internal class Chicken_15off : Discount
    {
        public Chicken_15off(List<Item> items) : base(items)
        {

        }
        public override void GetResult(List<Item> items)
        {
            var buyItems = items.Where(x => x.name == "雞排飯" && x.amount >= 3).ToList();
            if (buyItems.Count > 0 && buyItems[0].amount % 3 == 0)
                items.Add(new Item("(折扣)雞排飯買三個打85折", -(int)(double)(buyItems[0].price * 0.15), buyItems[0].amount));
            if (buyItems.Count > 0 && buyItems[0].amount % 3 != 0)
                items.Add(new Item("(折扣)雞排飯買三個打85折", -(int)(double)(buyItems[0].price * 0.15), buyItems[0].amount - (buyItems[0].amount % 3)));
        }
    }
}
