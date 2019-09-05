
using LinqLabs.HW;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Starter
{
    static class Program
    {
        /// <summary>
        /// 應用程式的主要進入點。
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            //Application.Run(new FrmHelloLinq());
            //Application.Run(new FrmLangForLINQ());
            //Application.Run(new FrmLINQ架構介紹_InsideLINQ());
            Application.Run(new FrmTest());
            //Application.Run(new FrmLinq_To_Entity());
        }
    }
}
