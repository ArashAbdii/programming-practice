using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace shop
{
    public partial class Form1 : Form
    {
        int total;
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'iranShop1DataSet.product' table. You can move, or remove it, as needed.
            this.productTableAdapter.Fill(this.iranShop1DataSet.product);

        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int index = dataGridView1.CurrentCell.RowIndex;
            textBox3.Text = dataGridView1.Rows[index].Cells[0].Value.ToString();
            textBox4.Text = dataGridView1.Rows[index].Cells[1].Value.ToString();
            textBox5.Text = dataGridView1.Rows[index].Cells[2].Value.ToString();
            textBox6.Text = dataGridView1.Rows[index].Cells[3].Value.ToString();
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            productTableAdapter.FillByproduct_name(iranShop1DataSet.product, textBox2.Text);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            productTableAdapter.InsertQuery(int.Parse(textBox3.Text), textBox4.Text, int.Parse(textBox5.Text), int.Parse(textBox6.Text));
            productTableAdapter.Fill(iranShop1DataSet.product);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            productTableAdapter.UpdateQuery(textBox4.Text, int.Parse(textBox5.Text), int.Parse(textBox6.Text), int.Parse(textBox3.Text));
            productTableAdapter.Fill(iranShop1DataSet.product);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            productTableAdapter.DeleteQuery(int.Parse(textBox3.Text));
            productTableAdapter.Fill(iranShop1DataSet.product);
        }

        private void button4_Click(object sender, EventArgs e)
        {
            textBox3.Text = textBox4.Text = textBox5.Text = textBox6.Text = "";
        }

        private void button11_Click(object sender, EventArgs e)
        {
            productBindingSource.MoveFirst();
        }

        private void button12_Click(object sender, EventArgs e)
        {
            productBindingSource.MoveFirst();
        }

        private void button13_Click(object sender, EventArgs e)
        {
            productBindingSource.MoveNext();
        }

        private void button14_Click(object sender, EventArgs e)
        {
            productBindingSource.MoveLast();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            tabControl1.SelectedTab = tabPage1;
            textBox7.Text = textBox3.Text;
            label18.Text = textBox4.Text;
            label20.Text = textBox5.Text;
            label22.Text = textBox6.Text;
        }

        private void button7_Click(object sender, EventArgs e)
        {
            int n = dataGridView2.Rows.Count - 1;
            dataGridView2.Rows.Add();
            dataGridView2.Rows[n].Cells[0].Value = textBox7.Text;
            dataGridView2.Rows[n].Cells[1].Value = label18.Text;
            dataGridView2.Rows[n].Cells[2].Value = textBox8.Text;
            dataGridView2.Rows[n].Cells[3].Value = label20.Text;
            dataGridView2.Rows[n].Cells[4].Value = (int.Parse(textBox8.Text) * int.Parse(label20.Text)).ToString();

            int totalPrice = int.Parse(textBox8.Text) * int.Parse(label20.Text);
             total += totalPrice;
            label24.Text = total.ToString();
        }

        private void button15_Click(object sender, EventArgs e)
        {
            tabControl1.SelectedTab = tabPage5;
        }

        private void dataGridView2_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dataGridView2.Columns[e.ColumnIndex] is DataGridViewImageColumn && e.RowIndex >= 0)
                dataGridView2.Rows.RemoveAt(e.RowIndex);
        }
    }
}
