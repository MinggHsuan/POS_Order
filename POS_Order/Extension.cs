using System;
using System.Collections.Generic;
using System.Drawing.Printing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace POS_Order
{


    public static class Extension
    {
        public static int GetDigitalCount(this string input)
        {
            int count = 0;
            for (int i = 0; i < input.Length; i++)
            {
                if (Char.IsDigit(input[i]))
                {
                    count++;
                }
            }
            return count;
        }

        public static void AutoGenerate(this FlowLayoutPanel flowLayoutPanel, MenuModel menuModel, EventHandler checkedChanged, EventHandler valueChanged)
        {
            for (int i = 0; i < menuModel.Menus.Length; i++)
            {
                FlowLayoutPanel panelbox = new FlowLayoutPanel();
                panelbox.Width = 180;
                panelbox.Height = 200;
                Label label = new Label();
                label.Width = 40;
                label.Text = menuModel.Menus[i].Title;
                panelbox.Controls.Add(label);
                for (int j = 0; j < menuModel.Menus[i].Foods.Length; j++)
                {
                    FlowLayoutPanel panel = new FlowLayoutPanel();
                    panel.Width = 180;
                    panel.Height = 30;
                    CheckBox checkBox = new CheckBox();
                    checkBox.CheckedChanged += checkedChanged;
                    NumericUpDown numericUpDown = new NumericUpDown();
                    numericUpDown.ValueChanged += valueChanged;
                    checkBox.Text = menuModel.Menus[i].Foods[j].Name + "$" + menuModel.Menus[i].Foods[j].Price;
                    checkBox.Width = 100;
                    numericUpDown.Width = 60;
                    panel.Controls.Add(checkBox);
                    panel.Controls.Add(numericUpDown);
                    panelbox.Controls.Add(panel);
                }
                flowLayoutPanel.Controls.Add(panelbox);
            }

        }

        //public static int AutoClac(this FlowLayoutPanel flowLayoutPanel)
        //{
        //    int result = 0;
        //    //var panel = flowLayoutPanel.Controls;
        //    for (int i = 0; i < flowLayoutPanel.Controls.Count; i++)
        //    {
        //        if (i < 4)
        //        {
        //            continue;
        //        }
        //        //FlowLayoutPanel flowLayoutPanel1 = (FlowLayoutPanel)panel[i];
        //        FlowLayoutPanel flowLayoutPanel1 = (FlowLayoutPanel)flowLayoutPanel.Controls[i];
        //        Label label = (Label)flowLayoutPanel1.Controls[3];
        //        //FlowLayoutPanel item = (FlowLayoutPanel)flowLayoutPanel.Controls[i];

        //        result += int.Parse(label.Text.Split('元')[0]);
        //    }
        //    return result;
        //}

    }
}
