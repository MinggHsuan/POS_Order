using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace POS_Order
{
    public class TotalPrice
    {
        public FlowLayoutPanel panel;
        public int total;

        public TotalPrice(FlowLayoutPanel panel, int total)
        {
            this.panel = panel;
            this.total = total;
            PanelHandlers.Notify(this);
        }
    }
}
