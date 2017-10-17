/* Made by Silverfeelin
 * Licensed under a MIT license: https://github.com/Silverfeelin/AHIT-SkinMaker/blob/master/LICENSE.md
 */
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Windows.Forms;

namespace AHITSkinMaker
{
    public partial class MainForm : Form
    {
        DirectoryInfo lastModDirectory;
        string modIconPath;

        public MainForm()
        {
            InitializeComponent();
            
            string gameFolder = Properties.Settings.Default.GameFolder;
            if (!string.IsNullOrEmpty(gameFolder))
            {
                TbxGameFolder.Text = gameFolder;
                UpdateGameInfo();
            }
        }

        private void BtnUpdateGameFolder_Click(object sender, EventArgs e)
        {
            UpdateGameInfo();
            Properties.Settings.Default.GameFolder = TbxGameFolder.Text;
            Properties.Settings.Default.Save();
        }

        private void UpdateGameInfo()
        {
            bool valid = true;
            string path = TbxGameFolder.Text;

            TbxEditorExecutablePath.Text = "";

            if (!Directory.Exists(path))
            {
                AppendInfo("The given path does not exist. Please enter the full path to your A Hat in Time game folder.");
                BtnCreateMod.Enabled = false;
                return;
            }
            
            string bit = Environment.Is64BitOperatingSystem ? "Win64" : "Win32";
            TbxEditorExecutablePath.Text = Path.Combine(path, "Binaries\\" + bit + "\\HatinTimeEditor.exe");
            
            if (!File.Exists(TbxEditorExecutablePath.Text))
            {
                AppendInfo("The Editor executable could not be found. Are you on the modding beta branch?");
                valid = false;
            }

            BtnCreateMod.Enabled = valid;
        }

        private void AppendInfo(string text, params object[] args)
        {
            TbxInfo.AppendText(string.Format(text, args) + Environment.NewLine);
            TbxInfo.SelectionStart = TbxInfo.Text.Length;
            TbxInfo.ScrollToCaret();
        }

        private void BtnCreateMod_Click(object sender, EventArgs e)
        {
            BtnCompile.Enabled = false;
            BtnCook.Enabled = false;

            string modName = TbxModName.Text,
                gameFolder = TbxGameFolder.Text;

            bool valid = true;

            if (string.IsNullOrWhiteSpace(modName) || modName.Contains(" "))
            {
                AppendInfo("Please enter a mod name without spaces.");
                valid = false;
            }
            if (LbxSkins.Items.Count == 0)
            {
                AppendInfo("Please add one or more skins.");
                valid = false;
            }

            if (!valid) return;

            AppendInfo("Hang tight.. this will take a while.");
            
            // Modfolder
            lastModDirectory = Directory.CreateDirectory(Path.Combine(gameFolder, "HatinTimeGame\\Mods", modName + "_" + Guid.NewGuid().ToString("N").Substring(0, 4)));
            string modFolderPath = lastModDirectory.FullName;

            modName = modName.EndsWith("Mod") ? modName : modName + "Mod";

            // Modinfo.ini
            string modIni = Properties.Resources.Ini.Replace("{MODNAME}", modName);
            if (!string.IsNullOrWhiteSpace(modIconPath))
            {
                string modIconFileName = "icon" + Path.GetExtension(modIconPath);
                File.Copy(modIconPath, Path.Combine(modFolderPath, modIconFileName));
                modIni = modIni.Replace("{ICON}", modIconFileName);
            }
            else
            {
                modIni = modIni.Replace("{ICON}", "icon.jpg");
            }
            File.WriteAllText(Path.Combine(modFolderPath, "modinfo.ini"), modIni);

            // Classes folder
            string classesFolderPath = Path.Combine(modFolderPath, "Classes");
            Directory.CreateDirectory(classesFolderPath);
            
            // GameMod class
            string gameMod = Properties.Resources.GameMod.Replace("{MODNAME}", modName);
            string gameModAddSkins = "";
            string gameModRemoveSkins = "";

            // Int file
            string intPath = Path.Combine(modFolderPath, "Localization\\INT");
            string localization = Properties.Resources.Int;
            string localizationSkins = "";

            // Skins
            foreach (var item in LbxSkins.Items)
            {
                // Create class
                Skin s = item as Skin;
                File.WriteAllText(Path.Combine(classesFolderPath, s.ClassName + ".uc"), s.ToClass());

                // Add to GameMod
                gameModAddSkins += ("Hat_PlayerController(GetALocalPlayerController()).GetLoadout().AddBackpack(class'Hat_Loadout'.static.MakeLoadoutItem(class'{SKINCLASSNAME}'), false);\n").Replace("{SKINCLASSNAME}", s.ClassName);
                gameModRemoveSkins += ("Hat_PlayerController(GetALocalPlayerController()).GetLoadout().RemoveBackpack(class'Hat_Loadout'.static.MakeLoadoutItem(class'{SKINCLASSNAME}', class'Hat_ItemQuality_SearchAny'));\n").Replace("{SKINCLASSNAME}", s.ClassName);

                // Add to INT
                localizationSkins += string.Format("{0} = {1}\n", s.TextLocalizationKey, s.Text);
            }

            // Save GameMod
            gameMod = gameMod.Replace("{ADDSKINS}", gameModAddSkins).Replace("{REMOVESKINS}", gameModRemoveSkins);
            File.WriteAllText(Path.Combine(classesFolderPath, modName + ".uc"), gameMod);

            // Save INT
            localization = localization.Replace("{SKINS}", localizationSkins);
            Directory.CreateDirectory(intPath);
            File.WriteAllText(Path.Combine(intPath, "collectibles.int"), localization);

            AppendInfo("Done! Though compiling and cooking has yet to be added.");
            BtnCompile.Enabled = true;
        }

        public static int Clamp(int v, int low, int high)
        {
            return v < low ? low : v > high ? high : v;
        }

        private void BtnCompile_Click(object sender, EventArgs e)
        {
            if (!lastModDirectory.Exists)
            {
                AppendInfo("Mod directory does not exist!");
                BtnCompile.Enabled = false;
                return;
            }

            AppendInfo("Compiling...");

            Process p = new Process();
            ProcessStartInfo psi = new ProcessStartInfo();
            p.StartInfo = psi;
            psi.FileName = TbxEditorExecutablePath.Text;
            psi.Arguments = "make -full";
            p.Start();
            
            p.WaitForExit();
            AppendInfo("Compiling done. If there are no errors or warnings, you can cook the mod now.");
            BtnCook.Enabled = true;
        }

        private void BtnCook_Click(object sender, EventArgs e)
        {
            Process p = new Process();
            ProcessStartInfo psi = new ProcessStartInfo();
            p.StartInfo = psi;
            psi.FileName = TbxEditorExecutablePath.Text;
            psi.Arguments = "CookPackages -MODSONLY -platform=PC";
            p.Start();
            p.PriorityClass = ProcessPriorityClass.AboveNormal;
            p.WaitForExit();

            AppendInfo("Cooking done. Your mod should now work in-game!");
        }

        private void BtnAddSkin_Click(object sender, EventArgs e)
        {
            AddSkinForm asf = new AddSkinForm(TbxModName.Text);
            if (asf.ShowDialog() != DialogResult.OK) return;

            LbxSkins.Items.Add(asf.Result);
            LbxSkins.SelectedIndex = LbxSkins.Items.Count - 1;
        }

        private void BtnRemoveSkin_Click(object sender, EventArgs e)
        {
            if (LbxSkins.SelectedIndex != -1)
            {
                LbxSkins.Items.RemoveAt(LbxSkins.SelectedIndex);
            }
        }

        private void LbxSkins_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (LbxSkins.SelectedIndex == -1)
            {
                PbxPreview.Image = Properties.Resources.Template;
            }
            else
            {
                Skin s = LbxSkins.SelectedItem as Skin;
                UpdatePreview(s.Colors);
            }
        }

        private void UpdatePreview(Dictionary<SkinColors, Color> colors)
        {
            PbxPreview.Image = TemplateManager.CreatePreview(colors);
        }

        private void BtnSelectIcon_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "Image Files|*.png;*.bmp;*.jpg";

            if (ofd.ShowDialog() != DialogResult.OK) return;
            
            try
            {
                Bitmap c;
                using (Bitmap b = new Bitmap(ofd.FileName))
                {
                    c = new Bitmap(b);
                }

                if (c.Width != c.Height)
                {
                    MessageBox.Show("Mod icon must have equal dimensions (I.e. 512x512)!");
                    modIconPath = null;
                }
                else
                {
                    modIconPath = ofd.FileName;
                    PbxIcon.Image = c;
                }
            }
            catch
            {
                MessageBox.Show("Could not load image.");
                modIconPath = null;
            }
        }

        private void BtnChangeSkin_Click(object sender, EventArgs e)
        {
            Skin s = LbxSkins.SelectedItem as Skin;
            if (s != null)
            {
                AddSkinForm asf = new AddSkinForm(s);
                asf.ShowDialog();

                LbxSkins.Refresh();
                UpdatePreview(s.Colors);
                LbxSkins.Items[LbxSkins.SelectedIndex] = LbxSkins.Items[LbxSkins.SelectedIndex]; // Why ;-; (Refreshes text)
            }
        }
    }
}
