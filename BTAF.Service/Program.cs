using System;
using System.ServiceProcess;
using System.Windows.Forms;
using BTAF.Lib;

namespace BTAF.Service
{
    internal static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main(string[] args)
        {
            if (args.Length == 0)
            {
                RunConfig();
            }
            else
            {
                try
                {
                    switch (args[0].ToUpperInvariant())
                    {
                        case "/SERVICE":
                            RunService();
                            break;
                        case "/INSTALL":
                            ServiceInstallHelper.Install();
                            break;
                        case "/UNINSTALL":
                            ServiceInstallHelper.Uninstall();
                            break;
                        default:
                            ShowHelp();
                            MessageBox.Show(@"BTAF.Service [/INSTALL|/UNINSTALL]

This is not a command line utility. Simply double click the executable to open the configuration window",
                                "BTAF Help", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            break;
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Failed to perform the requested action.\r\n{ex.Message}", ex.GetType().Name,
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                return;
            }
        }

        private static void RunService()
        {
            ServiceBase[] ServicesToRun;
            ServicesToRun = new ServiceBase[]
            {
                new BTAFService()
            };
            ServiceBase.Run(ServicesToRun);
        }

        private static void RunConfig()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new FrmConfig());
        }

        private static void ShowHelp()
        {
            MessageBox.Show("BTAF.Service [/INSTALL|/UNINSTALL|/SERVICE]", "BTAF Help", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
    }
}
