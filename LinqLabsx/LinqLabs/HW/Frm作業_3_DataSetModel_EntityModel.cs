using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using LinqLabs;

namespace LinqLabs.HW
{
    public partial class Frm作業_3_DataSetModel_EntityModel : Form
    {
        public Frm作業_3_DataSetModel_EntityModel()
        {
            InitializeComponent();
            productsTableAdapter1.Fill(northwindDataSet1.Products);
            ordersTableAdapter1.Fill(northwindDataSet1.Orders);
        
        }

        private void button4_Click(object sender, EventArgs e)
        {
            int[] nums = { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15 };
            Dictionary<int, string> dic = new Dictionary<int, string>(); 
            
            foreach (int n in nums)
            {
                if (n<=5)
                {
                    dic.Add(n, "small");
                }
                else if(n<=10)
                {
                    dic.Add( n,"medium");
                }
                else
                {
                    dic.Add( n,"big");
                }


            }

            dataGridView1.DataSource = dic;
            


        }

        private void button38_Click(object sender, EventArgs e)
        {
            DirectoryInfo dir = new DirectoryInfo(@"c:\windows");
            FileInfo[] files=dir.GetFiles();
            var q = from f in files
                    group f by f.Length > 100000 ? "大檔案" : "小檔案" into g
                    select new
                    {
                        g.Key,
                        MyCount = g.Count()
                    };


            dataGridView1.DataSource = q.ToList();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            DirectoryInfo dir = new DirectoryInfo(@"c:\windows");
            FileInfo[] files = dir.GetFiles();

            var q = from x in files
                    group x by x.CreationTime.Year > 2017 ? "近期" : "久遠" into g
                    select new
                    {
                        g.Key,
                        Mycount = g.Count()
                    };
            
            dataGridView1.DataSource = q.ToList();

        }

        private void button10_Click(object sender, EventArgs e)
        {
            this.ordersTableAdapter1.Fill(this.northwindDataSet1.Orders);
            var q = from o in this.northwindDataSet1.Orders
                    group o by o.OrderDate.Year into g
                    select new
                    {
                        訂單年份 = g.Key,
                        訂單數 = g.Count(),
                        MyGroup = g,
                        MyMonthGroup = (from m in g
                                        group m by m.OrderDate.Month into Month
                                        select new { MKey = Month.Key, MCount = Month.Count() })
                    };

            
            this.dataGridView1.DataSource = q.ToList();

            this.treeView1.Nodes.Clear();
            foreach (var group in q)
            {
                string s = $"{group.訂單年份} ({group.訂單數})";
                TreeNode x = this.treeView1.Nodes.Add(s);

                foreach (var m in group.MyMonthGroup)
                {
                    TreeNode y = x.Nodes.Add($"{m.MKey}月");
                    foreach (var item in group.MyGroup)
                    {
                        y.Nodes.Add($"{item.OrderID} - {item.OrderDate}");
                    }
                }

            }
        }

        private void button8_Click(object sender, EventArgs e)
        {
            //product 價格高中低
            var q = from x in northwindDataSet1.Products
                    group x by myUnitPrice(x.UnitPrice) into g
                    select new
                    {
                        g.Key,
                        Mycount=g.Count(),
                        g
                    };
            dataGridView1.DataSource = q.ToList(); 


            foreach(var g in q)
            {
                TreeNode x=treeView1.Nodes.Add(g.Key);
                foreach(var p in g.g)
                {
                    x.Nodes.Add(p.UnitPrice.ToString());
                }
            }
        }

        private string myUnitPrice(decimal unitPrice)
        {
            if (unitPrice<50)
            {
                return "低";
            }
            else if(unitPrice<100)
            {
                return "中";
            }
            else
            {
                return "高";
            }
        }

        private void button15_Click(object sender, EventArgs e)
        {
            //orders groupby 年
        }
        double sum = 0;
        private void button2_Click(object sender, EventArgs e)
        {
            //myUnitPrice*quantity

            var q = from od in dbContext.Order_Details
                    select od;

            foreach(var item in q)
                sum +=(double)item.UnitPrice+item.Quantity;

            MessageBox.Show("Total:"+sum);

        }
        NorthwindEntities dbContext = new NorthwindEntities();
        private void button1_Click(object sender, EventArgs e)
        {
            var q1 = from os in dbContext.Orders
                    select new
                    {   os.Employee.FirstName,
                        os.Order_Details.FirstOrDefault().UnitPrice,
                        os.Order_Details.FirstOrDefault().Quantity
                    };
            dataGridView1.DataSource = q1.ToList();




            var q2 = from a in q1
                     select new
                     {
                         a.FirstName,
                         TotalPrice = a.Quantity * a.UnitPrice
                     };
            dataGridView2.DataSource = q2.ToList();

            //var q3 = from b in q2
            //         group b by b.FirstName into g
            //         select new
            //         {
            //             g.Key,
            //             TotalSales = g.Sum(b => b.TotalPrice)
            //         };
            //var q4 = from c in q3
            //         orderby c.TotalSales descending
            //         select c;

            //dataGridView2.DataSource = q4.Take(5).ToList();

        }

        private void button9_Click(object sender, EventArgs e)
        {//產品最高單價前五 與類別名稱
            var q1 = (from p in dbContext.Products
                      orderby p.UnitPrice descending
                      select new
                      {
                          p.UnitPrice,
                          p.ProductName,
                          p.Categories.CategoryName
                      }).Take(5);

            dataGridView1.DataSource = q1.ToList();

        }

        private void button7_Click(object sender, EventArgs e)
        {//     NW 產品有任何一筆單價大於300 ?
            var q = (from p in dbContext.Products
                     where p.UnitPrice > 300
                     select p).Any();
            MessageBox.Show(q + "");
        }
    }
}
