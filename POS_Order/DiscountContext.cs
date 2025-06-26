using POS_Order.Discounts;
using POS_Order.Strategies;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POS_Order
{
    public class DiscountContext
    {
        public MenuModel.Discount discountType;
        public List<Item> items;
        ADiscountStrategy discount = null;
        public DiscountContext(MenuModel.Discount discountType, List<Item> items)
        {
            this.discountType = discountType;
            this.items = items;
            Type type = Type.GetType(discountType.Strategy);
            discount = (ADiscountStrategy)Activator.CreateInstance(type, new object[] { discountType, items });
        }

        public void ReturnResult()
        {
            discount.Discount();
        }
    }
}
