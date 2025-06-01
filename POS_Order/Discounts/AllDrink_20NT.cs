using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POS_Order.Discounts
{
    internal class AllDrink_20NT : Discount
    {
        public AllDrink_20NT(List<Item> items) : base(items)
        {

        }
        public override void GetResult(List<Item> items)
        {
            var buyItems = items.Where(x => x.name == "紅茶" || x.name == "綠茶" || x.name == "奶茶").ToList();
            if (buyItems.Count > 0)
                items.Add(new Item("(折扣)飲料均一價20元",
                    -(buyItems.Sum(x => x.subtotal) - buyItems.Sum(x => x.amount) * 20), 1));
        }
    }
}
