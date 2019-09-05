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
    public partial class Frm作業_2_AdventureWorksDataSet : Form
    {
        public Frm作業_2_AdventureWorksDataSet()
        {
            InitializeComponent();
            productPhotoTableAdapter1.Fill(adventureWorksDataSet1.ProductPhoto);
            comboBox3.DataSource = adventureWorksDataSet1.ProductPhoto.Select(p => p.ModifiedDate.Year).Distinct().ToList();

        }

        private void bindingSource1_CurrentChanged(object sender, EventArgs e)
        {

        }

        private void button11_Click(object sender, EventArgs e)
        {
            dataGridView1.DataSource = adventureWorksDataSet1.ProductPhoto;
        }

        private void button3_Click(object sender, EventArgs e)
        {

            var q = from p in adventureWorksDataSet1.ProductPhoto
                    where p.ModifiedDate > DateTime.Parse(dateTimePicker1.Text)
                    select p;
            dataGridView1.DataSource = q.ToList();
        }

        private void button5_Click(object sender, EventArgs e)
        {
          
        }

        private void button10_Click(object sender, EventArgs e)
        {
            //某季腳踏車? 有幾筆 ?
            var q=from p in adventureWorksDataSet1.ProductPhoto
                    group p by p.ModifiedDate.Month % 4==1 into g
                    select new
                    {
                        MyGroup = g
                    };

            dataGridView1.DataSource= q.ToList();

        }
    }
}
