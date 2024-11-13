using AnomalyLauncher;

using System;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Text;
using System.Windows.Forms;

namespace Launcher
{
    public partial class SelectionUI : Form
    {
        private bool _hasAvxSupport;
        public bool ShouldDeleteShaderCache { get; private set; } = false;

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
    }
}

