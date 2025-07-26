using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static POS_Order.MenuModel;

namespace POS_Order.Models
{
    internal class MenuData
    {
        public static Menu[] Menus { get; set; }
        public static MenuModel.Discount[] Discounts { get; set; }

        static MenuData()
        {
            string menupath = ConfigurationManager.AppSettings["MenuPath"];
            string menujson = File.ReadAllText(menupath, Encoding.UTF8);
            MenuModel menuModel = JsonConvert.DeserializeObject<MenuModel>(menujson);

            Menus = menuModel.Menus;
            Discounts = menuModel.Discounts;
        }

    }
}
