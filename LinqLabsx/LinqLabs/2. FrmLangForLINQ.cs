using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Starter
{
    public partial class FrmLangForLINQ : Form
    {
        public FrmLangForLINQ()
        {
            InitializeComponent();

            this.productsTableAdapter1.Fill(this.northwindDataSet1.Products);
        }

        private void button7_Click(object sender, EventArgs e)
        {
            int n1 = 100;
            int n2 = 200;

            MessageBox.Show(n1 + "," + n2);
            SwapAnyType<int>(ref n1, ref n2);
            MessageBox.Show(n1 + "," + n2);

            //=======================
            string s1, s2;
            s1 = "aaa";
            s2 = "bbb";
            MessageBox.Show(s1 + "," + s2);
            //SwapAnyType<string>(ref s1, ref s2);
            SwapAnyType(ref s1, ref s2);     //推斷型別
            MessageBox.Show(s1 + "," + s2);
        }

        void SwapAnyType<T>(ref T n1, ref T n2)
        {
            T temp = n1;
            n1 = n2;
            n2 = temp;
        }

        void Swap(ref int n1, ref int n2)
        {
            int temp = n1;
            n1 = n2;
            n2 = temp;
        }

        void Swap(ref string n1, ref string n2)
        {

        }
        void Swap(ref Point n1, ref Point n2)
        {

        }

        private void button4_Click(object sender, EventArgs e)
        {
            int n1 = 100;
            int n2 = 200;
            MessageBox.Show(n1 + "," + n2);
           Swap( ref n1, ref n2);
            MessageBox.Show(n1 + "," + n2);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            //C# 1.0 具名方法
            this.buttonX.Click += ButtonX_Click;


            //C# 2.0 匿名方法
            this.buttonX.Click += delegate (object sender1, EventArgs e1)
                                           {
                                               MessageBox.Show("匿名方法");
                                           };

            //C# 3.0 匿名方法 簡潔版 =>
            this.buttonX.Click += (object sender1, EventArgs e1)=>
                                    {
                                        MessageBox.Show("匿名方法 =>....");
                                    };
        }

        private void ButtonX_Click(object sender, EventArgs e)
        {
            MessageBox.Show("ButtonX click");
        }

        bool Test(int n)
        {
            return n > 5;
        }

        private void button9_Click(object sender, EventArgs e)
        {
            bool result = Test(1);
            MessageBox.Show("result = " + result);
        }


        //step 1: create delegate Class
        //Step 2: create delegate Object
        //Step 3: Invoke (Call Method)

        public delegate bool MyDelegate(int n);

        private void button10_Click(object sender, EventArgs e)
        {

            //C# 1.0 具名方法
            MyDelegate delegateObj = Test;// new MyDelegate(Test);
            bool result = delegateObj.Invoke(1);
            MessageBox.Show("result = " + result);

            delegateObj = aaa;
            //result = delegateObj.Invoke(11);
            result = delegateObj(11); //省略 Invoke
            MessageBox.Show("result = " + result);

            //====================================
            //C# 2.0 匿名方法
            MyDelegate delegateObj_2 = delegate (int n)
                                                 {
                                                     return n > 5;
                                                 };
            result = delegateObj_2.Invoke(10);
            MessageBox.Show("result = " + result);


            //====================================
            //C# 3.0 匿名方法簡潔板 => labmda expression

            //MyDelegate delegateObj_3 =  (int n)=>
            //                            {
            //                                return n > 5;
            //                            };

            MyDelegate delegateObj_3 = n => n > 5;
            result = delegateObj_3.Invoke(10);
            MessageBox.Show("result = " + result);

        }

        private bool aaa(int n)
        {
            return n > 5;
        }


        List<int> MyWhere(int[] source, MyDelegate delegateObj)
        {
            List<int> resultList = new List<int>();

            foreach (int n in source)
            {
                if (delegateObj.Invoke(n))
                {
                    resultList.Add(n);
                }
            }
            return resultList;
        }

        private void button11_Click(object sender, EventArgs e)
        {
            int[] nums = { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };
            
            List<int> list1 =  MyWhere(nums, Test);

            foreach (int n in list1)
            {
                this.listBox1.Items.Add(n);
            }

            //==================================
            List<int> list2 = MyWhere(nums, n => n > 5);
            foreach (int n in list2)
            {
                this.listBox2.Items.Add(n);
            }

            List<int>  evenList = MyWhere(nums, n => n %2 ==0);
            List<int> oddList = MyWhere(nums, n => n % 2 == 1);

        }

        private void button3_Click(object sender, EventArgs e)
        {

            int[] nums = { 1, 2, 3, 4, 5, 6, 7, 8, 9, 111, 1111 };

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

        IEnumerable<int> MyIterator(int[] source, MyDelegate delegateObj)
        {
            foreach (int n in source)
            {
                if (delegateObj.Invoke(n))
                {
                    yield return n;
                }
            }
        }
        private void button13_Click(object sender, EventArgs e)
        {
            int[] nums = { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10,11,13 };

            IEnumerable<int> q = MyIterator(nums, n => n > 5);

            foreach (int n in q)
            {
                this.listBox1.Items.Add(n);
            }

            //=================================
            var q2 = MyIterator(nums, n => n % 2 == 1);

            foreach (int n in q2)
            {
                this.listBox2.Items.Add(n);
            }

        }


//        #region 組件 System.Core, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
//// C:\Program Files (x86)\Reference Assemblies\Microsoft\Framework\.NETFramework\v4.5.2\System.Core.dll
//#endregion

//using System.Collections;
//using System.Collections.Generic;

//namespace System.Linq
//    {
//        //
//        // 摘要:
//        //     提供一組 static (在 Visual Basic 中為 Shared) 方法，用於查詢實作 System.Collections.Generic.IEnumerable`1
//        //     的物件。
//        public static class Enumerable

        private void button44_Click(object sender, EventArgs e)
        {
            string s = "abc";
            int n =  s.WordCount();
            MessageBox.Show("word Count =" + n);

            MessageBox.Show("Char = " + s.Chars(2));

            string s1 = "ABCDEF";
            char ch = s1.Chars(3);
            MessageBox.Show("Char = " + ch);

            ch = MyStringExtend.Chars(s1, 3);
            MessageBox.Show("Char = " + ch);


        }

        //var abc = "dsfsd";

        private void button45_Click(object sender, EventArgs e)
        {
            //var
            //1. 懶得寫
            //2. 難寫
            //3. 匿名型別 (不得不寫)

            var s = "abcde";
            s.ToUpper();

            var n = 88;

            var p = new Point(100, 100);
            MessageBox.Show(p.X + ", " + p.Y);

          

        }

        private void button41_Click(object sender, EventArgs e)
        {
            List<Point> list = new List<Point>() {
                                                    new Point { X=100, Y =200 },
                                                    new Point { X=200, Y =200 },
                                                    new Point { X = 300, Y = 200 }
                                                 };

            list.Add(new Point { X = 99, Y = 99 });
            list.Add(new Point { X = 99, Y = 99 });

            this.dataGridView1.DataSource = list;
        }

        private void button43_Click(object sender, EventArgs e)
        {
            var  p1 =  new { X = 222, Y = 222, Z = 555 };
            var p2 = new { X = 222, Y = 222, Z = 555, P1=333,P2=44 };

            var p3 = new { X = 222, Y = 222, Z = 555 };

            this.listBox1.Items.Add(p1.GetType());
            this.listBox1.Items.Add(p2.GetType());
            this.listBox1.Items.Add(p3.GetType());


            int[] nums = { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };

            var q = from n in nums
                    where n > 5
                    select new { N = n, Square = n * n, Cube = n * n * n };

            this.dataGridView1.DataSource = q.ToList();

            //==================================
            var q2 = nums.Where(n => n > 5).Select(n => new { N = n, Square = n * n, Cube = n * n * n });

            this.dataGridView2.DataSource= q2.ToList();

            //====================================

            var q3 = from p in this.northwindDataSet1.Products
                     where p.UnitPrice > 30  && p.ProductName.ToUpper().StartsWith("C")
                     select new
                     {
                         ID = p.ProductID,
                         p.ProductName,        //推斷屬性
                         p.UnitPrice,
                         p.UnitsInStock,
                         TotalPrice =$"{p.UnitsInStock * p.UnitPrice:c2}" 
                     };

            this.dataGridView1.DataSource= q3.ToList();
        }

        private void button40_Click(object sender, EventArgs e)
        {
            //具名型別陣列
            Point[] pts = new Point[]{
                                 new Point(10,10),
                                 new Point(20, 20)
                                };

            //匿名型別陣列
            var arr = new[] {
                                new { x = 1, y = 1 },
                                new { x = 2, y = 2 }
                             };


            foreach (var item in arr)
            {
                listBox1.Items.Add(item.x + ", " + item.y);

            }
            this.dataGridView1.DataSource = arr;
        }
    }
}

//class MyString : String
//{

//}

public static class MyStringExtend
{
    public static int WordCount(this string s)
    {
        return s.Length;
    }

    public static char Chars(this string s, int index)
    {
        return s[index];
    }
}
