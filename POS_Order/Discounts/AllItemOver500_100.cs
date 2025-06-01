using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POS_Order.Discounts
{
    internal class AllItemOver500_100 : Discount
    {
        public AllItemOver500_100(List<Item> items) : base(items)
        {

        }
        public override void GetResult(List<Item> items)
        {
            if (items.Sum(x => x.subtotal) >= 500)
                items.Add(new Item("(折扣)全場消費滿500折100", -100, items.Sum(x => x.subtotal) / 500));
        }
    }
}
