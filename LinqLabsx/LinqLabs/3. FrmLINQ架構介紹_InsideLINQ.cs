using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Starter
{
    public partial class FrmLINQ架構介紹_InsideLINQ : Form
    {
        public FrmLINQ架構介紹_InsideLINQ()
        {
            InitializeComponent();

            this.productsTableAdapter1.Fill(this.northwindDataSet1.Products);
        }

        private void button30_Click(object sender, EventArgs e)
        {
            ArrayList arrList = new ArrayList();
            arrList.Add(2);
            arrList.Add(3);

            var q = from n in arrList.Cast<int>()
                    where n > 1
                    select new { N = n };

           this.dataGridView1.DataSource= q.ToList();
        }

        private void button7_Click(object sender, EventArgs e)
        {
            var q = (from p in this.northwindDataSet1.Products
                    orderby p.UnitsInStock descending
                    select p).Take(5);

            this.dataGridView1.DataSource = q.ToList();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            //I. 延遲查詢 (deferred execution)
            //定義時不會估算
            //使用時才估算


            int[] nums = new int[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };

            int i = 0;
            var q = from n in nums
                    select ++i;

            //foreach 執行 Query
            foreach (var v in q)
            {
                listBox1.Items.Add(string.Format("v = {0}, i = {1}", v, i));
            }
            listBox1.Items.Add("===========================================");



            //=======================================================

            i = 0;
            var q1 = (from n in nums
                      select ++i).ToList();

            foreach (var v in q1)
            {
                listBox1.Items.Add(string.Format("v = {0}, i = {1}", v, i));
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            // when 執行 Query

            //var q = nums.Where(........

            //1. foreach 執行 Query
            //2. ToXXX()
            //3. Aggregation Min/Max 立即執行 (Immedate execution)


            int[] nums = { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };
            this.listBox1.Items.Add("sum = " + nums.Sum());
            this.listBox1.Items.Add("min = " + nums.Min());
            this.listBox1.Items.Add("max = " + nums.Max());
            this.listBox1.Items.Add("Avg = " + nums.Average());
            this.listBox1.Items.Add("Count = " + nums.Count());


            //var q = from n in nums
            //        where n % 2 == 0
            //        select n;
            //q.Sum();

            //nums.Median()
            //Python Pandas

            this.listBox1.Items.Add("Avg UnitPrice - " + this.northwindDataSet1.Products.Average(p => p.UnitPrice));
            this.listBox1.Items.Add("Max UnitPrice - " + this.northwindDataSet1.Products.Max(p => p.UnitPrice));
            this.listBox1.Items.Add("Min UnitPrice - " + this.northwindDataSet1.Products.Min(p => p.UnitPrice));
        }

        private void button54_Click(object sender, EventArgs e)
        {

        }
    }
}