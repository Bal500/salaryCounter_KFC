using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace salaryCounter
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        static int allMinutes, allHours, subtractedMinutes;

        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox1.Text == "" || textBox2.Text == "" || textBox3.Text == "" || textBox4.Text == "" || comboBox1.SelectedIndex == -1)
            {
                MessageBox.Show("Nem töltötted ki mindegyik mezőt!", "Hiba");
                return;
            }

            string defSalary = comboBox1.GetItemText(comboBox1.SelectedItem);
            string[] splittedSalary = defSalary.Split(' ');

            int defMoney = int.Parse(splittedSalary[0]);
            int defMoneyMinutes = defMoney / 60;

            double nightMoney = defMoney * 1.3;
            double nightMoneyMinutes = nightMoney / 60;

            if (int.Parse(textBox1.Text) > int.Parse(textBox3.Text))
            {
                int actual = int.Parse(textBox3.Text) + 24;
                textBox3.Text = actual.ToString();
            }

            if (int.Parse(textBox1.Text) < 18 && int.Parse(textBox3.Text) > 18)
            {
                int minutes = (int.Parse(textBox1.Text) * 60) + int.Parse(textBox2.Text);
                int dayTime = (18 * 60) - minutes;
                int dayTimeSalary = dayTime * defMoneyMinutes;
                if (checkBox1.Checked)
                {
                    dayTimeSalary *= 2;
                }
                listBox1.Items.Add(dayTimeSalary.ToString() + " Ft");
                allMinutes += dayTime;

                int nightMinutes = (int.Parse(textBox3.Text) * 60) + int.Parse(textBox4.Text);
                int nightTime = nightMinutes - 18 * 60;
                double nightTimeSalary = nightTime * nightMoneyMinutes;
                if (int.Parse(textBox3.Text) - int.Parse(textBox1.Text) > 7)
                {
                    nightTimeSalary -= nightMoneyMinutes * 30;
                }
                if (checkBox1.Checked)
                {
                    nightTimeSalary *= 2;
                }
                listBox2.Items.Add(nightTimeSalary.ToString() + " Ft");
                allMinutes += nightTime;
            }

            if (int.Parse(textBox1.Text) < 18 && int.Parse(textBox3.Text) < 18)
            {
                int minutes = ((int.Parse(textBox3.Text) * 60) + int.Parse(textBox4.Text)) - ((int.Parse(textBox1.Text) * 60) + int.Parse(textBox2.Text));
                int dayTimeSalary = minutes * defMoneyMinutes;
                if (int.Parse(textBox3.Text) - int.Parse(textBox1.Text) > 7)
                {
                    dayTimeSalary -= defMoneyMinutes * 30;
                }
                if (checkBox1.Checked)
                {
                    dayTimeSalary *= 2;
                }
                listBox1.Items.Add(dayTimeSalary.ToString() + " Ft");
                allMinutes += minutes;
            }

            if (int.Parse(textBox1.Text) > 18)
            {
                int minutes = ((int.Parse(textBox3.Text) * 60) + int.Parse(textBox4.Text)) - ((int.Parse(textBox1.Text) * 60) + int.Parse(textBox2.Text));
                double nightTimeSalary = minutes * nightMoneyMinutes;
                if (int.Parse(textBox3.Text) - int.Parse(textBox1.Text) > 7)
                {
                    nightTimeSalary -= defMoneyMinutes * 30;
                }
                if (checkBox1.Checked)
                {
                    nightTimeSalary *= 2;
                }
                listBox1.Items.Add(nightTimeSalary.ToString() + " Ft");
                allMinutes += minutes;
            }

            textBox1.Text = "";
            textBox2.Text = "";
            textBox3.Text = "";
            textBox4.Text = "";
            checkBox1.Checked = false;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            listBox1.Items.Clear();
            listBox2.Items.Clear();
            comboBox1.SelectedItem = null;
            allHours = 0;
            allMinutes = 0;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            List<int> money = new List<int>();
            int salary = 0;
            for (int i = 0; i < listBox1.Items.Count; i++)
            {
                string item = listBox1.Items[i].ToString();
                string[] split = item.Split(' ');
                int moneyValue = int.Parse(split[0]);
                salary += moneyValue;
            }

            for (int i = 0; i < listBox2.Items.Count; i++)
            {
                string item = listBox2.Items[i].ToString();
                string[] split = item.Split(' ');
                int moneyValue = int.Parse(split[0]);
                salary += moneyValue;
            }

            if (checkBox2.Checked)
            {
                salary -= 1000;
            }

            if (allMinutes > 450)
            {
                allMinutes -= 30;
            }

            if (allMinutes % 60 != 0)
            {
                int i = 1;
                while (allMinutes % 60 != 0)
                {
                    allMinutes -= i;
                    subtractedMinutes += i;
                }
            }

            allHours = allMinutes / 60;

            MessageBox.Show("A fizetésed: " + salary + " Ft lesz! (" + allHours + " óra " + subtractedMinutes + " perc)", "Összegzés");
        }
    }
}