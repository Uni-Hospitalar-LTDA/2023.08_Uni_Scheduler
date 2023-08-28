using _2023._08_Uni_Scheduler.Configuration;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace _2023._08_Uni_Scheduler.App
{
    public partial class frmTeste : Form
    {
        public frmTeste()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Console.WriteLine(DateExtractor.GetSqlCondition(textBox1.Text));
            //MessageBox.Show(DateExtractor.getDateConditions(textBox1.Text));
            //getDateConditions
        }
    }
}
