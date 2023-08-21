using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace _2023._08_Uni_Scheduler.App.Generic_Screen
{
    public partial class ProgressForm : Form
    {
        public ProgressForm()
        {
            InitializeComponent();
            btnCancelar.Click += btnCancelar_Click;
        }
        private Action onCancel;
        /** Instance **/
        public CancellationTokenSource cancellationTokenSource { get; private set; } = new CancellationTokenSource();
        public bool IsCancelled { get; private set; } = false;

        public void UpdateProgress(int progress)
        {
            if (progressBar.InvokeRequired)
            {
                progressBar.Invoke(new Action<int>(UpdateProgress), progress);
            }
            else
            {
                progressBar.Value = progress;
            }
        }



        private void btnCancelar_Click(object sender, EventArgs e)
        {
            cancellationTokenSource.Cancel();
            onCancel();
            MessageBox.Show("Operação cancelada pelo usuário.");
            this.Close();
        }

        public void SetCancellationTokenSource(CancellationTokenSource cancellationTokenSource, Action onCancel)
        {
            this.cancellationTokenSource = cancellationTokenSource;
            this.onCancel = onCancel;
        }
    }
}
