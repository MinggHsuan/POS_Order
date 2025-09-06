using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace POS_Order
{
    public class RenderData
    {
        public FlowLayoutPanel panel;
        public int total;
        public string reason;
        public string discountName;

        public RenderData(FlowLayoutPanel panel, int total, string discountName, string reason)
        {
            this.panel = panel;
            this.total = total;
            this.reason = reason;
            this.discountName = discountName;
        }
    }
}
