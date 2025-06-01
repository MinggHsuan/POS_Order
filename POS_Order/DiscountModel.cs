using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POS_Order
{
    internal class DiscountModel
    {
        public string Key { get; set; }
        public string Value { get; set; }
        public DiscountModel(string Key, string Value)
        {
            this.Key = Key;
            this.Value = Value;
        }
    }
}
