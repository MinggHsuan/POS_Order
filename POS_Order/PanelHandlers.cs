﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace POS_Order
{
    public class PanelHandlers
    {
        public static EventHandler<TotalPrice> Handler;

        public static void Notify(TotalPrice box)
        {
            Handler?.Invoke(null, box);
        }
    }
}
