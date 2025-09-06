using POS_Order.Models;
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
        public static async Task AddOrder(OrderRequest order)
        {
            for (int i = 0; i < items.Count; i++)
            {
                if (order.item.amount == 0 && items[i].name == order.item.name)
                {
                    items.Remove(items[i]);
                    order.items = items;
                    await Discount.DisCountOrder(order);
                    return;
                }

                if (items[i].name == order.item.name)
                {
                    items[i].amount = order.item.amount;
                    items[i].subtotal = order.item.subtotal;
                    order.items = items;
                    await Discount.DisCountOrder(order);
                    return;
                }
            }
            items.Add(order.item);
            order.items = items;
            await Discount.DisCountOrder(order);
        }

        public static async Task Checkout(OrderRequest order)
        {
            order.items = items;
            await Discount.DisCountOrder(order);

        }


    }
}
