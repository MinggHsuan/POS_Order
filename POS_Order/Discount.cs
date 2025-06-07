using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace POS_Order
{
    public abstract class Discount
    {
        protected List<Item> items;
        protected Discount(List<Item> items)
        {
            this.items = items;
        }
        public abstract void GetResult(List<Item> items);

        public static void DisCountOrder(string discountType, List<Item> items)
        {
            items.RemoveAll(x => x.name.Contains("贈送") || x.name.Contains("折扣"));
            if (discountType == "")
            {
                ShowPanel.BuildUp(items);
            }
            //DiscountFactory discountFactory = new DiscountFactory();
            //Discount discount = discountFactory.GetDiscount(discountType, items);
            DiscountContext discountContext = new DiscountContext(discountType, items);
            discountContext.ReturnResult();
            ShowPanel.BuildUp(items);

        }
    }
}
