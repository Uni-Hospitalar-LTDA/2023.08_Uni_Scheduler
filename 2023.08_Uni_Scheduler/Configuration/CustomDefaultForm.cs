using _2023._08_Uni_Scheduler.App.Generic_Screen;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace _2023._08_Uni_Scheduler.Configuration
{
    public class CustomDefaultForm : Form
    {
        public CustomDefaultForm()
        {
            StartPosition = FormStartPosition.CenterScreen;
            KeyPreview = true;
            MaximizeBox = false;            
        }


        public async void RunMethodWithProgressBar(Action<Action<int>, CancellationToken> method)
        {
            // Cria e exibe o formulário da barra de progresso
            ProgressForm progressForm = new ProgressForm();
            progressForm.Show();

            // Cria um CancellationToken para controle de cancelamento
            CancellationTokenSource cancellationTokenSource = new CancellationTokenSource();
            // Cria um CancellationTokenSource para controle de cancelamento
            CancellationToken cancellationToken = cancellationTokenSource.Token;

            // Cria um TaskCompletionSource
            TaskCompletionSource<object> tcs = new TaskCompletionSource<object>();

            // Executa o método em uma nova thread STA
            Thread staThread = new Thread(() =>
            {
                try
                {
                    method(progressForm.UpdateProgress, cancellationToken);
                    tcs.SetResult(null);
                }
                catch (OperationCanceledException)
                {
                    // A operação foi cancelada pelo usuário
                    tcs.SetCanceled();
                }
                catch (Exception ex)
                {
                    // Um erro ocorreu durante a execução do método
                    tcs.SetException(ex);
                }
                finally
                {
                    // Fecha o formulário da barra de progresso                    
                    if (progressForm.IsHandleCreated)
                    {
                        progressForm.Invoke(new Action(() => progressForm.Close()));
                    }
                    else
                    {
                        EventHandler handleCreatedHandler = null;
                        handleCreatedHandler = new EventHandler((sender, e) =>
                        {
                            progressForm.Invoke(new Action(() => progressForm.Close()));
                            progressForm.HandleCreated -= handleCreatedHandler;
                        });

                        progressForm.HandleCreated += handleCreatedHandler;
                    }
                    //Form.ActiveForm.Invoke(new Action(() => ActiveForm.Focus()));
                }
            });

            staThread.SetApartmentState(ApartmentState.STA);
            staThread.Start();

            if (progressForm != null)
            {
                bool isCanceled = false;
                bool wasSuccessful = false;
                progressForm.SetCancellationTokenSource(cancellationTokenSource, () => isCanceled = true);

                try
                {
                    await tcs.Task;
                    wasSuccessful = true;

                }
                catch (OperationCanceledException)
                {
                    MessageBox.Show("A exportação foi cancelada.");

                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Ocorreu um erro durante a exportação - {ex.Message}");
                }
                finally
                {
                    if (wasSuccessful && !isCanceled)
                    {
                        MessageBox.Show("A operação foi concluída com sucesso!");
                    }
                }

            }
        }

    }
    public static class FormConfiguration
    {
        public static void ConfigureDefault(this Form form)
        {
            form.MinimumSize = new System.Drawing.Size(form.Width, form.Height);
            form.MaximumSize = new System.Drawing.Size(form.Width, form.Height);
        }
    }
}
