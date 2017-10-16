/* Made by Silverfeelin
 * Licensed under a MIT license: https://github.com/Silverfeelin/AHIT-SkinMaker/blob/master/LICENSE.md
 */
using System;
using System.Collections.Generic;
using System.Drawing;

namespace AHITSkinMaker
{
    public class Skin
    {
        public string ClassName { get; set; }
        public string TextLocalizationKey
        {
            get
            {
                return ClassName + "Name";
            }
        }
        public string Text { get; set; }
        public string QualityClass { get; set; }
        public string IconPath { get; set; }
        public Dictionary<SkinColors, Color> Colors { get; set; }

        public Skin(Dictionary<SkinColors, Color> colors)
        {
            Colors = colors;
        }

        public string ToClass()
        {
            string skin = Properties.Resources.Skin;

            string colors = "";
            foreach (KeyValuePair<SkinColors, Color> item in Colors)
            {
                Color c = item.Value;
                colors += string.Format("  SkinColor[{0}] = (R={1}, G={2}, B={3})", item.Key, c.R, c.G, c.B) + Environment.NewLine;
            }
            
            skin = skin.Replace("{CLASS}", ClassName);
            skin = skin.Replace("{ICON}", IconPath);
            skin = skin.Replace("{LOCALIZATIONKEY}", TextLocalizationKey);
            skin = skin.Replace("{QUALITY}", QualityClass);
            skin = skin.Replace("{COLORS}", colors);

            return skin;
        }

        public override string ToString()
        {
            return Text;
        }
    }
}
