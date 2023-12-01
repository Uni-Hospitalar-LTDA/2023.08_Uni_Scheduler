using Squirrel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace _2023._08_Uni_Scheduler.Configuration
{
    public class Updates
    {
        public static string _updateUrlWeb = @"https://github.com/Uni-Hospitalar-LTDA/2023.08_Uni_Scheduler-Publisher";

        public static async Task<string> CheckForUpdates()
        {
            bool update = false;
            try
            {
                using (var gitHubManager = await UpdateManager.GitHubUpdateManager(_updateUrlWeb))
                {
                    var releaseEntry = await gitHubManager.UpdateApp();
                    if (releaseEntry != null)
                    {
                        MessageBox.Show("Atualizando para a versão mais recente!");
                        update = true;
                        return gitHubManager.CurrentlyInstalledVersion() + " => " + releaseEntry.Version;
                    }
                    return gitHubManager.CurrentlyInstalledVersion().ToString();
                }
            }
            catch (Exception e)
            {
                if (e.Message != "Update.exe not found, not a Squirrel-installed app?")
                {
                    MessageBox.Show("Failed to update: " + e.Message);
                    return "1.0.0";
                }
            }
            finally
            {
                if (update)
                    UpdateManager.RestartApp();
            }

            return "1.0.0";
        }
    }
}
