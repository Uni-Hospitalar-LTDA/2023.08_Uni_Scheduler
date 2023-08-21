using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace _2023._08_Uni_Scheduler.Configuration
{
    public class CustomApplication
    {
        public CustomApplication() 
        {
            
        }

        public const string name = "Atenas Data Bot v2.0";

        public static void ShowOrActivateForm<T>() where T : Form, new()
        {
            if (Application.OpenForms.OfType<T>().Count() > 0)
            {
                if (Application.OpenForms.OfType<T>().First().WindowState == FormWindowState.Minimized)
                {
                    Application.OpenForms.OfType<T>().First().WindowState = FormWindowState.Normal;
                    Application.OpenForms.OfType<T>().First().Focus();
                }
                else
                {
                    Application.OpenForms.OfType<T>().First().Focus();
                }
            }
            else
            {
                T frm = new T();
                frm.Show();
            }
        }
    }
}
