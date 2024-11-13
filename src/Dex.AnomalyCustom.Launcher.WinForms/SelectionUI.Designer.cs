using AnomalyLauncher;

using System.Windows.Forms;

namespace Launcher
{
    partial class SelectionUI
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SelectionUI));
            this.checkBoxAVX = new System.Windows.Forms.CheckBox();
            this.labelWarning = new System.Windows.Forms.Label();
            this.comboBoxShadowMap = new System.Windows.Forms.ComboBox();
            this.checkBoxFixSound = new System.Windows.Forms.CheckBox();
            this.checkBoxPrefetchSounds = new System.Windows.Forms.CheckBox();
            this.radioButtonDX11 = new System.Windows.Forms.RadioButton();
            this.checkBoxResetSettings = new System.Windows.Forms.CheckBox();
            this.checkBoxDebugMode = new System.Windows.Forms.CheckBox();
            this.checkBoxRestoreMcmSettings = new System.Windows.Forms.CheckBox();
            this.buttonQuit = new RoundedButton();
            this.buttonSaveSettings = new RoundedButton();
            this.buttonDeleteShaderCache = new RoundedButton();
            this.buttonPlayAnomaly = new RoundedButton();
            this.SuspendLayout();
            // 
            // checkBoxAVX
            // 
            this.checkBoxAVX.AutoSize = true;
            this.checkBoxAVX.BackColor = System.Drawing.Color.Transparent;
            this.checkBoxAVX.Font = new System.Drawing.Font("Play", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.checkBoxAVX.ForeColor = System.Drawing.Color.DimGray;
            this.checkBoxAVX.Location = new System.Drawing.Point(12, 147);
            this.checkBoxAVX.Name = "checkBoxAVX";
            this.checkBoxAVX.Size = new System.Drawing.Size(178, 25);
            this.checkBoxAVX.TabIndex = 7;
            this.checkBoxAVX.Text = "Support for AVX CPU";
            this.checkBoxAVX.UseVisualStyleBackColor = false;
            // 
            // labelWarning
            // 
            this.labelWarning.AutoSize = true;
            this.labelWarning.BackColor = System.Drawing.Color.Transparent;
            this.labelWarning.ForeColor = System.Drawing.Color.Red;
            this.labelWarning.Location = new System.Drawing.Point(404, 446);
            this.labelWarning.Name = "labelWarning";
            this.labelWarning.Size = new System.Drawing.Size(0, 13);
            this.labelWarning.TabIndex = 8;
            // 
            // comboBoxShadowMap
            // 
            this.comboBoxShadowMap.BackColor = System.Drawing.Color.White;
            this.comboBoxShadowMap.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxShadowMap.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.comboBoxShadowMap.Font = new System.Drawing.Font("Play", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.comboBoxShadowMap.ForeColor = System.Drawing.Color.Black;
            this.comboBoxShadowMap.FormattingEnabled = true;
            this.comboBoxShadowMap.Items.AddRange(new object[] {
            "Shadow Map 1536",
            "Shadow Map 2048",
            "Shadow Map 2560",
            "Shadow Map 3072",
            "Shadow Map 4096"});
            this.comboBoxShadowMap.Location = new System.Drawing.Point(12, 178);
            this.comboBoxShadowMap.Name = "comboBoxShadowMap";
            this.comboBoxShadowMap.Size = new System.Drawing.Size(180, 29);
            this.comboBoxShadowMap.TabIndex = 11;
            // 
            // checkBoxFixSound
            // 
            this.checkBoxFixSound.AutoSize = true;
            this.checkBoxFixSound.BackColor = System.Drawing.Color.Transparent;
            this.checkBoxFixSound.Font = new System.Drawing.Font("Play", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.checkBoxFixSound.ForeColor = System.Drawing.Color.DimGray;
            this.checkBoxFixSound.Location = new System.Drawing.Point(12, 240);
            this.checkBoxFixSound.Name = "checkBoxFixSound";
            this.checkBoxFixSound.Size = new System.Drawing.Size(154, 46);
            this.checkBoxFixSound.TabIndex = 2;
            this.checkBoxFixSound.Text = " Sounds problem \r\n  workaround";
            this.checkBoxFixSound.UseVisualStyleBackColor = false;
            // 
            // checkBoxPrefetchSounds
            // 
            this.checkBoxPrefetchSounds.AutoSize = true;
            this.checkBoxPrefetchSounds.BackColor = System.Drawing.Color.Transparent;
            this.checkBoxPrefetchSounds.Font = new System.Drawing.Font("Play", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.checkBoxPrefetchSounds.ForeColor = System.Drawing.Color.DimGray;
            this.checkBoxPrefetchSounds.Location = new System.Drawing.Point(12, 323);
            this.checkBoxPrefetchSounds.Name = "checkBoxPrefetchSounds";
            this.checkBoxPrefetchSounds.Size = new System.Drawing.Size(146, 25);
            this.checkBoxPrefetchSounds.TabIndex = 3;
            this.checkBoxPrefetchSounds.Text = "Prefetch sounds";
            this.checkBoxPrefetchSounds.UseVisualStyleBackColor = false;
            // 
            // radioButtonDX11
            // 
            this.radioButtonDX11.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.radioButtonDX11.BackColor = System.Drawing.Color.Transparent;
            this.radioButtonDX11.Checked = true;
            this.radioButtonDX11.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.radioButtonDX11.Font = new System.Drawing.Font("Play", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.radioButtonDX11.ForeColor = System.Drawing.Color.DimGray;
            this.radioButtonDX11.Location = new System.Drawing.Point(12, 117);
            this.radioButtonDX11.Name = "radioButtonDX11";
            this.radioButtonDX11.Size = new System.Drawing.Size(220, 24);
            this.radioButtonDX11.TabIndex = 3;
            this.radioButtonDX11.TabStop = true;
            this.radioButtonDX11.Text = "DirectX 11 (R4)";
            this.radioButtonDX11.UseVisualStyleBackColor = false;
            // 
            // checkBoxResetSettings
            // 
            this.checkBoxResetSettings.BackColor = System.Drawing.Color.Transparent;
            this.checkBoxResetSettings.FlatAppearance.BorderColor = System.Drawing.Color.Black;
            this.checkBoxResetSettings.FlatAppearance.BorderSize = 0;
            this.checkBoxResetSettings.Font = new System.Drawing.Font("Play", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.checkBoxResetSettings.ForeColor = System.Drawing.Color.DimGray;
            this.checkBoxResetSettings.Location = new System.Drawing.Point(12, 375);
            this.checkBoxResetSettings.Margin = new System.Windows.Forms.Padding(3, 5, 3, 3);
            this.checkBoxResetSettings.Name = "checkBoxResetSettings";
            this.checkBoxResetSettings.Size = new System.Drawing.Size(202, 33);
            this.checkBoxResetSettings.TabIndex = 3;
            this.checkBoxResetSettings.Text = "Restore default user.ltx";
            this.checkBoxResetSettings.UseVisualStyleBackColor = false;
            // 
            // checkBoxDebugMode
            // 
            this.checkBoxDebugMode.AutoSize = true;
            this.checkBoxDebugMode.BackColor = System.Drawing.Color.Transparent;
            this.checkBoxDebugMode.Checked = true;
            this.checkBoxDebugMode.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBoxDebugMode.Font = new System.Drawing.Font("Play", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.checkBoxDebugMode.ForeColor = System.Drawing.Color.DimGray;
            this.checkBoxDebugMode.Location = new System.Drawing.Point(12, 292);
            this.checkBoxDebugMode.Name = "checkBoxDebugMode";
            this.checkBoxDebugMode.Size = new System.Drawing.Size(121, 25);
            this.checkBoxDebugMode.TabIndex = 13;
            this.checkBoxDebugMode.Text = "Debug Mode";
            this.checkBoxDebugMode.UseVisualStyleBackColor = false;
            // 
            // checkBoxRestoreMcmSettings
            // 
            this.checkBoxRestoreMcmSettings.AutoSize = true;
            this.checkBoxRestoreMcmSettings.BackColor = System.Drawing.Color.Transparent;
            this.checkBoxRestoreMcmSettings.Font = new System.Drawing.Font("Play", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.checkBoxRestoreMcmSettings.ForeColor = System.Drawing.Color.DimGray;
            this.checkBoxRestoreMcmSettings.Location = new System.Drawing.Point(12, 414);
            this.checkBoxRestoreMcmSettings.Name = "checkBoxRestoreMcmSettings";
            this.checkBoxRestoreMcmSettings.Size = new System.Drawing.Size(190, 25);
            this.checkBoxRestoreMcmSettings.TabIndex = 14;
            this.checkBoxRestoreMcmSettings.Text = "Restore MCM settings";
            this.checkBoxRestoreMcmSettings.UseVisualStyleBackColor = false;
            // 
            // buttonQuit
            // 
            this.buttonQuit.BackColor = System.Drawing.Color.Transparent;
            this.buttonQuit.BorderColor = System.Drawing.Color.Transparent;
            this.buttonQuit.BorderTickness = 0.5F;
            this.buttonQuit.ButtonBackColor = System.Drawing.Color.Black;
            this.buttonQuit.ButtonOpacity = 0.5F;
            this.buttonQuit.FlatAppearance.BorderSize = 0;
            this.buttonQuit.FlatAppearance.CheckedBackColor = System.Drawing.Color.Transparent;
            this.buttonQuit.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.buttonQuit.FlatAppearance.MouseOverBackColor = System.Drawing.Color.DimGray;
            this.buttonQuit.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonQuit.Font = new System.Drawing.Font("Play", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.buttonQuit.ForeColor = System.Drawing.Color.Snow;
            this.buttonQuit.Location = new System.Drawing.Point(666, 391);
            this.buttonQuit.Name = "buttonQuit";
            this.buttonQuit.RoundRadius = 1;
            this.buttonQuit.Size = new System.Drawing.Size(265, 48);
            this.buttonQuit.TabIndex = 12;
            this.buttonQuit.Text = "Quit";
            this.buttonQuit.UseVisualStyleBackColor = false;
            this.buttonQuit.Click += new System.EventHandler(this.buttonQuit_Click);
            // 
            // buttonSaveSettings
            // 
            this.buttonSaveSettings.BackColor = System.Drawing.Color.Transparent;
            this.buttonSaveSettings.BorderColor = System.Drawing.Color.Transparent;
            this.buttonSaveSettings.BorderTickness = 0.5F;
            this.buttonSaveSettings.ButtonBackColor = System.Drawing.Color.Black;
            this.buttonSaveSettings.ButtonOpacity = 0.5F;
            this.buttonSaveSettings.FlatAppearance.BorderSize = 0;
            this.buttonSaveSettings.FlatAppearance.CheckedBackColor = System.Drawing.Color.Transparent;
            this.buttonSaveSettings.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.buttonSaveSettings.FlatAppearance.MouseOverBackColor = System.Drawing.Color.DimGray;
            this.buttonSaveSettings.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonSaveSettings.Font = new System.Drawing.Font("Play", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.buttonSaveSettings.ForeColor = System.Drawing.Color.Snow;
            this.buttonSaveSettings.Location = new System.Drawing.Point(666, 346);
            this.buttonSaveSettings.Name = "buttonSaveSettings";
            this.buttonSaveSettings.RoundRadius = 1;
            this.buttonSaveSettings.Size = new System.Drawing.Size(265, 39);
            this.buttonSaveSettings.TabIndex = 11;
            this.buttonSaveSettings.Text = "Save settings";
            this.buttonSaveSettings.UseVisualStyleBackColor = false;
            this.buttonSaveSettings.Click += new System.EventHandler(this.buttonSaveSettings_Click);
            // 
            // buttonDeleteShaderCache
            // 
            this.buttonDeleteShaderCache.BackColor = System.Drawing.Color.Transparent;
            this.buttonDeleteShaderCache.BorderColor = System.Drawing.Color.Transparent;
            this.buttonDeleteShaderCache.BorderTickness = 0.5F;
            this.buttonDeleteShaderCache.ButtonBackColor = System.Drawing.Color.Black;
            this.buttonDeleteShaderCache.ButtonOpacity = 0.5F;
            this.buttonDeleteShaderCache.FlatAppearance.BorderSize = 0;
            this.buttonDeleteShaderCache.FlatAppearance.CheckedBackColor = System.Drawing.Color.Transparent;
            this.buttonDeleteShaderCache.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.buttonDeleteShaderCache.FlatAppearance.MouseOverBackColor = System.Drawing.Color.DimGray;
            this.buttonDeleteShaderCache.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonDeleteShaderCache.Font = new System.Drawing.Font("Play", 8.249999F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.buttonDeleteShaderCache.ForeColor = System.Drawing.Color.Snow;
            this.buttonDeleteShaderCache.Location = new System.Drawing.Point(666, 292);
            this.buttonDeleteShaderCache.Name = "buttonDeleteShaderCache";
            this.buttonDeleteShaderCache.RoundRadius = 1;
            this.buttonDeleteShaderCache.Size = new System.Drawing.Size(265, 48);
            this.buttonDeleteShaderCache.TabIndex = 10;
            this.buttonDeleteShaderCache.Text = "Delete Shader Cache";
            this.buttonDeleteShaderCache.UseVisualStyleBackColor = false;
            this.buttonDeleteShaderCache.Click += new System.EventHandler(this.buttonDeleteShaderCache_Click);
            // 
            // buttonPlayAnomaly
            // 
            this.buttonPlayAnomaly.BackColor = System.Drawing.Color.Transparent;
            this.buttonPlayAnomaly.BorderColor = System.Drawing.Color.Transparent;
            this.buttonPlayAnomaly.BorderTickness = 0.5F;
            this.buttonPlayAnomaly.ButtonBackColor = System.Drawing.Color.Black;
            this.buttonPlayAnomaly.ButtonOpacity = 0.5F;
            this.buttonPlayAnomaly.FlatAppearance.BorderSize = 0;
            this.buttonPlayAnomaly.FlatAppearance.CheckedBackColor = System.Drawing.Color.Transparent;
            this.buttonPlayAnomaly.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.buttonPlayAnomaly.FlatAppearance.MouseOverBackColor = System.Drawing.Color.DimGray;
            this.buttonPlayAnomaly.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonPlayAnomaly.Font = new System.Drawing.Font("Play", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.buttonPlayAnomaly.ForeColor = System.Drawing.Color.Snow;
            this.buttonPlayAnomaly.Location = new System.Drawing.Point(666, 230);
            this.buttonPlayAnomaly.Name = "buttonPlayAnomaly";
            this.buttonPlayAnomaly.RoundRadius = 1;
            this.buttonPlayAnomaly.Size = new System.Drawing.Size(265, 56);
            this.buttonPlayAnomaly.TabIndex = 9;
            this.buttonPlayAnomaly.Text = "Play S.T.A.L.K.E.R Anomaly Custom";
            this.buttonPlayAnomaly.UseVisualStyleBackColor = false;
            this.buttonPlayAnomaly.Click += new System.EventHandler(this.buttonPlayAnomaly_Click);
            // 
            // SelectionUI
            // 
            this.AccessibleDescription = "";
            this.AccessibleName = "";
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Inherit;
            this.AutoSize = true;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.ClientSize = new System.Drawing.Size(961, 468);
            this.Controls.Add(this.checkBoxRestoreMcmSettings);
            this.Controls.Add(this.checkBoxDebugMode);
            this.Controls.Add(this.comboBoxShadowMap);
            this.Controls.Add(this.checkBoxPrefetchSounds);
            this.Controls.Add(this.radioButtonDX11);
            this.Controls.Add(this.checkBoxFixSound);
            this.Controls.Add(this.buttonQuit);
            this.Controls.Add(this.buttonSaveSettings);
            this.Controls.Add(this.buttonDeleteShaderCache);
            this.Controls.Add(this.buttonPlayAnomaly);
            this.Controls.Add(this.labelWarning);
            this.Controls.Add(this.checkBoxAVX);
            this.Controls.Add(this.checkBoxResetSettings);
            this.ForeColor = System.Drawing.Color.Black;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "SelectionUI";
            this.Text = "Anomaly Custom Launcher";
            this.Load += new System.EventHandler(this.SelectionUI_Shown);
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        private CheckBox checkBoxAVX;
        private Label labelWarning;
        private ComboBox comboBoxShadowMap;
        private CheckBox checkBoxFixSound;
        private CheckBox checkBoxPrefetchSounds;
        private RoundedButton buttonQuit;
        private RoundedButton buttonSaveSettings;
        private RoundedButton buttonDeleteShaderCache;
        private RoundedButton buttonPlayAnomaly;
        private CheckBox checkBoxResetSettings;
        private CheckBox checkBoxDebugMode;
        private RadioButton radioButtonDX11;
        private CheckBox checkBoxRestoreMcmSettings;

        #endregion
    }
}
