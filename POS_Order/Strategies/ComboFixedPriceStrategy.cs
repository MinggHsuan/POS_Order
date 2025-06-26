using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POS_Order.Strategies
{
    public class ComboFixedPriceStrategy : ADiscountStrategy
    {
        public ComboFixedPriceStrategy(MenuModel.Discount discountType, List<Item> items) : base(discountType, items)
        {
        }

        public override void Discount()
        {
        }
    }
}
