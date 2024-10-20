using AnomalyLauncher;
using AnomalyLauncher.Properties;
using AnomalyLauncher.Properties;
using System;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Text;
using System.Windows.Forms;

namespace Launcher
{
    public class SelectionUI : Form
    {
        private bool _hasAvxSupport;
        public bool ShouldDeleteShaderCache { get; private set; } = false;
        private IContainer components = (IContainer)null;
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


        public SelectionUI()
        {
            this.InitializeComponent();
            this.ShouldStart = false;
            this._hasAvxSupport = Program.HasAvxSupport();
            this.buttonPlayAnomaly.Focus();
            this.labelWarning.Visible = false;
            this.ReadConfig();
            this.Load += SelectionUI_Load;
        }

        private void SelectionUI_Load(object sender, EventArgs e)
        {
            byte[][] images = {
                AnomalyLauncher.Properties.Resources.LauncherCustom1,
                AnomalyLauncher.Properties.Resources.LauncherCustom2
            };

            Random rnd = new Random();
            int randomIndex = rnd.Next(0, images.Length);

            // Преобразуем выбранное изображение в Image и устанавливаем его как фон формы
            using (MemoryStream ms = new MemoryStream(images[randomIndex]))
            {
                this.BackgroundImage = Image.FromStream(ms);
                this.BackgroundImageLayout = ImageLayout.Stretch;
            }

        }

        private void ReadConfig()
        {
            string[] strArray = File.Exists("AnomalyLauncher.cfg") ? File.ReadAllLines("AnomalyLauncher.cfg") : new string[0];
            if ((uint)strArray.Length > 0U)
            {
                this.radioButtonDX11.Checked = strArray[0] == "DX11";
            }
            if (strArray.Length > 1)
            {
                this.checkBoxAVX.Checked = strArray[1] == "AVX";
            }
            if (strArray.Length > 2)
            {
                this.checkBoxDebugMode.Checked = strArray[2] == "DBG";
            }
            try
            {
                int num3 = strArray.Length > 3 ? int.Parse(strArray[3]) : 1;
                if (num3 < 0 || num3 > 4)
                    num3 = 1;
                this.comboBoxShadowMap.SelectedIndex = num3;
            }
            catch
            {
                this.comboBoxShadowMap.SelectedIndex = 1;
            }
            if (strArray.Length > 4)
            {
                this.checkBoxFixSound.Checked = strArray[4] == "SNDFIX";
            }
            if (strArray.Length > 5)
            {
                this.checkBoxPrefetchSounds.Checked = strArray[5] == "SNDPREFETCH";
            }
            if (!this._hasAvxSupport)
            {
                this.checkBoxAVX.Checked = false;
                this.checkBoxAVX.Enabled = false;
            }
        }

        private bool WriteConfig()
        {
            bool flag = true;
            string[] contents = new string[6]; 
            contents[0] = this.radioButtonDX11.Checked ? "DX11" : "DX9";
            contents[1] = this.checkBoxAVX.Checked ? "AVX" : "NOAVX";
            contents[2] = this.checkBoxDebugMode.Checked ? "DBG" : "NODBG"; 
            contents[3] = this.comboBoxShadowMap.SelectedIndex.ToString();
            contents[4] = this.checkBoxFixSound.Checked ? "SNDFIX" : "NOSNDFIX"; 
            contents[5] = this.checkBoxPrefetchSounds.Checked ? "SNDPREFETCH" : "NOSNDPREFETCH";
            File.WriteAllLines("AnomalyLauncher.cfg", contents);
            return flag;
        }

        public bool ShouldStart { get; private set; }

        private void buttonQuit_Click(object sender, EventArgs e) => this.Close();

        private void buttonPlayAnomaly_Click(object sender, EventArgs e)
        {
            if (!this.WriteConfig())
            {
                int num = (int)MessageBox.Show("Invalid screen resolution", "Anomaly Launcher", MessageBoxButtons.OK, MessageBoxIcon.Hand);
            }
            else
            {
                if (this.checkBoxResetSettings.Checked || !File.Exists(Program.USER_LTX) || this.checkBoxRestoreMcmSettings.Checked) {
                    this.ResetUserLtx();
                    this.ResetMcmSettings();
                }
                this.WriteCommandLine();
                this.ApplySoundFix();
                this.ShouldStart = true;
                this.Close();
            }
        }

        private void buttonDeleteShaderCache_Click(object sender, EventArgs e)
        {
            // Устанавливаем флаг на удаление кэша шейдеров
            ShouldDeleteShaderCache = true;
        }

        private void ResetMcmSettings() {
            if (File.Exists(Program.modOrganizerLtx)) {
                if (File.Exists(Program.modOrganizerLtxOld))
                    File.Delete(Program.modOrganizerLtxOld);
                File.Move(Program.modOrganizerLtx, Program.modOrganizerLtxOld);
            }
            File.WriteAllLines(Program.modOrganizerLtx, DefaultMcmLtx.LINES);
        }

        private void ResetUserLtx()
        {
            if (File.Exists(Program.USER_LTX))
            {
                if (File.Exists(Program.USER_LTX_OLD))
                    File.Delete(Program.USER_LTX_OLD);
                File.Move(Program.USER_LTX, Program.USER_LTX_OLD);
            }
            File.WriteAllLines(Program.USER_LTX, DefaultUserLtx.LINES);
        }

        private void ApplySoundFix()
        {
            if (this.checkBoxFixSound.Checked)
            {
                if (!File.Exists(Program.ALSOFT_INI))
                    return;
                if (File.Exists(Program.ALSOFT_INI_BAK))
                    File.Delete(Program.ALSOFT_INI_BAK);
                File.Move(Program.ALSOFT_INI, Program.ALSOFT_INI_BAK);
            }
            else
            {
                if (File.Exists(Program.ALSOFT_INI) || !File.Exists(Program.ALSOFT_INI_BAK))
                    return;
                File.Move(Program.ALSOFT_INI_BAK, Program.ALSOFT_INI);
            }
        }

        private void WriteCommandLine()
        {
            StringBuilder stringBuilder = new StringBuilder();
            switch (this.comboBoxShadowMap.SelectedIndex)
            {
                case 0:
                    stringBuilder.AppendLine("-smap1536");
                    break;
                case 2:
                    stringBuilder.AppendLine("-smap2560");
                    break;
                case 3:
                    stringBuilder.AppendLine("-smap3072");
                    break;
                case 4:
                    stringBuilder.AppendLine("-smap4096");
                    break;
                default:
                    stringBuilder.AppendLine("-smap2048");
                    break;
            }
            if (this.checkBoxDebugMode.Checked)
                stringBuilder.AppendLine("-dbg");
            if (this.checkBoxPrefetchSounds.Checked)
                stringBuilder.AppendLine("-prefetch_sounds");
            File.WriteAllText("commandline.txt", stringBuilder.ToString());
        }

        private void WriteToUserLtx()
        {
            string str1 = string.Empty;
            if (File.Exists(Program.USER_LTX))
                str1 = File.ReadAllText(Program.USER_LTX);
            StringBuilder stringBuilder = new StringBuilder();
            stringBuilder.AppendLine(str1);
            string str2 = "1920x1080";
            stringBuilder.AppendLine("vid_mode " + str2);
            stringBuilder.AppendLine("rs_borderless 0");
            stringBuilder.AppendLine("rs_fullscreen on");
            stringBuilder.AppendLine("rs_screenmode fullscreen");
            if (!Directory.Exists(Program.APPDATA))
                Directory.CreateDirectory(Program.APPDATA);
            File.WriteAllText(Program.USER_LTX, stringBuilder.ToString());
        }

        private void buttonSaveSettings_Click(object sender, EventArgs e)
        {
            this.WriteConfig();
            int num = (int)MessageBox.Show("Settings saved.", "Anomaly Launcher", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
        }

        private void SelectionUI_Shown(object sender, EventArgs e)
        {
            try
            {
                this.Enabled = false;
                if (!Program.CheckForAddons((Action<string>)(text =>
                {
                    this.labelWarning.Text = text;
                    Application.DoEvents();
                })))
                    return;
                this.labelWarning.Text = "* installed addons can cause problems or crashes *";
                this.labelWarning.Visible = true;
            }
            catch (Exception ex)
            {
                this.labelWarning.Text = string.Format("Exception: {0}", (object)ex);
                this.labelWarning.Visible = true;
            }
            finally
            {
                this.Enabled = true;
            }
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing && this.components != null)
                this.components.Dispose();
            base.Dispose(disposing);
        }

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
            this.checkBoxAVX.Location = new System.Drawing.Point(12, 95);
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
            this.labelWarning.Location = new System.Drawing.Point(442, 462);
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
            this.comboBoxShadowMap.Location = new System.Drawing.Point(12, 264);
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
            this.checkBoxFixSound.Location = new System.Drawing.Point(12, 139);
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
            this.checkBoxPrefetchSounds.Location = new System.Drawing.Point(12, 211);
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
            this.radioButtonDX11.Location = new System.Drawing.Point(12, 47);
            this.radioButtonDX11.Name = "radioButtonDX11";
            this.radioButtonDX11.Size = new System.Drawing.Size(233, 24);
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
            this.checkBoxResetSettings.Location = new System.Drawing.Point(12, 392);
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
            this.checkBoxDebugMode.Location = new System.Drawing.Point(12, 334);
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
            this.checkBoxRestoreMcmSettings.Location = new System.Drawing.Point(12, 462);
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
            this.buttonQuit.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.buttonQuit.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.buttonQuit.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonQuit.Font = new System.Drawing.Font("Play", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.buttonQuit.ForeColor = System.Drawing.Color.Snow;
            this.buttonQuit.Location = new System.Drawing.Point(641, 445);
            this.buttonQuit.Name = "buttonQuit";
            this.buttonQuit.RoundRadius = 1;
            this.buttonQuit.Size = new System.Drawing.Size(290, 57);
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
            this.buttonSaveSettings.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.buttonSaveSettings.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.buttonSaveSettings.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonSaveSettings.Font = new System.Drawing.Font("Play", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.buttonSaveSettings.ForeColor = System.Drawing.Color.Snow;
            this.buttonSaveSettings.Location = new System.Drawing.Point(641, 368);
            this.buttonSaveSettings.Name = "buttonSaveSettings";
            this.buttonSaveSettings.RoundRadius = 1;
            this.buttonSaveSettings.Size = new System.Drawing.Size(290, 57);
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
            this.buttonDeleteShaderCache.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.buttonDeleteShaderCache.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.buttonDeleteShaderCache.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonDeleteShaderCache.Font = new System.Drawing.Font("Play", 8.249999F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.buttonDeleteShaderCache.ForeColor = System.Drawing.Color.Snow;
            this.buttonDeleteShaderCache.Location = new System.Drawing.Point(641, 320);
            this.buttonDeleteShaderCache.Name = "buttonDeleteShaderCache";
            this.buttonDeleteShaderCache.RoundRadius = 1;
            this.buttonDeleteShaderCache.Size = new System.Drawing.Size(290, 39);
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
            this.buttonPlayAnomaly.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.buttonPlayAnomaly.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.buttonPlayAnomaly.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonPlayAnomaly.Font = new System.Drawing.Font("Play", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.buttonPlayAnomaly.ForeColor = System.Drawing.Color.Snow;
            this.buttonPlayAnomaly.Location = new System.Drawing.Point(641, 244);
            this.buttonPlayAnomaly.Name = "buttonPlayAnomaly";
            this.buttonPlayAnomaly.RoundRadius = 1;
            this.buttonPlayAnomaly.Size = new System.Drawing.Size(290, 66);
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
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.ClientSize = new System.Drawing.Size(974, 615);
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
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "SelectionUI";
            this.Text = "Anomaly Custom Launcher";
            this.Load += new System.EventHandler(this.SelectionUI_Shown);
            this.ResumeLayout(false);
            this.PerformLayout();

        }
    }
}

