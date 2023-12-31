using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Squirrel;
using System.Threading.Tasks;

namespace WinBGMuter
{
    internal class InstallManager
    {

        public static void Init()
        {
            SquirrelAwareApp.HandleEvents(
                onInitialInstall: OnAppInstall,
                onAppUninstall: OnAppUninstall,
                onEveryRun: OnAppRun);
        }

        private static void OnAppInstall(SemanticVersion version, IAppTools tools)
        {
            tools.CreateShortcutForThisExe(ShortcutLocation.StartMenu | ShortcutLocation.Desktop);
        }

        private static void OnAppUninstall(SemanticVersion version, IAppTools tools)
        {
            tools.RemoveShortcutForThisExe(ShortcutLocation.StartMenu | ShortcutLocation.Desktop);
        }

        private static void OnAppRun(SemanticVersion version, IAppTools tools, bool firstRun)
        {
            tools.SetProcessAppUserModelId();
            // show a welcome message when the app is first installed
            if (firstRun) MessageBox.Show("Thanks for installing my application!");
        }



        public static void UpdateApp()
        {
            using var mgr = new UpdateManager(
                "X:\\01_Projects\\02_Background Muter\\" +
                "WinBGMute\\Releases");

            if (!mgr.IsInstalledApp)
            {
                LoggingEngine.LogLine("[-] Application is not installed. Check if running in portable mode.",null,null,LoggingEngine.LOG_LEVEL_TYPE.LOG_ERROR);
                return;
            }

            string appstring = mgr.CurrentlyInstalledVersion() + " - " + mgr.AppId.ToString();
            LoggingEngine.LogLine(appstring);
            var newVersion = mgr.UpdateApp();
            MessageBox.Show(appstring);


            // optionally restart the app automatically, or ask the user if/when they want to restart
            if (newVersion != null)
            {
                UpdateManager.RestartApp();
            }

        }
    }
}
