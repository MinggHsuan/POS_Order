using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Menu;

namespace POS_Order.Strategies
{
    public class ComboGiftStrategy : ADiscountStrategy
    {
        public ComboGiftStrategy(MenuModel.Discount discountType, List<Item> items) : base(discountType, items)
        {
        }

        public override void Discount()
        {
            List<Item> buyItem = new List<Item>();
            List<Item> buySideItem = new List<Item>();
            List<int> minComboAmount = new List<int>();

            for (int i = 0; i < discountType.Conditions.Length; i++)
            {
                if (discountType.Conditions[i].Name.Contains('|'))
                {
                    string[] nameType = discountType.Conditions[i].Name.Split('|');
                    for (int j = 0; j < nameType.Length; j++)
                    {
                        Item Sideitem = items.Find(x => x.name == nameType[j]);
                        if (Sideitem == null)
                        {
                            continue;
                        }
                        buySideItem.Add(Sideitem);
                    }
                    if (buySideItem.Count == 0)
                    {
                        return;
                    }

                    minComboAmount.Add((buySideItem.Sum(x => x.amount)) / discountType.Conditions[i].RequirAmount);
                    continue;
                }
                Item Item = items.Find(x => x.name == discountType.Conditions[i].Name && x.amount >= discountType.Conditions[i].RequirAmount);
                if (Item == null)
                {
                    return;
                }
                buyItem.Add(Item);
                minComboAmount.Add(Item.amount / discountType.Conditions[i].RequirAmount);
            }



            for (int i = 0; i < discountType.Rewards.Length; i++)
            {
                items.Add(new Item($"(贈送){discountType.Rewards[i].Name}", 0, minComboAmount.Min() * discountType.Rewards[i].RewardsAmount));
            }

        }
    }
}
