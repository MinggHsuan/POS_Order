using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace POS_Order
{
    public class Order
    {
        static List<Item> items = new List<Item>();
        public static void AddOrder(string discountType, Item item)
        {
            for (int i = 0; i < items.Count; i++)
            {
                if (item.amount == 0 && items[i].name == item.name)
                {
                    items.Remove(items[i]);
                    Discount.DisCountOrder(discountType, items);
                    return;
                }

                if (items[i].name == item.name)
                {
                    items[i].amount = item.amount;
                    items[i].subtotal = item.subtotal;
                    Discount.DisCountOrder(discountType, items);
                    return;
                }
            }
            items.Add(item);
            Discount.DisCountOrder(discountType, items);
        }

        public static void Checkout(string discountType)
        {
            Discount.DisCountOrder(discountType, items);

        }



    }
}
