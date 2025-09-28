namespace BTAF.Service
{
    partial class FrmConfig
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmConfig));
            this.TabControlMain = new System.Windows.Forms.TabControl();
            this.TabPageConfig = new System.Windows.Forms.TabPage();
            this.label2 = new System.Windows.Forms.Label();
            this.BtnAdd = new System.Windows.Forms.Button();
            this.LvMonitoredDevices = new System.Windows.Forms.ListView();
            this.CHDeviceId = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.CHDeviceName = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.CbKeepAudioBusy = new System.Windows.Forms.CheckBox();
            this.LblPathWarning = new System.Windows.Forms.Label();
            this.BtnSave = new System.Windows.Forms.Button();
            this.label8 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.TabPageService = new System.Windows.Forms.TabPage();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.label6 = new System.Windows.Forms.Label();
            this.BtnServiceReset = new System.Windows.Forms.Button();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.label5 = new System.Windows.Forms.Label();
            this.BtnServiceStop = new System.Windows.Forms.Button();
            this.BtnServiceStart = new System.Windows.Forms.Button();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.label4 = new System.Windows.Forms.Label();
            this.BtnServiceDisable = new System.Windows.Forms.Button();
            this.BtnServiceEnable = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label3 = new System.Windows.Forms.Label();
            this.BtnServiceUninstall = new System.Windows.Forms.Button();
            this.BtnServiceInstall = new System.Windows.Forms.Button();
            this.TabPageManual = new System.Windows.Forms.TabPage();
            this.LbManualLog = new System.Windows.Forms.ListBox();
            this.BtnManualStop = new System.Windows.Forms.Button();
            this.BtnManualStart = new System.Windows.Forms.Button();
            this.label7 = new System.Windows.Forms.Label();
            this.BtnClose = new System.Windows.Forms.Button();
            this.TabControlMain.SuspendLayout();
            this.TabPageConfig.SuspendLayout();
            this.TabPageService.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.TabPageManual.SuspendLayout();
            this.SuspendLayout();
            // 
            // TabControlMain
            // 
            this.TabControlMain.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.TabControlMain.Controls.Add(this.TabPageConfig);
            this.TabControlMain.Controls.Add(this.TabPageService);
            this.TabControlMain.Controls.Add(this.TabPageManual);
            this.TabControlMain.Location = new System.Drawing.Point(12, 12);
            this.TabControlMain.Name = "TabControlMain";
            this.TabControlMain.SelectedIndex = 0;
            this.TabControlMain.Size = new System.Drawing.Size(560, 338);
            this.TabControlMain.TabIndex = 0;
            // 
            // TabPageConfig
            // 
            this.TabPageConfig.Controls.Add(this.label2);
            this.TabPageConfig.Controls.Add(this.BtnAdd);
            this.TabPageConfig.Controls.Add(this.LvMonitoredDevices);
            this.TabPageConfig.Controls.Add(this.CbKeepAudioBusy);
            this.TabPageConfig.Controls.Add(this.LblPathWarning);
            this.TabPageConfig.Controls.Add(this.BtnSave);
            this.TabPageConfig.Controls.Add(this.label8);
            this.TabPageConfig.Controls.Add(this.label1);
            this.TabPageConfig.Location = new System.Drawing.Point(4, 22);
            this.TabPageConfig.Name = "TabPageConfig";
            this.TabPageConfig.Padding = new System.Windows.Forms.Padding(3);
            this.TabPageConfig.Size = new System.Drawing.Size(552, 312);
            this.TabPageConfig.TabIndex = 0;
            this.TabPageConfig.Text = "Configuration";
            this.TabPageConfig.UseVisualStyleBackColor = true;
            // 
            // label2
            // 
            this.label2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(6, 138);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(259, 13);
            this.label2.TabIndex = 8;
            this.label2.Text = "Selected entries can be removed using the [DEL] key";
            // 
            // BtnAdd
            // 
            this.BtnAdd.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.BtnAdd.Location = new System.Drawing.Point(471, 133);
            this.BtnAdd.Name = "BtnAdd";
            this.BtnAdd.Size = new System.Drawing.Size(75, 23);
            this.BtnAdd.TabIndex = 7;
            this.BtnAdd.Text = "&Add...";
            this.BtnAdd.UseVisualStyleBackColor = true;
            this.BtnAdd.Click += new System.EventHandler(this.BtnAdd_Click);
            // 
            // LvMonitoredDevices
            // 
            this.LvMonitoredDevices.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.LvMonitoredDevices.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.CHDeviceId,
            this.CHDeviceName});
            this.LvMonitoredDevices.FullRowSelect = true;
            this.LvMonitoredDevices.HideSelection = false;
            this.LvMonitoredDevices.Location = new System.Drawing.Point(6, 28);
            this.LvMonitoredDevices.Name = "LvMonitoredDevices";
            this.LvMonitoredDevices.Size = new System.Drawing.Size(540, 99);
            this.LvMonitoredDevices.TabIndex = 6;
            this.LvMonitoredDevices.UseCompatibleStateImageBehavior = false;
            this.LvMonitoredDevices.View = System.Windows.Forms.View.Details;
            this.LvMonitoredDevices.KeyDown += new System.Windows.Forms.KeyEventHandler(this.LvMonitoredDevices_KeyDown);
            // 
            // CHDeviceId
            // 
            this.CHDeviceId.Text = "Device Id";
            this.CHDeviceId.Width = 250;
            // 
            // CHDeviceName
            // 
            this.CHDeviceName.Text = "Display Name";
            this.CHDeviceName.Width = 250;
            // 
            // CbKeepAudioBusy
            // 
            this.CbKeepAudioBusy.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.CbKeepAudioBusy.AutoSize = true;
            this.CbKeepAudioBusy.Location = new System.Drawing.Point(9, 167);
            this.CbKeepAudioBusy.Name = "CbKeepAudioBusy";
            this.CbKeepAudioBusy.Size = new System.Drawing.Size(146, 17);
            this.CbKeepAudioBusy.TabIndex = 5;
            this.CbKeepAudioBusy.Text = "Fix bad audio stream start";
            this.CbKeepAudioBusy.UseVisualStyleBackColor = true;
            // 
            // LblPathWarning
            // 
            this.LblPathWarning.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.LblPathWarning.AutoEllipsis = true;
            this.LblPathWarning.ForeColor = System.Drawing.Color.Red;
            this.LblPathWarning.Location = new System.Drawing.Point(6, 223);
            this.LblPathWarning.Name = "LblPathWarning";
            this.LblPathWarning.Size = new System.Drawing.Size(540, 57);
            this.LblPathWarning.TabIndex = 4;
            this.LblPathWarning.Text = resources.GetString("LblPathWarning.Text");
            this.LblPathWarning.Visible = false;
            // 
            // BtnSave
            // 
            this.BtnSave.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.BtnSave.Location = new System.Drawing.Point(471, 283);
            this.BtnSave.Name = "BtnSave";
            this.BtnSave.Size = new System.Drawing.Size(75, 23);
            this.BtnSave.TabIndex = 2;
            this.BtnSave.Text = "&Save";
            this.BtnSave.UseVisualStyleBackColor = true;
            this.BtnSave.Click += new System.EventHandler(this.BtnSave_Click);
            // 
            // label8
            // 
            this.label8.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label8.AutoEllipsis = true;
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(6, 187);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(529, 26);
            this.label8.TabIndex = 3;
            this.label8.Text = "Enable this option if the beginning of audio is cut off, or plays 1-2 millisecond" +
    "s of garbage when playback starts.\r\nThere are usually no negative side effects i" +
    "f this option is enabled but not needed";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 12);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(123, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Monitored audio devices";
            // 
            // TabPageService
            // 
            this.TabPageService.Controls.Add(this.groupBox4);
            this.TabPageService.Controls.Add(this.groupBox3);
            this.TabPageService.Controls.Add(this.groupBox2);
            this.TabPageService.Controls.Add(this.groupBox1);
            this.TabPageService.Location = new System.Drawing.Point(4, 22);
            this.TabPageService.Name = "TabPageService";
            this.TabPageService.Padding = new System.Windows.Forms.Padding(3);
            this.TabPageService.Size = new System.Drawing.Size(552, 312);
            this.TabPageService.TabIndex = 1;
            this.TabPageService.Text = "Service Control";
            this.TabPageService.UseVisualStyleBackColor = true;
            // 
            // groupBox4
            // 
            this.groupBox4.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox4.Controls.Add(this.label6);
            this.groupBox4.Controls.Add(this.BtnServiceReset);
            this.groupBox4.Location = new System.Drawing.Point(6, 234);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(540, 70);
            this.groupBox4.TabIndex = 3;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "Bluetooth Audio Gateway Service";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(6, 45);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(467, 13);
            this.label6.TabIndex = 3;
            this.label6.Text = "This restores the broken Windows 11 behavior until you start the audio fix servic" +
    "e for the next time";
            // 
            // BtnServiceReset
            // 
            this.BtnServiceReset.Location = new System.Drawing.Point(6, 19);
            this.BtnServiceReset.Name = "BtnServiceReset";
            this.BtnServiceReset.Size = new System.Drawing.Size(75, 23);
            this.BtnServiceReset.TabIndex = 2;
            this.BtnServiceReset.Text = "Reset";
            this.BtnServiceReset.UseVisualStyleBackColor = true;
            this.BtnServiceReset.Click += new System.EventHandler(this.BtnServiceReset_Click);
            // 
            // groupBox3
            // 
            this.groupBox3.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox3.Controls.Add(this.label5);
            this.groupBox3.Controls.Add(this.BtnServiceStop);
            this.groupBox3.Controls.Add(this.BtnServiceStart);
            this.groupBox3.Location = new System.Drawing.Point(6, 158);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(540, 70);
            this.groupBox3.TabIndex = 3;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Service start/stop";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(6, 45);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(341, 13);
            this.label5.TabIndex = 3;
            this.label5.Text = "Unless the service is disabled, it will start again on the next system boot";
            // 
            // BtnServiceStop
            // 
            this.BtnServiceStop.Location = new System.Drawing.Point(87, 19);
            this.BtnServiceStop.Name = "BtnServiceStop";
            this.BtnServiceStop.Size = new System.Drawing.Size(75, 23);
            this.BtnServiceStop.TabIndex = 2;
            this.BtnServiceStop.Text = "Stop";
            this.BtnServiceStop.UseVisualStyleBackColor = true;
            this.BtnServiceStop.Click += new System.EventHandler(this.BtnServiceStop_Click);
            // 
            // BtnServiceStart
            // 
            this.BtnServiceStart.Location = new System.Drawing.Point(6, 19);
            this.BtnServiceStart.Name = "BtnServiceStart";
            this.BtnServiceStart.Size = new System.Drawing.Size(75, 23);
            this.BtnServiceStart.TabIndex = 2;
            this.BtnServiceStart.Text = "Start";
            this.BtnServiceStart.UseVisualStyleBackColor = true;
            this.BtnServiceStart.Click += new System.EventHandler(this.BtnServiceStart_Click);
            // 
            // groupBox2
            // 
            this.groupBox2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox2.Controls.Add(this.label4);
            this.groupBox2.Controls.Add(this.BtnServiceDisable);
            this.groupBox2.Controls.Add(this.BtnServiceEnable);
            this.groupBox2.Location = new System.Drawing.Point(6, 82);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(540, 70);
            this.groupBox2.TabIndex = 3;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Service enable/disable";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(6, 45);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(471, 13);
            this.label4.TabIndex = 3;
            this.label4.Text = "You can disable the service to keep it installed but prevent it from starting man" +
    "ually or automatically";
            // 
            // BtnServiceDisable
            // 
            this.BtnServiceDisable.Location = new System.Drawing.Point(87, 19);
            this.BtnServiceDisable.Name = "BtnServiceDisable";
            this.BtnServiceDisable.Size = new System.Drawing.Size(75, 23);
            this.BtnServiceDisable.TabIndex = 2;
            this.BtnServiceDisable.Text = "Disable";
            this.BtnServiceDisable.UseVisualStyleBackColor = true;
            this.BtnServiceDisable.Click += new System.EventHandler(this.BtnServiceDisable_Click);
            // 
            // BtnServiceEnable
            // 
            this.BtnServiceEnable.Location = new System.Drawing.Point(6, 19);
            this.BtnServiceEnable.Name = "BtnServiceEnable";
            this.BtnServiceEnable.Size = new System.Drawing.Size(75, 23);
            this.BtnServiceEnable.TabIndex = 2;
            this.BtnServiceEnable.Text = "Enable";
            this.BtnServiceEnable.UseVisualStyleBackColor = true;
            this.BtnServiceEnable.Click += new System.EventHandler(this.BtnServiceEnable_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.BtnServiceUninstall);
            this.groupBox1.Controls.Add(this.BtnServiceInstall);
            this.groupBox1.Location = new System.Drawing.Point(6, 6);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(540, 70);
            this.groupBox1.TabIndex = 3;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Service installation";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(6, 45);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(392, 13);
            this.label3.TabIndex = 3;
            this.label3.Text = "You must uninstall the service before moving this application to a different loca" +
    "tion";
            // 
            // BtnServiceUninstall
            // 
            this.BtnServiceUninstall.Location = new System.Drawing.Point(87, 19);
            this.BtnServiceUninstall.Name = "BtnServiceUninstall";
            this.BtnServiceUninstall.Size = new System.Drawing.Size(75, 23);
            this.BtnServiceUninstall.TabIndex = 2;
            this.BtnServiceUninstall.Text = "Uninstall";
            this.BtnServiceUninstall.UseVisualStyleBackColor = true;
            this.BtnServiceUninstall.Click += new System.EventHandler(this.BtnServiceUninstall_Click);
            // 
            // BtnServiceInstall
            // 
            this.BtnServiceInstall.Location = new System.Drawing.Point(6, 19);
            this.BtnServiceInstall.Name = "BtnServiceInstall";
            this.BtnServiceInstall.Size = new System.Drawing.Size(75, 23);
            this.BtnServiceInstall.TabIndex = 2;
            this.BtnServiceInstall.Text = "Install";
            this.BtnServiceInstall.UseVisualStyleBackColor = true;
            this.BtnServiceInstall.Click += new System.EventHandler(this.BtnServiceInstall_Click);
            // 
            // TabPageManual
            // 
            this.TabPageManual.Controls.Add(this.LbManualLog);
            this.TabPageManual.Controls.Add(this.BtnManualStop);
            this.TabPageManual.Controls.Add(this.BtnManualStart);
            this.TabPageManual.Controls.Add(this.label7);
            this.TabPageManual.Location = new System.Drawing.Point(4, 22);
            this.TabPageManual.Name = "TabPageManual";
            this.TabPageManual.Size = new System.Drawing.Size(552, 312);
            this.TabPageManual.TabIndex = 2;
            this.TabPageManual.Text = "Manual Operation";
            this.TabPageManual.UseVisualStyleBackColor = true;
            // 
            // LbManualLog
            // 
            this.LbManualLog.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.LbManualLog.FormattingEnabled = true;
            this.LbManualLog.Location = new System.Drawing.Point(19, 107);
            this.LbManualLog.Name = "LbManualLog";
            this.LbManualLog.Size = new System.Drawing.Size(521, 199);
            this.LbManualLog.TabIndex = 2;
            // 
            // BtnManualStop
            // 
            this.BtnManualStop.Location = new System.Drawing.Point(100, 78);
            this.BtnManualStop.Name = "BtnManualStop";
            this.BtnManualStop.Size = new System.Drawing.Size(75, 23);
            this.BtnManualStop.TabIndex = 1;
            this.BtnManualStop.Text = "Stop";
            this.BtnManualStop.UseVisualStyleBackColor = true;
            this.BtnManualStop.Click += new System.EventHandler(this.BtnManualStop_Click);
            // 
            // BtnManualStart
            // 
            this.BtnManualStart.Location = new System.Drawing.Point(19, 78);
            this.BtnManualStart.Name = "BtnManualStart";
            this.BtnManualStart.Size = new System.Drawing.Size(75, 23);
            this.BtnManualStart.TabIndex = 1;
            this.BtnManualStart.Text = "Start";
            this.BtnManualStart.UseVisualStyleBackColor = true;
            this.BtnManualStart.Click += new System.EventHandler(this.BtnManualStart_Click);
            // 
            // label7
            // 
            this.label7.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label7.AutoEllipsis = true;
            this.label7.Location = new System.Drawing.Point(16, 15);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(524, 60);
            this.label7.TabIndex = 0;
            this.label7.Text = resources.GetString("label7.Text");
            // 
            // BtnClose
            // 
            this.BtnClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.BtnClose.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.BtnClose.Location = new System.Drawing.Point(493, 356);
            this.BtnClose.Name = "BtnClose";
            this.BtnClose.Size = new System.Drawing.Size(75, 23);
            this.BtnClose.TabIndex = 1;
            this.BtnClose.Text = "&Close";
            this.BtnClose.UseVisualStyleBackColor = true;
            this.BtnClose.Click += new System.EventHandler(this.BtnClose_Click);
            // 
            // FrmConfig
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.BtnClose;
            this.ClientSize = new System.Drawing.Size(584, 391);
            this.Controls.Add(this.BtnClose);
            this.Controls.Add(this.TabControlMain);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MinimumSize = new System.Drawing.Size(600, 430);
            this.Name = "FrmConfig";
            this.Text = "BTAF Service Configuration";
            this.TabControlMain.ResumeLayout(false);
            this.TabPageConfig.ResumeLayout(false);
            this.TabPageConfig.PerformLayout();
            this.TabPageService.ResumeLayout(false);
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.TabPageManual.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl TabControlMain;
        private System.Windows.Forms.TabPage TabPageConfig;
        private System.Windows.Forms.TabPage TabPageService;
        private System.Windows.Forms.Button BtnClose;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button BtnSave;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button BtnServiceUninstall;
        private System.Windows.Forms.Button BtnServiceInstall;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Button BtnServiceReset;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Button BtnServiceStop;
        private System.Windows.Forms.Button BtnServiceStart;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button BtnServiceDisable;
        private System.Windows.Forms.Button BtnServiceEnable;
        private System.Windows.Forms.Label LblPathWarning;
        private System.Windows.Forms.TabPage TabPageManual;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Button BtnManualStop;
        private System.Windows.Forms.Button BtnManualStart;
        private System.Windows.Forms.ListBox LbManualLog;
        private System.Windows.Forms.CheckBox CbKeepAudioBusy;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.ListView LvMonitoredDevices;
        private System.Windows.Forms.ColumnHeader CHDeviceId;
        private System.Windows.Forms.ColumnHeader CHDeviceName;
        private System.Windows.Forms.Button BtnAdd;
        private System.Windows.Forms.Label label2;
    }
}