using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;

namespace Starter
{
    public partial class FrmLINQ_To_XXX : Form
    {
        public FrmLINQ_To_XXX()
        {
            InitializeComponent();
        }

        private void button31_Click(object sender, EventArgs e)
        {

        }

        private void button6_Click(object sender, EventArgs e)
        {
            int[] nums = { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10,11,111,121,101 };

            IEnumerable<IGrouping<int,int>> q = from n in nums
                                                 group n by (n % 2);

            this.dataGridView1.DataSource= q.ToList();


            foreach (var group in q)
            {
                TreeNode x = this.treeView1.Nodes.Add(group.Key.ToString());

                foreach (var item in group)
                {
                    x.Nodes.Add(item.ToString());
                }
            }

        }
    }
}
