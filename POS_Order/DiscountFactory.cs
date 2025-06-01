using POS_Order.Discounts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POS_Order
{
    internal class DiscountFactory
    {
        public Discount GetDiscount(string discountType, List<Item> items)
        {
            Discount discount = null;
            switch (discountType)
            {
                case "雞腿飯買二送一":
                    discount = new Chicken_BuyTwoOneFree(items);
                    break;
                case "雞排飯買三個打85折":
                    discount = new Chicken_15off(items);
                    break;
                case "排骨飯搭配薯條100元":
                    discount = new RibsWithFries_100(items);
                    break;
                case "雞排飯搭配聖代送紅茶一杯":
                    discount = new ChickenWithSundae_BlackTea(items);
                    break;
                case "雞排飯搭配薯條打95折":
                    discount = new ChickenWithFries_5off(items);
                    break;
                case "飲料均一價20元":
                    discount = new AllDrink_20NT(items);
                    break;
                case "所有飲料買三杯就送一杯(送最便宜品項)":
                    discount = new AllDrink_BuyThreeOneFree(items);
                    break;
                case "排骨飯搭配任一種配餐送奶茶":
                    discount = new RibsWithSide_MilkTea(items);
                    break;
                case "全場消費滿500折100":
                    discount = new AllItemOver500_100(items);
                    break;
                case "全場一律打9折":
                    discount = new AllItem_10off(items);
                    break;
            }
            return discount;
        }
    }
}
