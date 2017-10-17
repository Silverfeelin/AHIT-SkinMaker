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
using System.Text;
using System.Windows.Forms;

namespace AHITSkinMaker
{
    public partial class MainForm : Form
    {
        DirectoryInfo _lastModDirectory;
        DirectoryInfo LastModDirectory
        {
            get
            {
                return _lastModDirectory;
            }
            set
            {
                _lastModDirectory = value;
                BtnOpenFolder.Enabled = value != null;
            }
        }

        string modIconPath;

        public MainForm()
        {
            InitializeComponent();

            string gameFolder = Properties.Settings.Default.GameFolder;
            if (!string.IsNullOrEmpty(gameFolder))
            {
                TbxGameFolder.Text = gameFolder;
                UpdateGameFolder();
            }

            TbxAuthor.Text = Properties.Settings.Default.Author;
        }

        #region Control Events

        private void BtnUpdateGameFolder_Click(object sender, EventArgs e)
        {
            UpdateGameFolder();
            Properties.Settings.Default.GameFolder = TbxGameFolder.Text;
            Properties.Settings.Default.Save();
        }

        private void UpdateGameFolder()
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

        private void BtnCreateMod_Click(object sender, EventArgs e)
        {
            BtnCompile.Enabled = false;
            BtnCook.Enabled = false;

            string modName = TbxModName.Text,
                gameFolder = TbxGameFolder.Text,
                author = TbxAuthor.Text,
                visualModName = TbxVisualModName.Text,
                description = TbxDescription.Text;

            // Check input validity
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
            if (string.IsNullOrWhiteSpace(visualModName))
                visualModName = "Change Me!";
            if (string.IsNullOrWhiteSpace(author))
                author = "Change Me!";

            if (!valid) return;

            // Update / New mod
            bool updateMod = false;

            if (LastModDirectory != null && LastModDirectory.Exists)
            {
                if (MessageBox.Show("Do you want to update the existing folder instead of creating a new one? This will wipe the existing data!",
                    "Warning",
                    MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    updateMod = true;
                }
            }

            AppendInfo("This should only take a second...");

            // Create modFolder
            if (!updateMod)
            {
                LastModDirectory = Directory.CreateDirectory(Path.Combine(gameFolder, "HatinTimeGame\\Mods", modName + "_" + Guid.NewGuid().ToString("N").Substring(0, 4)));
            }

            // Create subfolders
            RecreateSubDirectories(LastModDirectory);

            // Append Mod to mod name
            modName = modName.EndsWith("Mod") ? modName : modName + "Mod";

            // Skins
            List<Skin> skins = new List<Skin>(LbxSkins.Items.Count);
            foreach (var item in LbxSkins.Items)
            {
                skins.Add(item as Skin);
            }

            // Create modinfo.ini
            AppendInfo("Saving modinfo.ini...");
            CreateModIni(LastModDirectory, modName, visualModName, author, description, modIconPath);

            // GameMod class
            AppendInfo("Saving GameMod class...");
            CreateGameMod(LastModDirectory, modName, skins);

            // Int file
            AppendInfo("Saving localization file...");
            CreateLocalizationFile(LastModDirectory, skins);

            // Skin classes
            AppendInfo("Saving Skin classes...");
            SaveSkins(LastModDirectory, skins);

            AppendInfo("Done! Though compiling and cooking has yet to be added.");
            BtnCompile.Enabled = true;
        }

        private void BtnCompile_Click(object sender, EventArgs e)
        {
            if (!LastModDirectory.Exists)
            {
                AppendInfo("Mod directory does not exist!");
                BtnCompile.Enabled = false;
                return;
            }

            AppendInfo("Compiling. Close the window after it says it is done.");
            CompileMod(LastModDirectory, new FileInfo(TbxEditorExecutablePath.Text));
            AppendInfo("Compiling done. If there are no errors or warnings, you can cook the mod now.");

            BtnCook.Enabled = true;
        }

        private void BtnCook_Click(object sender, EventArgs e)
        {
            if (!LastModDirectory.Exists)
            {
                AppendInfo("Mod directory does not exist!");
                BtnCompile.Enabled = false;
                BtnCook.Enabled = false;
                return;
            }

            AppendInfo("Cooking. Close the window after it says it is done. This will take a while!");
            CookMod(LastModDirectory, new FileInfo(TbxEditorExecutablePath.Text));
            AppendInfo("Cooking done. Your mod should now work in-game!");
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

        private void TbxAuthor_TextChanged(object sender, EventArgs e)
        {
            Properties.Settings.Default.Author = TbxAuthor.Text;
            Properties.Settings.Default.Save();
        }

        private void BtnOpenFolder_Click(object sender, EventArgs e)
        {
            if (LastModDirectory != null && LastModDirectory.Exists)
            {
                Process.Start(LastModDirectory.FullName);
            }
        }

        #endregion

        private void AppendInfo(string text, params object[] args)
        {
            TbxInfo.AppendText(string.Format(text, args) + Environment.NewLine);
            TbxInfo.SelectionStart = TbxInfo.Text.Length;
            TbxInfo.ScrollToCaret();
        }

        private void RecreateSubDirectories(DirectoryInfo modDirectory)
        {
            string skinFolder = Path.Combine(modDirectory.FullName, "Classes\\Collectibles\\Skins"),
                gameModFolder = Path.Combine(modDirectory.FullName, "Classes\\GameMod"),
                intFolder = Path.Combine(modDirectory.FullName, "Localization\\INT");

            if (Directory.Exists(skinFolder)) Directory.Delete(skinFolder, true);
            if (Directory.Exists(gameModFolder)) Directory.Delete(gameModFolder, true);
            if (Directory.Exists(intFolder)) Directory.Delete(intFolder, true);

            Directory.CreateDirectory(skinFolder);
            Directory.CreateDirectory(gameModFolder);
            Directory.CreateDirectory(intFolder);
        }

        private void CreateModIni(DirectoryInfo modDirectory, string modName, string visualModName, string author, string description, string iconPath = null)
        {
            string modIni = Properties.Resources.Ini;
            modIni = modIni.Replace("{MODNAME}", modName);
            modIni = modIni.Replace("{NAME}", visualModName);
            modIni = modIni.Replace("{AUTHOR}", author);
            modIni = modIni.Replace("{DESCRIPTION}", description);

            if (!string.IsNullOrWhiteSpace(iconPath))
            {
                // Copy icon to mod folder
                string modIconFileName = "icon" + Path.GetExtension(iconPath);
                File.Copy(iconPath, Path.Combine(modDirectory.FullName, modIconFileName));
                modIni = modIni.Replace("{ICON}", modIconFileName);
            }
            else
            {
                // Set default.
                modIni = modIni.Replace("{ICON}", "icon.jpg");
            }

            File.WriteAllText(Path.Combine(modDirectory.FullName, "modinfo.ini"), modIni);
        }

        private void CreateGameMod(DirectoryInfo modDirectory, string modClassName, ICollection<Skin> skins)
        {
            // GameMod class
            string gameMod = Properties.Resources.GameMod;

            gameMod = gameMod.Replace("{MODNAME}", modClassName);

            StringBuilder addSkins = new StringBuilder(),
                removeSkins = new StringBuilder();

            foreach (Skin s in skins)
            {
                addSkins.Append(("Hat_PlayerController(GetALocalPlayerController()).GetLoadout().AddBackpack(class'Hat_Loadout'.static.MakeLoadoutItem(class'{SKINCLASSNAME}'), false);\n").Replace("{SKINCLASSNAME}", s.ClassName));
                removeSkins.Append(("Hat_PlayerController(GetALocalPlayerController()).GetLoadout().RemoveBackpack(class'Hat_Loadout'.static.MakeLoadoutItem(class'{SKINCLASSNAME}', class'Hat_ItemQuality_SearchAny'));\n").Replace("{SKINCLASSNAME}", s.ClassName));
            }

            gameMod = gameMod.Replace("{ADDSKINS}", addSkins.ToString());
            gameMod = gameMod.Replace("{REMOVESKINS}", removeSkins.ToString());
            
            File.WriteAllText(Path.Combine(modDirectory.FullName, "Classes\\GameMod", modClassName + ".uc"), gameMod);
        }

        private void CreateLocalizationFile(DirectoryInfo modDirectory, ICollection<Skin> skins)
        {
            if (skins.Count == 0) return;

            string localization = Properties.Resources.Int;
            StringBuilder s = new StringBuilder();

            foreach (Skin skin in skins)
            {
                s.AppendFormat("{0} = {1}\n", skin.TextLocalizationKey, skin.Text);
            }

            localization = localization.Replace("{SKINS}", s.ToString());

            string path = Path.Combine(modDirectory.FullName, "Localization\\INT\\collectibles.int");
            File.WriteAllText(path, localization);
        }

        /// <summary>
        /// Saves skins to class files to the given directory.
        /// </summary>
        private void SaveSkins(DirectoryInfo modDirectory, ICollection<Skin> skins)
        {
            DirectoryInfo skinDirectory = new DirectoryInfo(Path.Combine(modDirectory.FullName, "Classes\\Collectibles\\Skins"));

            foreach (Skin skin in skins)
            {
                string path = Path.Combine(skinDirectory.FullName, skin.ClassName + ".uc");
                File.WriteAllText(path, skin.ToClass());
            }
        }

        private void CompileMod(DirectoryInfo modDir, FileInfo fileEditor)
        {
            Process p = new Process();
            ProcessStartInfo psi = new ProcessStartInfo();
            p.StartInfo = psi;
            psi.FileName = fileEditor.FullName;
            psi.Arguments = "make -full";
            p.Start();

            p.WaitForExit();
        }

        private void CookMod(DirectoryInfo modDir, FileInfo fileEditor)
        {
            Process p = new Process();
            ProcessStartInfo psi = new ProcessStartInfo();
            p.StartInfo = psi;
            psi.FileName = fileEditor.FullName;
            psi.Arguments = "CookPackages -MODSONLY -platform=PC";

            p.Start();
            p.PriorityClass = ProcessPriorityClass.High;

            p.WaitForExit();
        }

        private void UpdatePreview(Dictionary<SkinColors, Color> colors)
        {
            PbxPreview.Image = TemplateManager.CreatePreview(colors);
        }
    }
}
