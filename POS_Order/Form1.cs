using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
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
            //string input = "HELLO123ABC456";
            //int count = input.GetDigitalCount(); // Extension.GetDigitalCount(input)
            //Console.WriteLine(count);
            string[] mainFoods = { "雞腿飯$90", "雞排飯$85", "排骨飯$70" };
            string[] sideFoods = { "薯條$40", "雞塊$50", "薯球$20" };
            string[] dessert = { "草莓蛋糕$100", "巧克力冰$95", "聖代$200" };
            string[] drinks = { "紅茶$20", "綠茶$30", "奶茶$40" };

            flowLayoutPanel1.AutoGenerate(mainFoods, Checkbox_CheckedChange, Numberic_ValueChange);
            flowLayoutPanel2.AutoGenerate(sideFoods, Checkbox_CheckedChange, Numberic_ValueChange);
            flowLayoutPanel3.AutoGenerate(dessert, Checkbox_CheckedChange, Numberic_ValueChange);
            flowLayoutPanel4.AutoGenerate(drinks, Checkbox_CheckedChange, Numberic_ValueChange);

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

            FlowLayoutPanel panelbox = new FlowLayoutPanel();
            panelbox.Name = checkbox.Text;
            panelbox.Width = flowLayoutPanel5.Width;
            panelbox.Height = 20;
            if (!checkbox.Checked || numericUpDown.Value == 0)
            {
                flowLayoutPanel5.Controls.RemoveByKey(panelbox.Name);
                result = flowLayoutPanel1.AutoClac() + flowLayoutPanel2.AutoClac() +
                flowLayoutPanel3.AutoClac() + flowLayoutPanel4.AutoClac();
                totalLab.Text = result.ToString();
                result = 0;
                return;
            }

            if (!flowLayoutPanel5.Controls.ContainsKey(panelbox.Name))
            {
                flowLayoutPanel5.Controls.Add(panelbox);
            }

            if (flowLayoutPanel5.Controls.ContainsKey(panelbox.Name))
            {
                flowLayoutPanel5.Controls.RemoveByKey(panelbox.Name);
                flowLayoutPanel5.Controls.Add(panelbox);
            }

            Label item = new Label();
            item.Text = checkbox.Text.Split('$')[0];
            item.Width = 70;

            Label price = new Label();
            int intprice = int.Parse(checkbox.Text.Split('$')[1]);
            price.Text = intprice.ToString() + "元";
            price.Width = 70;

            Label amount = new Label();
            int intamount = int.Parse(numericUpDown.Value.ToString());
            amount.Text = intamount.ToString() + "個";
            amount.Width = 70;

            Label subtotal = new Label();
            subtotal.Text = (intprice * intamount).ToString() + "元";
            subtotal.Width = 65;

            panelbox.Controls.Add(item);
            panelbox.Controls.Add(price);
            panelbox.Controls.Add(amount);
            panelbox.Controls.Add(subtotal);

            result = flowLayoutPanel1.AutoClac() + flowLayoutPanel2.AutoClac() +
                flowLayoutPanel3.AutoClac() + flowLayoutPanel4.AutoClac();
            totalLab.Text = result.ToString();
            result = 0;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            result = flowLayoutPanel1.AutoClac() + flowLayoutPanel2.AutoClac() +
                flowLayoutPanel3.AutoClac() + flowLayoutPanel4.AutoClac();
            totalLab.Text = result.ToString();
            result = 0;
        }


    }
}
