using System;
using System.Linq;
using System.Windows.Forms;
using BTAF.Lib;

namespace BTAF.Service
{
    public partial class FrmAudioDeviceSelect : Form
    {
        private readonly string[] idExclusionList;

        public AudioDevice SelectedItem { get; private set; } = null;

        public FrmAudioDeviceSelect(string[] idExclusionList)
        {
            this.idExclusionList = idExclusionList;
            InitializeComponent();
            RefreshDevices();
        }

        private void RefreshDevices()
        {
            CBAudioDeviceList.Items.Clear();
            var devices = AudioDeviceEnumerator
                .EnumerateDevices(true)
                .Where(m => !idExclusionList.Contains(m.Id, StringComparer.InvariantCultureIgnoreCase))
                .ToArray();
            if (devices.Length == 0)
            {
                MessageBox.Show(
                    "There are currently no audio devices connected which are not already monitored\r\n" +
                    "Connect your bluetooth audio device, and after a few seconds, click \"Refresh\"",
                    Text,
                    MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            else
            {
                foreach (var dev in devices)
                {
                    CBAudioDeviceList.Items.Add(new AudioComboBoxItem(dev));
                }
            }
        }

        private void BtnOk_Click(object sender, EventArgs e)
        {
            if (CBAudioDeviceList.SelectedItem is AudioComboBoxItem item)
            {
                SelectedItem = item.Device;
                DialogResult = DialogResult.OK;
            }
            else
            {
                MessageBox.Show("Please select an audio device", Text,
                    MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        private void BtnCancel_Click(object sender, EventArgs e)
        {
            SelectedItem = null;
        }

        private void BtnRefresh_Click(object sender, EventArgs e)
        {
            RefreshDevices();
        }
    }
}
