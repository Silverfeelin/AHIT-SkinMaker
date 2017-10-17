/* Made by Silverfeelin
 * Licensed under a MIT license: https://github.com/Silverfeelin/AHIT-SkinMaker/blob/master/LICENSE.md
 */
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AHITSkinMaker
{
    public partial class AddSkinForm : Form
    {
        
        
        public Skin Result { get; set; }

        Dictionary<SkinColors, Control> colorButtons;

        public AddSkinForm()
        {
            InitializeComponent();

            Bitmap template = Properties.Resources.Template;
            
            colorButtons = new Dictionary<SkinColors, Control>();
            colorButtons[SkinColors.SkinColor_Hat] = PbxSelectHat;
            colorButtons[SkinColors.SkinColor_HatAlt] = PbxSelectHatAlt;
            colorButtons[SkinColors.SkinColor_HatBand] = PbxSelectHatBand;
            colorButtons[SkinColors.SkinColor_Dress] = PbxSelectDress;
            colorButtons[SkinColors.SkinColor_Cape] = PbxSelectCape;
            colorButtons[SkinColors.SkinColor_Pants] = PbxSelectPants;
            colorButtons[SkinColors.SkinColor_Shoes] = PbxSelectShoes;
            colorButtons[SkinColors.SkinColor_ShoesBottom] = PbxSelectShoesBottom;
            colorButtons[SkinColors.SkinColor_Zipper] = PbxSelectZipper;
            colorButtons[SkinColors.SkinColor_Orange] = PbxSelectOrange;
            colorButtons[SkinColors.SkinColor_Hair] = PbxSelectHair;

            CbxIcon.SelectedIndex = 0;
            CbxQuality.SelectedIndex = 0;
            
            Result = new Skin(new Dictionary<SkinColors, Color>());
        }

        public AddSkinForm(string modName = null) : this()
        {
            if (!string.IsNullOrWhiteSpace(modName))
                TbxSkinClassName.Text = string.Format("{0}_Collectible_Skin_{1}", modName, Guid.NewGuid().ToString("N").Substring(0, 4));
        }

        public AddSkinForm(Skin skin) : this()
        {
            Result = skin;
            TbxSkinClassName.Text = skin.ClassName;
            TbxSkinNameText.Text = skin.Text;
            CbxIcon.Text = skin.IconPath;
            CbxQuality.Text = skin.QualityClass;

            UpdatePreview(skin.Colors);

            foreach (var kv in skin.Colors)
            {
                colorButtons[kv.Key].BackColor = kv.Value;
            }
        }

        private void SelectColor_Click(object sender, EventArgs e)
        {
            Control control = (Control)sender;
            ColorDialog.Color = control.BackColor;

            if (ColorDialog.ShowDialog() != DialogResult.OK) return;

            Color color = ColorDialog.Color;
            control.BackColor = color;

            SkinColors skinColor;
            if (Enum.TryParse(control.Tag.ToString(), out skinColor))
            {
                Result.Colors[skinColor] = color;
            }

            UpdatePreview(Result.Colors);
        }

        private void LoadColors(Bitmap b)
        {
            for (int i = 0; i < 11; i++)
            {
                SkinColors sc = (SkinColors)i;
                Point p = TemplateManager.PalettePositions[sc];
                Color c = b.GetPixel(p.X, p.Y);
                colorButtons[sc].BackColor = c;

                if (c != Properties.Resources.Template.GetPixel(p.X, p.Y))
                    Result.Colors[sc] = c;
                else
                    Result.Colors.Remove(sc);
            }

            UpdatePreview(Result.Colors);
        }
        
        private void UpdatePreview(Dictionary<SkinColors, Color> colors)
        {
            PbxPreview.Image = TemplateManager.CreatePreview(colors);
        }

        private void BtnLoadImage_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "Image Files|*.png;*.bmp;*.jpg";

            if (ofd.ShowDialog() != DialogResult.OK) return;

            try
            {
                Bitmap b = new Bitmap(ofd.FileName);
                LoadColors(b);
                b.Dispose();
            }
            catch (Exception exc)
            {
                MessageBox.Show("Could not load image.\n" + exc.Message);
            }
        }

        private void BtnSaveTemplate_Click(object sender, EventArgs e)
        {
            SaveFileDialog sfd = new SaveFileDialog();
            sfd.Filter = "Image|*.png";

            if (sfd.ShowDialog() == DialogResult.OK)
            {
                Properties.Resources.Template.Save(sfd.FileName);
            }
        }

        private void BtnSave_Click(object sender, EventArgs e)
        {
            string error = "",
                className = TbxSkinClassName.Text,
                skinText = TbxSkinNameText.Text,
                quality = CbxQuality.Text,
                iconPath = CbxIcon.Text;

            if (string.IsNullOrWhiteSpace(className))
            {
                error += "- Enter a skin class name, such as 'MyMod_Collectible_Skin_Oreo'\n";
            }
            if (string.IsNullOrWhiteSpace(skinText))
            {
                error += "- Enter the skin name text, such as 'Oreo Skin'.\n";
            }
            if (string.IsNullOrWhiteSpace(quality))
            {
                error += "- Select one of the available qualities.\n";
            }
            if (string.IsNullOrWhiteSpace(iconPath))
            {
                error += "- Enter an icon texture path, or select a default one. The path may not contain spaces.\n";
            }
            if (Result.Colors.Count == 0)
            {
                error += "- Modify at least one color.\n";
            }

            if (error != "")
            {
                error = error.Remove(error.LastIndexOf("\n"));
                MessageBox.Show("Please resolve the following issues:\n" + error, "Error");
                return;
            }

            Result.ClassName = className;
            Result.Text = skinText;
            Result.QualityClass = quality;
            Result.IconPath = iconPath;

            DialogResult = DialogResult.OK;
        }

        private void BtnCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
        }

        private void BtnGenerateClassName_Click(object sender, EventArgs e)
        {
            TbxSkinClassName.Text = string.Format("GeneratedMod_Collectible_Skin_{0}", Guid.NewGuid().ToString("N").Substring(0, 8));
        }
    }
}
