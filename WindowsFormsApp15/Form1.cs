using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp15
{
    public partial class Form1 : Form
    {
        List<string> name = new List<string>();
        List<int> weight = new List<int>();
        List<int> value = new List<int>();
        public Form1()
        {
            InitializeComponent();
        }
        private void button1_Click(object sender, EventArgs e)
        {
            if(string.IsNullOrEmpty(textBox1.Text) || string.IsNullOrEmpty(textBox2.Text) || string.IsNullOrEmpty(textBox3.Text)) { MessageBox.Show("Проверьте заполнены, ли все поля!", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error); }
            name.Add(textBox1.Text);
            weight.Add(Convert.ToInt32(textBox2.Text));
            value.Add(Convert.ToInt32(textBox3.Text));
            dataGridView1.Rows.Add(textBox1.Text, textBox2.Text, textBox3.Text);
            textBox1.Clear();
            textBox2.Clear();
            textBox3.Clear();
        }
        private void button2_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(textBox4.Text)) { MessageBox.Show("Введите вес рюкзака!!!!", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error); }
            int capacity = Convert.ToInt32(textBox4.Text);
            Dictionary<string, double> coeff = new Dictionary<string, double>();
            for (int i = 0; i < name.Count; i++) { coeff.Add(name[i], (float) value[i] / weight[i]); }
            Dictionary<string, double> sorted_coeff = new Dictionary<string, double>();
            foreach(KeyValuePair<string, double> cf in coeff.OrderByDescending(key => key.Value)) { sorted_coeff.Add(cf.Key, cf.Value); }
            for(int i = 0; i < name.Count; i++)
            {
                int max_count = capacity / weight[name.IndexOf(sorted_coeff.ElementAt(i).Key)];
                dataGridView2.Rows.Add(sorted_coeff.ElementAt(i).Key, max_count);
                capacity -= weight[name.IndexOf(sorted_coeff.ElementAt(i).Key)] * max_count;
            }
            dataGridView1.Sort(dataGridView1.Columns[0], ListSortDirection.Ascending);
            dataGridView2.Sort(dataGridView2.Columns[0], ListSortDirection.Ascending);
            int summ = 0;
            for(int i = 0; i < dataGridView1.Rows.Count; i++) { summ += Convert.ToInt32(dataGridView2.Rows[i].Cells[1].Value) * Convert.ToInt32(dataGridView1.Rows[i].Cells[2].Value); }
            label5.Text = "Общая стоимость = " + summ.ToString();
        }
    }
}