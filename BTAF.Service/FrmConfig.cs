using System;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Threading;
using System.Windows.Forms;
using BTAF.Lib;

namespace BTAF.Service
{
    public partial class FrmConfig : Form
    {
        private readonly ConfigFile config;
        private readonly AudioMonitor monitor;

        public FrmConfig()
        {
            config = ConfigFile.Load();
            if (config.AudioDevices == null || config.AudioDevices.Length == 0)
            {
                var defaultDevice = AudioDeviceEnumerator.GetDefaultDevice();
                if (defaultDevice != null)
                {
                    config.AudioDevices = new AudioDevice[1] { defaultDevice };
                    MessageBox.Show("No audio device was configured. The default device was added to the list automatically.",
                        Text, MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            monitor = new AudioMonitor();
            monitor.AudioGatewayServiceChange += Monitor_AudioGatewayServiceChange;
            InitializeComponent();
            Scale();
            ShowAudioDevices();
            SetServiceButtons();
            SetManualButtons();
            AddManualEventMessage("Manual audio monitor ready");
            CbKeepAudioBusy.Checked = config.KeepDeviceBusy;

            var exe = Path.GetFullPath(Application.ExecutablePath);
            var profileDir = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile);
            if (exe.StartsWith(profileDir + Path.DirectorySeparatorChar))
            {
                LblPathWarning.Visible = true;
            }
        }

        private void Monitor_AudioGatewayServiceChange(object sender, bool isRunning)
        {
            Invoke((MethodInvoker)delegate
            {
                AddManualEventMessage(string.Format("Audio gateway service change: {0}", isRunning ? "Running" : "Stopped"));
            });
        }

        private void Scale()
        {
            Font = new Font(Font.FontFamily, Font.Size * 1.5f);
        }

        private void AddManualEventMessage(string message)
        {
            LbManualLog.SelectedIndex = LbManualLog.Items.Add(string.Format("[{0}] {1}", DateTime.Now, message));
            while (LbManualLog.Items.Count > 100)
            {
                LbManualLog.Items.RemoveAt(0);
            }
        }

        private void PerformServiceAction(Action act, Action continueWith = null)
        {
            TabControlMain.Enabled = false;
            var t = new Thread(() =>
            {
                act();
                Invoke((MethodInvoker)delegate ()
                {
                    TabControlMain.Enabled = true;
                    SetServiceButtons();
                    continueWith?.Invoke();
                });
            });
            t.IsBackground = true;
            t.Start();
        }

        private void SetServiceButtons()
        {
            if (ServiceInstallHelper.IsInstalled)
            {
                BtnServiceInstall.Enabled = false;
                BtnServiceUninstall.Enabled = true;
                if (ServiceInstallHelper.IsEnabled)
                {
                    BtnServiceEnable.Enabled = false;
                    BtnServiceDisable.Enabled = true;
                    if (ServiceInstallHelper.IsRunning)
                    {
                        BtnServiceStart.Enabled = false;
                        BtnServiceStop.Enabled = true;
                    }
                    else
                    {
                        BtnServiceStart.Enabled = true;
                        BtnServiceStop.Enabled = false;
                    }
                }
                else
                {
                    BtnServiceEnable.Enabled = true;
                    BtnServiceDisable.Enabled =
                        BtnServiceStart.Enabled =
                        BtnServiceStop.Enabled =
                        false;
                }
            }
            else
            {
                BtnServiceInstall.Enabled = true;
                BtnServiceUninstall.Enabled =
                    BtnServiceEnable.Enabled =
                    BtnServiceDisable.Enabled =
                    BtnServiceStart.Enabled =
                    BtnServiceStop.Enabled =
                    false;
            }
        }

        private void SetManualButtons()
        {
            BtnManualStart.Enabled = !monitor.IsRunning;
            BtnManualStop.Enabled = monitor.IsRunning;
        }

        private void ShowAudioDevices()
        {
            //LvMonitoredDevices.SuspendLayout();
            LvMonitoredDevices.Items.Clear();
            foreach (var dev in config.AudioDevices)
            {
                var item = LvMonitoredDevices.Items.Add(dev.Id);
                item.SubItems.Add(dev.Name);
                item.Tag = dev;
            }
            //LvMonitoredDevices.ResumeLayout();
            LvMonitoredDevices.AutoResizeColumns(ColumnHeaderAutoResizeStyle.ColumnContent);
        }

        private void BtnSave_Click(object sender, EventArgs e)
        {
            if (config.AudioDevices == null || config.AudioDevices.Length == 0)
            {
                MessageBox.Show("Please select an audio device", "No device", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            try
            {
                config.KeepDeviceBusy = CbKeepAudioBusy.Checked;
                config.Save();
                monitor.ReloadConfig();
                if (monitor.IsRunning)
                {
                    MessageBox.Show("Settings have been saved", "Settings saved", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    AddManualEventMessage("Applied new monitor configuration");
                }
                else if (ServiceInstallHelper.IsRunning)
                {
                    MessageBox.Show("The service is currently running. Restart it from the service control tab for the changes to take effect.",
                        "Service restart required", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else if (!ServiceInstallHelper.IsInstalled)
                {
                    MessageBox.Show("Settings saved. The settings currently have no effect because the service is not installed. Install it from the service control tab.",
                        "Service not installed", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show("Settings saved. The new settings will take effect when the service is started",
                        "Service not running", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error saving changes", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BtnClose_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void BtnServiceInstall_Click(object sender, EventArgs e)
        {
            if (!ServiceInstallHelper.IsInstalled)
            {
                PerformServiceAction(() =>
                {
                    ServiceInstallHelper.Install();
                });
            }
            else
            {
                SetServiceButtons();
            }

        }

        private void BtnServiceUninstall_Click(object sender, EventArgs e)
        {
            if (ServiceInstallHelper.IsInstalled)
            {
                PerformServiceAction(() =>
                {
                    ServiceInstallHelper.Uninstall();
                });
            }
            else
            {
                SetServiceButtons();
            }

        }

        private void BtnServiceEnable_Click(object sender, EventArgs e)
        {
            if (!ServiceInstallHelper.IsEnabled)
            {
                PerformServiceAction(() =>
                {
                    ServiceInstallHelper.Enable();
                });
            }
            else
            {
                SetServiceButtons();
            }

        }

        private void BtnServiceDisable_Click(object sender, EventArgs e)
        {
            if (ServiceInstallHelper.IsEnabled)
            {
                PerformServiceAction(() =>
                {
                    ServiceInstallHelper.Disable();
                });
            }
            else
            {
                SetServiceButtons();
            }

        }

        private void BtnServiceStart_Click(object sender, EventArgs e)
        {
            if (!ServiceInstallHelper.IsRunning)
            {
                PerformServiceAction(() =>
                {
                    ServiceInstallHelper.Start();
                });
            }
            else
            {
                SetServiceButtons();
            }
        }

        private void BtnServiceStop_Click(object sender, EventArgs e)
        {
            if (ServiceInstallHelper.IsRunning)
            {
                PerformServiceAction(() =>
                {
                    ServiceInstallHelper.Stop();
                });
            }
            else
            {
                SetServiceButtons();
            }
        }

        private void BtnServiceReset_Click(object sender, EventArgs e)
        {
            if (ServiceInstallHelper.IsRunning)
            {
                MessageBox.Show("Cannot reset the bluetooth service while the BTAF service is running. Stop it first", "Invalid service state", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            PerformServiceAction(() =>
            {
                ServiceControl.Enable();
                ServiceControl.Start();
            }, () =>
            {
                if (ServiceInstallHelper.IsEnabled)
                {
                    MessageBox.Show("The BTAF service is still enabled. The reset will at most only persist until the next boot", "BTAF service enabled", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                MessageBox.Show("The bluetooth audio gateway service was reset and started", "Service reset", MessageBoxButtons.OK, MessageBoxIcon.Information);
            });

        }

        private void BtnManualStart_Click(object sender, EventArgs e)
        {
            monitor.ReloadConfig();
            monitor.Start();
            SetManualButtons();
            AddManualEventMessage("Started audio monitor");
        }

        private void BtnManualStop_Click(object sender, EventArgs e)
        {
            monitor.Stop();
            SetManualButtons();
            AddManualEventMessage("Stopped audio monitor");
        }

        private void BtnAdd_Click(object sender, EventArgs e)
        {
            var exclusions = config.AudioDevices.Select(m => m.Id).ToArray();
            var devs = AudioDeviceEnumerator
                .EnumerateDevices(true)
                .Select(m => m.Id)
                .Except(exclusions, StringComparer.InvariantCultureIgnoreCase)
                .ToArray();
            if (devs.Length == 0)
            {
                if (exclusions.Length > 0)
                {
                    MessageBox.Show("All available audio devices are already monitored. If a bluetooth device is missing, try reconnecting it.", Text, MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show("There are no audio devices on this system. Try reconnecting your bluetooth device, and make sure it's not currently connected to a different computer or smartphone.", Text, MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            else
            {
                using (var dlg = new FrmAudioDeviceSelect(exclusions))
                {
                    if (dlg.ShowDialog() == DialogResult.OK && dlg.SelectedItem != null)
                    {
                        config.AudioDevices = config.AudioDevices.Concat(new AudioDevice[1] { dlg.SelectedItem }).ToArray();
                        ShowAudioDevices();
                    }
                }
            }
        }

        private void LvMonitoredDevices_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Delete)
            {
                e.SuppressKeyPress = e.Handled = true;
                var ids = LvMonitoredDevices.SelectedItems
                    .OfType<ListViewItem>()
                    .Select(m => m.Text)
                    .ToArray();
                config.AudioDevices = config.AudioDevices
                    .Where(m => !ids.Contains(m.Id, StringComparer.InvariantCultureIgnoreCase))
                    .ToArray();
                ShowAudioDevices();
            }
        }
    }
}
