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
        public static Dictionary<SkinColors, Point> PalettePositions { get; } = new Dictionary<SkinColors, Point>()
        {
            { SkinColors.SkinColor_Hat, new Point(8, 150) },
            { SkinColors.SkinColor_HatAlt, new Point(8, 166) },
            { SkinColors.SkinColor_HatBand, new Point(8, 182) },
            { SkinColors.SkinColor_Dress, new Point(8, 198) },
            { SkinColors.SkinColor_Cape, new Point(8, 214) },
            { SkinColors.SkinColor_Pants, new Point(8, 230) },
            { SkinColors.SkinColor_Shoes, new Point(8, 246) },
            { SkinColors.SkinColor_ShoesBottom, new Point(8, 262) },
            { SkinColors.SkinColor_Zipper, new Point(8, 278) },
            { SkinColors.SkinColor_Orange, new Point(8, 294) },
            { SkinColors.SkinColor_Hair, new Point(8, 310) }
        };

        public Dictionary<SkinColors, Color> Colors { get; set; }
        public Skin Result { get; set; }

        Dictionary<SkinColors, Control> colorButtons;

        public AddSkinForm()
        {
            InitializeComponent();

            Bitmap template = Properties.Resources.Template;

            Colors = new Dictionary<SkinColors, Color>();

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
        }

        public AddSkinForm(string modName = null) : this()
        {
            if (!string.IsNullOrWhiteSpace(modName))
                TbxSkinClassName.Text = string.Format("{0}_Collectible_Skin_{1}", modName, Guid.NewGuid().ToString("N").Substring(0, 4));
        }

        private void SelectColor_Click(object sender, EventArgs e)
        {
            if (ColorDialog.ShowDialog() != DialogResult.OK) return;

            Color color = ColorDialog.Color;

            Control control = (Control)sender;
            control.BackColor = ColorDialog.Color;

            SkinColors skinColor;
            if (Enum.TryParse(control.Tag.ToString(), out skinColor))
            {
                Colors[skinColor] = color;
            }

            UpdatePreview();
        }

        private void LoadColors(Bitmap b)
        {
            for (int i = 0; i < 11; i++)
            {
                SkinColors sc = (SkinColors)i;
                Point p = PalettePositions[sc];
                Color c = b.GetPixel(p.X, p.Y);
                colorButtons[sc].BackColor = c;

                if (c != Properties.Resources.Template.GetPixel(p.X, p.Y))
                    Colors[sc] = c;
                else
                    Colors.Remove(sc);
            }

            UpdatePreview();
        }

        private void UpdatePreview()
        {
            Bitmap b = new Bitmap(Properties.Resources.Template);
            List<ColorMap> map = new List<ColorMap>();

            for (int i = 0; i < 11; i++)
            {
                SkinColors s = (SkinColors)i;

                if (!Colors.ContainsKey(s)) continue;

                Point p = PalettePositions[s];
                Color oldColor = b.GetPixel(p.X, p.Y),
                    newColor = Colors[s];

                if (oldColor == newColor)
                    continue;

                ColorMap m = new ColorMap();
                m.OldColor = oldColor;
                m.NewColor = newColor;

                map.Add(m);

            }
            ImageAttributes attr = new ImageAttributes();
            attr.SetRemapTable(map.ToArray(), ColorAdjustType.Bitmap);

            using (Graphics g = Graphics.FromImage(b))
            {
                Rectangle rect = new Rectangle(0, 0, b.Width, b.Height);
                g.DrawImage(b, rect, 0, 0, rect.Width, rect.Height, GraphicsUnit.Pixel, attr);
            }

            PbxPreview.Image = b;
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
            catch
            {
                MessageBox.Show("Could not load image.");
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
            if (Colors.Count == 0)
            {
                error += "- Modify at least one color.\n";
            }

            if (error != "")
            {
                error = error.Remove(error.LastIndexOf("\n"));
                MessageBox.Show("Please resolve the following issues:\n" + error, "Error");
                return;
            }

            Result = new Skin(Colors)
            {
                ClassName = className,
                Text = skinText,
                QualityClass = quality,
                IconPath = iconPath
            };

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
