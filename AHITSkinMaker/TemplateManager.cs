using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AHITSkinMaker
{
    public static class TemplateManager
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

        public static Bitmap CreatePreview(Dictionary<SkinColors, Color> colors)
        {
            Bitmap b = new Bitmap(Properties.Resources.Template);
            List<ColorMap> map = new List<ColorMap>();

            for (int i = 0; i < 11; i++)
            {
                SkinColors s = (SkinColors)i;

                if (!colors.ContainsKey(s)) continue;

                Point p = PalettePositions[s];
                Color oldColor = b.GetPixel(p.X, p.Y),
                    newColor = colors[s];

                if (oldColor == newColor)
                    continue;

                ColorMap m = new ColorMap();
                m.OldColor = oldColor;
                m.NewColor = newColor;

                map.Add(m);

            }

            if (map.Count > 0)
            {
                ImageAttributes attr = new ImageAttributes();
                attr.SetRemapTable(map.ToArray(), ColorAdjustType.Bitmap);

                using (Graphics g = Graphics.FromImage(b))
                {
                    Rectangle rect = new Rectangle(0, 0, b.Width, b.Height);
                    g.DrawImage(b, rect, 0, 0, rect.Width, rect.Height, GraphicsUnit.Pixel, attr);
                }
            }

            return b;
        }
    }
}
