using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace POS_Order
{


    internal static class Extension
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

        public static void AutoGenerate(this FlowLayoutPanel flowLayoutPanel, string[] item, EventHandler checkedChanged, EventHandler valueChanged)
        {
            for (int i = 0; i < item.Length; i++)
            {
                FlowLayoutPanel panel = new FlowLayoutPanel();
                panel.Width = flowLayoutPanel.Width;
                panel.Height = 30;
                flowLayoutPanel.Controls.Add(panel);
                for (int j = 0; j < item.Length; j++)
                {
                    CheckBox checkBox = new CheckBox();
                    checkBox.CheckedChanged += checkedChanged;
                    NumericUpDown numericUpDown = new NumericUpDown();
                    numericUpDown.ValueChanged += valueChanged;
                    checkBox.Text = item[i];
                    checkBox.Width = 100;
                    numericUpDown.Width = 60;
                    panel.Controls.Add(checkBox);
                    panel.Controls.Add(numericUpDown);
                    break;
                }
            }


        }
        public static int AutoClac(this FlowLayoutPanel flowLayoutPanel)
        {
            int result = 0;
            int price = 0;
            var panel = flowLayoutPanel.Controls;
            for (int i = 0; i < panel.Count; i++)
            {
                FlowLayoutPanel flowLayoutPanel1 = (FlowLayoutPanel)panel[i];
                CheckBox checkBox = (CheckBox)flowLayoutPanel1.Controls[0];
                NumericUpDown numericUpDown = (NumericUpDown)flowLayoutPanel1.Controls[1];
                if (checkBox.Checked)
                {
                    price = int.Parse(checkBox.Text.Split('$')[1]);
                    result += int.Parse(numericUpDown.Value.ToString()) * price;
                }
            }
            return result;
        }
        //public static int DynamicGeneration(this FlowLayoutPanel flowLayoutPanel)
        //{

        //}

    }
}
