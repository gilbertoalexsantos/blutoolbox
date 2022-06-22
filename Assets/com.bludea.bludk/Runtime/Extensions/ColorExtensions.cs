using UnityEngine;

namespace Bludk
{
    public static class ColorExtensions
    {
        public static Color WithAlpha01(this Color c, float alpha)
        {
            return new Color(c.r, c.g, c.b, alpha);
        }
    }
}