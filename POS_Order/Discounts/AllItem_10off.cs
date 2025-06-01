using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POS_Order.Discounts
{
    internal class AllItem_10off : Discount
    {
        public AllItem_10off(List<Item> items) : base(items)
        {

        }
        public override void GetResult(List<Item> items)
        {
            items.Add(new Item("(折扣)全場一律打9折", -(int)(double)(items.Sum(x => x.subtotal) * 0.1), 1));
        }
    }
}
