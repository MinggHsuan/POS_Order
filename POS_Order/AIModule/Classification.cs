using POS_Order.Models;
using POS_Order.Strategies;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static POS_Order.MenuModel;

namespace POS_Order.AIModule
{
    internal class Classification
    {
        public ADiscountStrategy discount = null;
        public Classification(MenuModel.Discount discountType, List<Item> items)
        {
            Type type = Type.GetType(discountType.Strategy);
            discount = (ADiscountStrategy)Activator.CreateInstance(type, new object[] { discountType, items });
        }
        public Classification(string ResponseSource, string diccountName, List<Item> items)
        {
            Type type = Type.GetType(ResponseSource);
            MenuModel.Discount dis = MenuData.Discounts.FirstOrDefault(x => x.Name == diccountName);
            discount = (ADiscountStrategy)Activator.CreateInstance(type, new object[] { dis, items });
        }
    }
}
