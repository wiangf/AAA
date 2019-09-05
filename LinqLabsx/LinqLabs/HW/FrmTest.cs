using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LinqLabs.HW
{
    public partial class FrmTest : Form
    {
        public FrmTest()
        {
            InitializeComponent();
        }

        NorthwindEntities dbContext = new NorthwindEntities();
        private void button1_Click(object sender, EventArgs e)
        {
            //每月匯總銷售額
            var q = from od in dbContext.Order_Details
                    group od by od.Orders.OrderDate.Value.Month into g
                    select new
                    {
                        Key=g.Key,
                        totalSales = g.Sum(od => od.UnitPrice * od.ProductID)
                    };
            //那一個月銷售最不好,銷售最好
            var q2=q.OrderBy(c => c.totalSales).ToList();
            string a =( q2.Take(1).Select(c => c.Key).First().ToString());
            string b=(q2.Take(1).Select(c => c.totalSales).First().ToString());
            MessageBox.Show($"{a}月銷售最不好,該月總銷售{b}元");
            string d = (q2.OrderByDescending(p=>p.totalSales).Take(1).Select(c => c.Key).First().ToString());
            string y = (q2.OrderByDescending(p => p.totalSales).Take(1).Select(c => c.totalSales).First().ToString());

            MessageBox.Show($"{d}月銷售最好,該月總銷售{y}元");

            dataGridView1.DataSource = q2.ToList();
                  
        }
        
        
        private void button2_Click(object sender, EventArgs e)
        {   int i = 0;
            Random r = new Random();
            int[] scores = new int[100];
            foreach (var item in scores)
            {
                scores[i] = r.Next(1, 100);
                i++;
            }

            //優良 80 良>60 不良<60

            var q=scores.GroupBy(s=>MyJudge(s)).Select(k=>new { k.Key,Count=k.Count()});
            dataGridView1.DataSource= q.OrderBy(a=>a.Key).ToList();
        }

        private string MyJudge(int s)
        {
            if (s<60)
            {
                return "bad";
            }else if (s < 80)
            {
                return "medium";
            }
            else
            {
                return "good";
            }
        }
    }
}
