using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
namespace POS_Order
{
    public partial class Form1 : Form
    {
        int result;
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

            string menupath = ConfigurationManager.AppSettings["MenuPath"];
            string menujson = File.ReadAllText(menupath, Encoding.UTF8);
            MenuModel menuModel = JsonConvert.DeserializeObject<MenuModel>(menujson);


            string[] mainFoods = { "雞腿飯$90", "雞排飯$85", "排骨飯$70" };
            string[] sideFoods = { "薯條$40", "雞塊$50", "薯球$20" };
            string[] dessert = { "草莓蛋糕$100", "巧克力冰$95", "聖代$200" };
            string[] drinks = { "紅茶$20", "綠茶$30", "奶茶$40" };


            flowLayoutPanel1.AutoGenerate(mainFoods, Checkbox_CheckedChange, Numberic_ValueChange);
            flowLayoutPanel2.AutoGenerate(sideFoods, Checkbox_CheckedChange, Numberic_ValueChange);
            flowLayoutPanel3.AutoGenerate(dessert, Checkbox_CheckedChange, Numberic_ValueChange);
            flowLayoutPanel4.AutoGenerate(drinks, Checkbox_CheckedChange, Numberic_ValueChange);
            PanelHandlers.Handler += GetPanel;

            Dictionary<string, string> dict = new Dictionary<string, string>
              {
                  { "雞腿飯買二送一", "POS_Order.Discounts.Chicken_BuyTwoOneFree" },
                  { "雞排飯買三個打85折", "POS_Order.Discounts.Chicken_15off" },
                  { "排骨飯搭配薯條100元", "POS_Order.Discounts.RibsWithFries_100" },
                  { "雞排飯搭配聖代送紅茶一杯", "POS_Order.Discounts.ChickenWithSundae_BlackTea" },
                  { "雞排飯搭配薯條打95折", "POS_Order.Discounts.ChickenWithFries_5off" },
                  { "飲料均一價20元", "POS_Order.Discounts.AllDrink_20NT" },
                  { "所有飲料買三杯就送一杯(送最便宜品項)", "POS_Order.Discounts.AllDrink_BuyThreeOneFree" },
                  { "排骨飯搭配任一種配餐就送奶茶", "POS_Order.Discounts.RibsWithSide_MilkTea" },
                  { "全場消費滿500折100", "POS_Order.Discounts.AllItemOver500_100" },
                  { "全場一律打9折", "POS_Order.Discounts.AllItem_10off" }
              };
            List<DiscountModel> discountModels = new List<DiscountModel>();
            foreach (var item in dict)
            {
                discountModels.Add(new DiscountModel(item.Key, item.Value));
            }

            comboBox1.DataSource = discountModels;
            comboBox1.DisplayMember = "Key";
            comboBox1.ValueMember = "Value";
            //comboBox1.SelectedIndex = 1;
        }

        private void Checkbox_CheckedChange(object sender, EventArgs e)
        {
            CheckBox checkBox = (CheckBox)sender;
            FlowLayoutPanel panel = (FlowLayoutPanel)checkBox.Parent;
            NumericUpDown numericUpDown = (NumericUpDown)panel.Controls[1];
            numericUpDown.Value = checkBox.Checked == true ? 1 : 0;

        }

        private void Numberic_ValueChange(object sender, EventArgs e)
        {

            NumericUpDown numericUpDown = (NumericUpDown)sender;
            FlowLayoutPanel panel = (FlowLayoutPanel)numericUpDown.Parent;
            CheckBox checkbox = (CheckBox)panel.Controls[0];
            checkbox.Checked = numericUpDown.Value < 1 ? false : true;

            Item itembox = new Item(checkbox.Text.Split('$')[0],
                int.Parse(checkbox.Text.Split('$')[1]),
                int.Parse(numericUpDown.Value.ToString()));
            Order.AddOrder(comboBox1.SelectedValue.ToString(), itembox);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //result = flowLayoutPanel5.AutoClac();
            //totalLab.Text = result.ToString();
            //result = 0;
        }
        private void GetPanel(object sender, TotalPrice box)
        {
            flowLayoutPanel5.Controls.Clear();
            flowLayoutPanel5.Controls.Add(box.panel);
            totalLab.Text = box.total.ToString();
            box.total = 0;
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox1.SelectedValue is string discountType)
            {
                Order.Checkout(discountType);
            }
        }
    }
}

