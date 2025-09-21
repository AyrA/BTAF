using System;
using System.Collections.Generic;
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

        public FrmConfig()
        {
            config = ConfigFile.Load();
            InitializeComponent();
            RefreshAudioDevices();
            SetServiceButtons();
            var exe = Path.GetFullPath(Application.ExecutablePath);
            var profileDir = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile);
            if (exe.StartsWith(profileDir + Path.DirectorySeparatorChar))
            {
                LblPathWarning.Visible = true;
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

        private void RefreshAudioDevices()
        {
            var devices = AudioDeviceEnumerator.EnumerateDevices(true).ToArray();
            var comboItems = DeviceToComboBox(devices).ToArray();
            CBAudioDeviceList.Items.Clear();
            CBAudioDeviceList.Items.AddRange(comboItems.Cast<object>().ToArray());
            if (!string.IsNullOrEmpty(config.AudioDeviceId))
            {
                var dev = comboItems.FirstOrDefault(m => m.Device.Id == config.AudioDeviceId);
                if (dev != null)
                {
                    CBAudioDeviceList.SelectedItem = dev;
                }
            }
        }

        private IEnumerable<AudioComboBoxItem> DeviceToComboBox(IEnumerable<AudioDevice> devices)
        {
            return devices.Select(m => new AudioComboBoxItem(m));
        }

        private void BtnRefresh_Click(object sender, EventArgs e)
        {
            RefreshAudioDevices();
        }

        private void BtnSave_Click(object sender, EventArgs e)
        {
            try
            {
                config.Save();
                if (ServiceInstallHelper.IsRunning)
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
    }
}
