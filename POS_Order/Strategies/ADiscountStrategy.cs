using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POS_Order.Strategies
{
    public abstract class ADiscountStrategy
    {
        protected List<Item> items;
        protected MenuModel.Discount discountType;
        public ADiscountStrategy(MenuModel.Discount discountType, List<Item> items)
        {
            this.discountType = discountType;
            this.items = items;
        }
        public abstract void Discount();

    }
}
