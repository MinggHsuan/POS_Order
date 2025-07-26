using POS_Order.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POS_Order.Strategies
{
    public class ItemPercentageDiscountStrategy : ADiscountStrategy
    {
        public ItemPercentageDiscountStrategy(MenuModel.Discount discountType, List<Item> items) : base(discountType, items)
        {
        }
        public override void Discount()
        {
            List<Conditionbox> condition = discountType.Conditions
              .SelectMany((x, index) => x.Name.Split('|')
              .Select(name => new Conditionbox(name, x.RequirAmount, index)))
              .ToList();

            List<Conditionbox> buyItem = FindSameItem.FindCommon(items, condition);

            var classify = buyItem.GroupBy(x => new { x.conditionID, x.conditionAmount })
                .Select(x => new
                {
                    totalAmount = x.Sum(y => y.amount),
                    requirAmount = x.Key.conditionAmount
                })
                .Where(x => (x.totalAmount / x.requirAmount) > 0)
                .ToList();

            if (classify.Count != discountType.Conditions.Length)
            {
                return;
            }

            int minCombo = classify.Min(x => x.totalAmount / x.requirAmount);

            var setPrice = MenuData.Menus.SelectMany(x => x.Foods)
              .Join(buyItem,
              menu => menu.Name,
              buy => buy.name,
              (menu, buy) => new
              {
                  name = menu.Name,
                  price = menu.Price,
                  buy.amount,
                  buy.conditionID,
                  subtotal = menu.Price * buy.amount,
              }).ToList();

            items.AddRange(discountType.Rewards.Select(x =>
            {
                int disprice = 0;
                if (x.RewardsOff != 0)
                {
                    disprice = (int)(float)(setPrice.Sum(y => y.subtotal) * (1 - x.RewardsOff));
                }
                return new Item($"(折扣)", -disprice, 1);
            }));
        }
    }
}
