using _2023._08_Uni_Scheduler.App;
using DocumentFormat.OpenXml.Bibliography;
using Squirrel;
using System;
using System.Windows.Forms;

namespace _2023._08_Uni_Scheduler
{
    internal static class Program
    {
        //Caminho da stático da pasta AtenasDataBot
        public static readonly string _updateUrl = @"\\srvsec04\Softwares\AtenasDataBot\Releases";
        [STAThread]
        static void Main()
        {
            /**Manual squirrel https://github.com/Squirrel/Squirrel.Windows/blob/develop/docs/getting-started/2-packaging.md **/
            Task.WaitAny(CheckForUpdatesAsync());
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new frmMain());
            
        }

        private static async Task CheckForUpdatesAsync()
        {
            try
            {
                using (var updateManager = new UpdateManager(_updateUrl))
                {
                    var updateInfo = await updateManager.CheckForUpdate();

                    if (updateInfo.ReleasesToApply.Count > 0)
                    {
                        await Task.Run(async () =>
                        {
                            await updateManager.DownloadReleases(updateInfo.ReleasesToApply);
                            await updateManager.ApplyReleases(updateInfo);
                            await updateManager.UpdateApp();
                        });
                        await Task.Delay(5000);
                        UpdateManager.RestartApp();
                    }
                }
            }
            catch (Exception)
            {
                
            }
        }
    }
}
