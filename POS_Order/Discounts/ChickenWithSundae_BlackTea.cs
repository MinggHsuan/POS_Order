using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POS_Order.Discounts
{
    internal class ChickenWithSundae_BlackTea : Discount
    {
        public ChickenWithSundae_BlackTea(List<Item> items) : base(items)
        {

        }
        public override void GetResult(List<Item> items)
        {
            var buyItems = items.Where(x => x.name == "雞排飯" || x.name == "聖代").ToList();
            if (buyItems.Count == 2)
                items.Add(new Item("(贈送)雞排飯搭配聖代送紅茶一杯", 0, buyItems.Min(x => x.amount)));
        }
    }
}
