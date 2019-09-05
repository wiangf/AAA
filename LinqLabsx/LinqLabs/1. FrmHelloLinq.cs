using LinqLabs;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace Starter
{
    public partial class FrmHelloLinq : Form
    {
        public FrmHelloLinq()
        {
            InitializeComponent();

            this.productsTableAdapter1.Fill(this.northwindDataSet1.Products);
            this.ordersTableAdapter1.Fill(this.northwindDataSet1.Orders);
        }

        private void button4_Click(object sender, EventArgs e)
        {
            int[] nums = { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };

            //語法糖
            foreach (int n in nums)
            {
                this.listBox1.Items.Add(n);
            }

            //===================================================
            this.listBox1.Items.Add("============================");

            //摘要:  IEnumerable<T> 
            //公開支援指定類型集合上簡單反覆運算的列舉值。  可列舉的
            //內部轉譯
            System.Collections.IEnumerator en = nums.GetEnumerator();
            while ( en.MoveNext())
            {
                this.listBox1.Items.Add(en.Current);
            }

                
        }

        private void button5_Click(object sender, EventArgs e)
        {
            List<int> list = new List<int> { 4, 5, 6, 7, 2, 33, 44, 66 };

           
            //語法糖
            foreach (int n in list)
            {
                this.listBox1.Items.Add(n);
            }

            //===================================================
            this.listBox1.Items.Add("============================");

            //摘要:  IEnumerable<T> 
            //公開支援指定類型集合上簡單反覆運算的列舉值。  可列舉的
            //內部轉譯
            List<int>.Enumerator en = list.GetEnumerator();
            while (en.MoveNext())
            {
                this.listBox1.Items.Add(en.Current);
            }


        }

        private void button2_Click(object sender, EventArgs e)
        {
            //int n = 100;
            //var n1 = 200;
            //var s = "abc";

            //Step 1: Source Data
            //Step 2: define query
            //Step 3: execute query

            //Step1: Define Source
            int[] nums = { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };



            //Setp2: Define Query
            //define query  (q 是一個  Iterator 物件)　, 如陣列集合一般 (陣列集合也是一個  Iterator 物件)
            //IEnumerable<int> q -  公開支援指定型別集合上簡單反覆運算的列舉值。
            IEnumerable<int> q = from n in nums
                                //where n % 2 == 0
                                //where (n>=5 && n<=8) && (n % 2 == 0)
                                where IsEven(n)
                                select n;


            //Step 3: Execute Query
            //execute query(執行 iterator - 逐一查看集合的item)
            foreach (int n in q)
            {
                this.listBox1.Items.Add(n);
            }
        }

        private bool IsEven(int n)
        {
            //if (n%2==0)
            //{
            //    return true;
            //}
            //else
            //{
            //    return false;
            //}

            return n % 2 == 0;
        }

        private void button6_Click(object sender, EventArgs e)
        {
            int[] nums = { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };



            IEnumerable<Point> q = from n in nums
                                   where IsEven(n)
                                   select new Point { X = n, Y = n * n };


            //execute Query - foreach
            foreach (Point p in q)
            {
                this.listBox1.Items.Add(p.X + ", " + p.Y);
            }

            //===============================================
            //execute Query - TOXXX()
            List<Point> list = q.ToList(); //內部執行 foreach ......
            this.dataGridView1.DataSource = list;

            //==============================
            this.chart1.DataSource = list;

            this.chart1.Series[0].XValueMember = "X";
            this.chart1.Series[0].YValueMembers = "Y";
            this.chart1.Series[0].ChartType = SeriesChartType.Line;

            this.chart1.Series[0].Color = Color.Green;
            this.chart1.Series[0].BorderWidth = 3;

        }

        private void button1_Click(object sender, EventArgs e)
        {
            string[] words = { "xxx", "Apple", "xxxapple", "PineApple", "yyyapple" , "ssssss"};
                  

            IEnumerable<string> q = from w in words
                                    where w.Length > 5 && w.ToLower().Contains("apple")
                                    orderby w
                                    select w;


            foreach (string w in q)
            {
                this.listBox1.Items.Add(w);
            }

            this.dataGridView1.DataSource = q.ToList();
           
        }

        private void button3_Click(object sender, EventArgs e)
        {
            int[] nums = { 1, 2, 3 ,4,5,6,7,8,9,111,1111};

            //var q = from n in nums
            //        where n > 5
            //        select n;

            //public static IEnumerable<TSource> Where<TSource>(this IEnumerable<TSource> source, Func<TSource, bool> predicate);

            //1. 泛型 (泛用方法)                                          (ex.  void Swap<T>(ref T a, ref T b)
            //2. 委派參數 Lambda Expression (匿名方法簡潔版)               (ex.  MyWhere(nums, n => n %2==0);
            //3. Iterator 回傳                                           (ex.  MyIterator)
            //4. 擴充方法 (this...                                        (ex.  MyStringExtend.WordCount(s);


            IEnumerable<int> q = nums.Where<int>(n => n > 5);
          
            foreach (int n in q)
            {
                this.listBox1.Items.Add(n);
            }

            //=========================================
            string[] words = { "aaa", "bbbbbbb", "cccc" };

            //var q1 = from w in words
            //         where w.Length > 3
            //         select w;

            var q1 = words.Where<string>(w => w.Length > 5);
            
            foreach (string w in q1)
            {
                this.listBox1.Items.Add(w);
            }


        }

        private void button7_Click(object sender, EventArgs e)
        {

            
            IEnumerable<NorthwindDataSet.ProductsRow> q = from p in this.northwindDataSet1.Products
                                                            where p.UnitPrice >30
                                                            select p;

           this.dataGridView1.DataSource= q.ToList();
        }

        private void button8_Click(object sender, EventArgs e)
        {
            IEnumerable<NorthwindDataSet.OrdersRow> q = from o in this.northwindDataSet1.Orders
                                                        where (! o.IsOrderDateNull()) &&  o.OrderDate.Year == 1997
                                                        select o;

            this.dataGridView1.DataSource= q.ToList();
        }

        private void button9_Click(object sender, EventArgs e)
        {
           
            DirectoryInfo dir = new DirectoryInfo(@"c:\windows");
            FileInfo[] files = dir.GetFiles();

            var q = from f in files
                    where f.Extension == ".exe" || f.Extension == ".log"
                    orderby f.Length descending
                    select f;

            this.dataGridView1.DataSource = q.ToList();//files;

           // files[0].CreationTime
        }
    }
}
