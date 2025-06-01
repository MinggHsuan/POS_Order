using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POS_Order.Discounts
{
    internal class Chicken_BuyTwoOneFree : Discount
    {
        public Chicken_BuyTwoOneFree(List<Item> items) : base(items)
        {

        }
        public override void GetResult(List<Item> items)
        {
            var buyItems = items.Where(x => x.name == "雞腿飯" && x.amount >= 2).ToList();
            if (buyItems.Count > 0)
                items.Add(new Item("(折扣)雞腿飯買二送一", -90, buyItems[0].amount / 3));
        }
    }
}
