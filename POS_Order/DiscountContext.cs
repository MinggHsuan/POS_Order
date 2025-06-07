using POS_Order.Discounts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POS_Order
{
    public class DiscountContext
    {
        public string discountType;
        public List<Item> items;
        Discount discount = null;
        public DiscountContext(string discountType, List<Item> items)
        {
            this.discountType = discountType;
            this.items = items;
            Type type = Type.GetType(discountType);
            discount = (Discount)Activator.CreateInstance(type, new object[] { items });
        }

        public void ReturnResult()
        {
            discount.GetResult(items);
        }
    }
}
