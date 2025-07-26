using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POS_Order.Strategies
{
    public class OrderAmountDiscountStrategy : ADiscountStrategy
    {
        public OrderAmountDiscountStrategy(MenuModel.Discount discountType, List<Item> items) : base(discountType, items)
        {
        }

        public override void Discount()
        {
            if (items.Count == 0)
            {
                return;
            }
            int reqPrice = discountType.Conditions.Select(price => price.RequirPrice).First();
            if (reqPrice > items.Sum(total => total.subtotal))
            {
                return;
            }
            items.AddRange(discountType.Rewards.Select(x =>
            {
                int disprice = 0;
                int amount = 0;
                if (x.RewardsPrice != 0)
                {
                    disprice = x.RewardsPrice;
                    amount = items.Sum(total => total.subtotal) / reqPrice;
                }
                if (x.RewardsOff != 0)
                {
                    disprice = -(int)(items.Sum(y => y.subtotal) * (1.0 - x.RewardsOff));
                    amount = 1;
                }
                return new Item($"(折扣)", disprice, amount);
            }));
        }
    }
}
