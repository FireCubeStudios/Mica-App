using Windows.UI;

namespace REDACTED.Helpers
{
    class ColorHelper
    {
        private static
        (
             Color SystemAccentColorLight3,
             Color SystemAccentColorLight2,
             Color SystemAccentColorLight1,
             Color SystemAccentColor,
             Color SystemAccentColorDark1,
             Color SystemAccentColorDark2,
             Color SystemAccentColorDark3
        )
        GetColorPalette(Color c)
        {
            return
            (
                ChangeColorBrightness(c, 0.51f),
                ChangeColorBrightness(c, 0.25f),
                ChangeColorBrightness(c, 0.02f),
                c,
                ChangeColorBrightness(c, -0.19f),
                ChangeColorBrightness(c, -0.40f),
                ChangeColorBrightness(c, -0.68f)
            );
        }
        public static Color ChangeColorBrightness(Color color, double correctionFactor)
        {
            double red = color.R;
            double green = color.G;
            double blue = color.B;

            if (correctionFactor < 0)
            {
                correctionFactor = 1 + correctionFactor;
                red *= correctionFactor;
                green *= correctionFactor;
                blue *= correctionFactor;
            }
            else
            {
                red = (255 - red) * correctionFactor + red;
                green = (255 - green) * correctionFactor + green;
                blue = (255 - blue) * correctionFactor + blue;
            }

            return Color.FromArgb(color.A, (byte)red, (byte)green, (byte)blue);
        }
    }
}
