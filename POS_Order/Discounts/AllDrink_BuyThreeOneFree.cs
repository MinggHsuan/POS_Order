using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POS_Order.Discounts
{
    internal class AllDrink_BuyThreeOneFree : Discount
    {
        public AllDrink_BuyThreeOneFree(List<Item> items) : base(items)
        {

        }
        public override void GetResult(List<Item> items)
        {
            var buyItems = items.Where(x => x.name == "紅茶" || x.name == "綠茶" || x.name == "奶茶").ToList();
            if (buyItems.Count > 0 && buyItems.Sum(x => x.amount) / 3 > 0)
                items.Add(new Item("(贈送)所有飲料買三杯就送一杯", -(buyItems.Min(x => x.price)), buyItems.Sum(x => x.amount) / 3));
        }
    }
}
