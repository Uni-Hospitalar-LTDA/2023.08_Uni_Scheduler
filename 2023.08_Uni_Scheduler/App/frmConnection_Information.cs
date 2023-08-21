using _2023._08_Uni_Scheduler.Configuration;
using _2023._08_Uni_Scheduler.Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace _2023._08_Uni_Scheduler.App
{
    public partial class frmConnection_Information : CustomDefaultForm
    {
        public frmConnection_Information()
        {
            InitializeComponent();

            ConfigureFormProperties();
            ConfigureFormAttributes();


            ConfigureTextBoxProperties();
            ConfigureButtonProperties();


            ConfigureFormEvents();
        }

        internal bool insert { get; set; } = true;
        internal SchedulerApp_Connection connection { get; set; }

        /** Async Tasks **/
        private async Task saveAsync()
        {
            var savingList = new List<SchedulerApp_Connection>();
            if (string.IsNullOrEmpty(txtConnectionDescription.Text)
            ||string.IsNullOrEmpty(txtServer.               Text)
            ||string.IsNullOrEmpty(txtLogin.                Text)
            ||string.IsNullOrEmpty(txtPassword.             Text))
            {
                MessageBox.Show("Existem informações com o preenchimento pendente");
                return;
            }            

            savingList.Add(new SchedulerApp_Connection
            {
                 id = txtConnectionId.Text
                ,description = txtConnectionDescription.Text
                ,observation = txtObservation.Text
                ,server = txtServer.Text
                ,login = txtLogin.Text
                ,password = Cryptography.crypt(txtPassword.Text)
                ,logo = txtLogo.Text                
            });

            try
            {
                await SchedulerApp_Connection.insertAsync(savingList);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao inserir: "+ex.Message);
            }

        }

        private async Task updateAsync()
        {              
            if (string.IsNullOrEmpty(txtConnectionDescription.Text)
            ||string.IsNullOrEmpty(txtServer.               Text)
            ||string.IsNullOrEmpty(txtLogin.                Text)
            ||string.IsNullOrEmpty(txtPassword.             Text))
            {
                MessageBox.Show("Existem informações com o preenchimento pendente");
                return;
            }


            connection.id = txtConnectionId.Text;
            connection.description = txtConnectionDescription.Text;
            connection.observation = txtObservation.Text;
            connection.server = txtServer.Text;
            connection.login = txtLogin.Text;
            connection.password = Cryptography.crypt(txtPassword.Text);
            connection.logo = txtLogo.Text;

            try
            {
                await SchedulerApp_Connection.updateAsync(connection);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao alterar: "+ex.Message);
            }
        }
        /** Sync Methods **/
        private void getHtmlLogo(string imageUrl)
        {
            try
            {
                // Obtendo dimensões da imagem
                int width = 0;
                int height = 0;
                using (System.Net.WebClient wc = new System.Net.WebClient())
                {
                    using (System.IO.Stream s = wc.OpenRead(imageUrl))
                    {
                        using (System.Drawing.Image img = System.Drawing.Image.FromStream(s))
                        {
                            width = img.Width;
                            height = img.Height;
                        }
                    }
                }

                // Calculando a altura proporcional com base na largura do controle wbLogo
                int displayWidth = wbLogo.Width;
                int displayHeight = displayWidth * height / width;

                string html = $@"
        <html>
        <body>
            <div>Original Dimensions: {width}x{height}</div>
            <img src='{imageUrl}' alt='Image not found' style='display:block; margin:auto; width:{displayWidth}px; height:{displayHeight}px;' />
            
        </body>
        </html>";

                wbLogo.DocumentText = html;
            }
            catch (WebException ex)
            {
                MessageBox.Show(ex.Message);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        /** Form Configuration **/
        private void ConfigureFormProperties()
        {
            this.ConfigureDefault();
        }
        private void ConfigureFormAttributes()
        {
            this.Text = "Informação da Conexão";
        }
        private void ConfigureFormEvents()
        {
            this.Load += frmConnection_Information_Load;
        }
        private async void frmConnection_Information_Load(object sender, EventArgs e)
        {
            await ConfigureTextBoxAttributes();

            ConfigureTextBoxEvents();
            ConfigureButtonEvents();
        }



        /** TextBox Properties **/
        private async Task ConfigureTextBoxAttributes()
        {
            if (insert) 
            {
                txtConnectionId.Text = (await SchedulerApp_Connection.getNextCodeAsync()).ToString();
            }
            else if (!insert)
            {
                txtConnectionId.Text = connection.id;
                txtConnectionDescription.Text = connection.description;
                txtServer.Text = connection.server; 
                txtLogin.Text = connection.login;
                txtPassword.Text = Cryptography.decrypt(connection.password);
                txtLogo.Text = connection.logo;

                if (!string.IsNullOrEmpty(connection.logo))
                {
                    btnChargeLogo_Click(this,new EventArgs());
                }
            }

        }
        private void ConfigureTextBoxProperties()
        {
            txtConnectionId.ReadOnly = true;    
            txtConnectionId.TabStop = false;

            txtConnectionDescription.TabIndex = 0;
            txtObservation.TabIndex = 1;
            txtServer.TabIndex = 2;
            txtLogin.TabIndex = 3;
            txtPassword.TabIndex = 4;            
            txtLogo .TabIndex = 6;
            txtPassword.PasswordChar = '*';
        }
        private void ConfigureTextBoxEvents()
        {
            //txtConnectionDescription.Validated += txt_EmptyValidated;
            //txtServer.Validated += txt_EmptyValidated;
            //txtLogin.Validated += txt_EmptyValidated;
            //txtPassword.Validated += txt_EmptyValidated;            
        }
        private void txt_EmptyValidated(object sender, EventArgs e)
        {
            TextBox txt = (TextBox)sender;
            if (string.IsNullOrEmpty(txt.Text))
            {
                txt.Focus();
                MessageBox.Show($"A informação não pode ser nula ou vazia, por favor preencha!");

            }
        }
        /** Button Properties **/
        private void ConfigureButtonProperties()
        {
            btnTry.TabIndex = 5;
            btnChargeLogo.TabIndex = 7;
            btnSave.TabIndex = 8;
            btnClose.TabIndex = 9;
            btnClose.toDefaultCloseButton();
            
        }
        private void ConfigureButtonEvents()
        {
            btnTry.Click += btnTry_Click;
            btnSave.Click += btnSave_Click;
            btnChargeLogo.Click += btnChargeLogo_Click;
        }
        private async void btnTry_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtServer.Text)
            || string.IsNullOrEmpty(txtLogin.Text)
            || string.IsNullOrEmpty(txtPassword.Text))
            {
                MessageBox.Show("Existem informações com o preenchimento pendente");
                return;
            }
            await SchedulerApp_Connection.TestSqlConnection(txtServer.Text,txtLogin.Text,txtPassword.Text);
        }
        private async void btnSave_Click(object sender, EventArgs e)
        {

            if (insert)
            {
                try
                {
                    await saveAsync();
                    MessageBox.Show("Conexão inserida com sucesso!");
                    this.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
            else
            {
                try
                {
                    await updateAsync();
                    MessageBox.Show("Conexão inserida com sucesso!");
                    this.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

     

        private void btnChargeLogo_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtLogo.Text))
                getHtmlLogo(txtLogo.Text);
        }

    }
}
