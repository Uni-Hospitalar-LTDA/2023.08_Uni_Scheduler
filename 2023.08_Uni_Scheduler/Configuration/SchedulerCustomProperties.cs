using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace _2023._08_Uni_Scheduler.Configuration
{
    public static class SchedulerCustomProperties
    {
        /** DataGridView configuration **/
        public static void toDefault(this DataGridView dataGridView)
        {
            //Grid            
            dataGridView.RowHeadersVisible = false;
            dataGridView.AllowUserToAddRows = false;
            dataGridView.AllowUserToDeleteRows = false;
            dataGridView.AllowUserToOrderColumns = true;
            dataGridView.AllowUserToResizeRows = false;
            dataGridView.MultiSelect = true;
            dataGridView.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dataGridView.ReadOnly = true;

            //Layout
            // Estilização dos Cabeçalhos
            dataGridView.ColumnHeadersDefaultCellStyle.Font = new System.Drawing.Font(dataGridView.Font, FontStyle.Bold);
            dataGridView.ColumnHeadersDefaultCellStyle.BackColor = Color.Gray;
            dataGridView.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            dataGridView.EnableHeadersVisualStyles = false;

            dataGridView.RowsDefaultCellStyle.BackColor = Color.White;
            dataGridView.AlternatingRowsDefaultCellStyle.BackColor = Color.LightGray;
            dataGridView.AutoResizeColumns();

        }

        public static void toDefaultCloseButton(this Button button)
        {
            button.Click += closeScreenEvent_Click;
        }

        public static void toDefaultCloseMenuButton(this Button button)
        {
            button.Click += closeMenuScreenEvent_Click;
        }

        private static void closeScreenEvent_Click(object sender, EventArgs e)
        {
            if (DialogResult.Yes == MessageBox.Show("Tem certeza que deseja fechar?", "Fechar tela", MessageBoxButtons.YesNo))
                Form.ActiveForm.Close();
        }

        private static void closeMenuScreenEvent_Click(object sender, EventArgs e)
        {            
                Form.ActiveForm.Close();
        }

        public static string replaceSpecialCharacters(this string palavra)
        {
            string novaPalavra = palavra;

            // Substituir letras com diacríticos
            novaPalavra = novaPalavra.Replace("á", "a");
            novaPalavra = novaPalavra.Replace("à", "a");
            novaPalavra = novaPalavra.Replace("â", "a");
            novaPalavra = novaPalavra.Replace("ã", "a");
            novaPalavra = novaPalavra.Replace("ç", "c");
            novaPalavra = novaPalavra.Replace("é", "e");
            novaPalavra = novaPalavra.Replace("ê", "e");
            novaPalavra = novaPalavra.Replace("í", "i");
            novaPalavra = novaPalavra.Replace("ó", "o");
            novaPalavra = novaPalavra.Replace("ô", "o");
            novaPalavra = novaPalavra.Replace("õ", "o");
            novaPalavra = novaPalavra.Replace("ú", "u");
            novaPalavra = novaPalavra.Replace("ü", "u");

            // Substituir pontuação
            novaPalavra = novaPalavra.Replace(".", "");
            novaPalavra = novaPalavra.Replace(",", "");
            novaPalavra = novaPalavra.Replace(";", "");
            novaPalavra = novaPalavra.Replace(":", "");
            novaPalavra = novaPalavra.Replace("-", "");
            novaPalavra = novaPalavra.Replace("_", "");
            novaPalavra = novaPalavra.Replace("(", "");
            novaPalavra = novaPalavra.Replace(")", "");
            novaPalavra = novaPalavra.Replace("[", "");
            novaPalavra = novaPalavra.Replace("]", "");
            novaPalavra = novaPalavra.Replace("{", "");
            novaPalavra = novaPalavra.Replace("}", "");
            novaPalavra = novaPalavra.Replace("?", "");
            novaPalavra = novaPalavra.Replace("!", "");
            novaPalavra = novaPalavra.Replace("\"", "");
            novaPalavra = novaPalavra.Replace("/", "");
            novaPalavra = novaPalavra.Replace("'", "");
            novaPalavra = novaPalavra.Replace("`", "");
            novaPalavra = novaPalavra.Replace("´", "");

            return novaPalavra;
        }



    }
}
