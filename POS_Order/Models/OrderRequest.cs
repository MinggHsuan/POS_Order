using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POS_Order.Models
{
    public class OrderRequest
    {
        public Item item;
        public bool AiRecommand;
        public MenuModel.Discount Discount;
        public List<Item> items = new List<Item>();
        public OrderRequest(Item item, MenuModel.Discount Discount)
        {
            this.item = item;
            this.AiRecommand = false;
            this.Discount = Discount;
        }
        public OrderRequest(MenuModel.Discount Discount)
        {
            this.AiRecommand = false;
            this.Discount = Discount;
        }
        public OrderRequest()
        {
            this.AiRecommand = true;
        }
    }
}
