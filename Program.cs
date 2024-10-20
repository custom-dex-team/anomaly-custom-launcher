using AnomalyLauncher;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Security.Cryptography;
using System.Text;
using System.Windows.Forms;

namespace Launcher
{
    internal static class Program
    {
        public static readonly string launcherPath = AppDomain.CurrentDomain.BaseDirectory;
        public static readonly string parentPath = Directory.GetParent(launcherPath).FullName; 
        public static readonly string modOrganizerLtx = Path.Combine(parentPath, "Mod Organizer/overwrite/gamedata/configs/axr_options.ltx");
        public static readonly string modOrganizerLtxOld = Path.Combine(parentPath, "Mod Organizer/overwrite/gamedata/configs/axr_options.ltx.old");
        public static readonly string APPDATA = "appdata";
        private static readonly string BIN = "bin";
        private static readonly string GAMEDATA = "gamedata";
        private static readonly string TOOLS = "tools";
        public static readonly string USER_LTX = Path.Combine(Program.APPDATA, "user.ltx");
        public static readonly string USER_LTX_OLD = Path.Combine(Program.APPDATA, "user.ltx.old");
        private static readonly string SHADERS_CACHE = Path.Combine(Program.APPDATA, "shaders_cache");
        private static readonly string USER_LTX_UPDATE = string.Format("{0}.update", (object)Program.USER_LTX);
        public static readonly string ALSOFT_INI = Path.Combine(Program.BIN, "alsoft.ini");
        public static readonly string ALSOFT_INI_BAK = Path.Combine(Program.BIN, "alsoft.ini.bak");
        private static readonly string[] ALLOWED_FILES = new string[6]
        {
      "configs\\atmosfear_default_settings.ltx",
      "configs\\atmosfear_options.ltx",
      "configs\\axr_options.ltx",
      "configs\\cache_dbg.ltx",
      "configs\\localization.ltx",
      "configs\\warfare_options.ltx"
        };
        private static bool _deleteShaderCache;

        [STAThread]

        private static void Main(string[] args)
        {
            string directoryName = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            Directory.SetCurrentDirectory(directoryName);

            if (!Directory.Exists(Program.APPDATA))
                Directory.CreateDirectory(Program.APPDATA);

            if (!Directory.Exists(Program.GAMEDATA))
                Directory.CreateDirectory(Program.GAMEDATA);

            Program._deleteShaderCache = false;

            if (!Program.showSelectionUI())
                return;

            if (Directory.Exists(Program.APPDATA) && File.Exists(Program.USER_LTX_UPDATE))
            {
                string str1 = string.Empty;

                if (File.Exists(Program.USER_LTX))
                    str1 = File.ReadAllText(Program.USER_LTX);

                string str2 = File.ReadAllText(Program.USER_LTX_UPDATE);
                string contents = string.Format("{0}\n{1}", (object)str1, (object)str2);

                File.WriteAllText(Program.USER_LTX, contents);
                File.Delete(Program.USER_LTX_UPDATE);

                Program._deleteShaderCache = true;

            }

            string empty = string.Empty;

            if ((uint)args.Length > 0U)
                empty += string.Join(" ", args);

            string[] strArray = File.Exists("AnomalyLauncher.cfg") ? File.ReadAllLines("AnomalyLauncher.cfg") : new string[0];
            string str = "AnomalyDX9";

            if ((uint)strArray.Length > 0U)
            {
                if (strArray[0] == "DX11")
                    str = "AnomalyDX11";
            }

            if (Program.HasAvxSupport() && (strArray.Length < 2 || strArray[1] == "AVX"))
                str += "AVX";
            string path2 = str + ".exe";

            string fileName = Path.Combine(Path.Combine(directoryName, "bin"), path2);

            Process process = Process.Start(new ProcessStartInfo(fileName, empty)
            {
                WorkingDirectory = directoryName,
                UseShellExecute = false
            });

            if (process == null)
            {
                int num = (int)MessageBox.Show("Could not start " + fileName, "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Hand);
            }
            else
            {
                process.WaitForExit();
            }

        }


        private static void DeleteShaderCache()
        {
            if (Directory.Exists(Program.SHADERS_CACHE))
            {
                try
                {
                    Directory.Delete(Program.SHADERS_CACHE, true);
                    MessageBox.Show("Shader cache deleted successfully.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error deleting shader cache: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                MessageBox.Show("Shader cache directory does not exist.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private static bool showSelectionUI()
        {
            SelectionUI mainForm = new SelectionUI();
            mainForm.Show();
            Application.Run((Form)mainForm);
            Program._deleteShaderCache = mainForm.ShouldDeleteShaderCache;

            if (Program._deleteShaderCache)
            {
                DeleteShaderCache();
            }

            return mainForm.ShouldStart;
        }

        public static bool HasAvxSupport()
        {
            try
            {
                return ((ulong)Program.GetEnabledXStateFeatures() & 4UL) > 0UL;
            }
            catch
            {
                return false;
            }
        }
        public static bool CheckForAddons(Action<string> status)
        {
            bool flag = false;
            if (!Directory.Exists(Program.GAMEDATA))
                return flag;
            foreach (string file in Directory.GetFiles(Program.GAMEDATA, "*.*", SearchOption.AllDirectories))
            {
                status(string.Format("Detected {0}", (object)file));
                if (!Program.IsAllowedFile(file))
                {
                    flag = true;
                    break;
                }
            }
            return flag;
        }

        private static bool IsAllowedFile(string filename)
        {
            foreach (string path2 in Program.ALLOWED_FILES)
            {
                if (Path.Combine(Program.GAMEDATA, path2) == filename)
                    return true;
            }
            return false;
        }

        [DllImport("kernel32.dll")]
        private static extern long GetEnabledXStateFeatures();
    }
}

