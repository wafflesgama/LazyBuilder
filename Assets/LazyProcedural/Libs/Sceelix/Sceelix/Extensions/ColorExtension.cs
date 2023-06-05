using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Sceelix.Extensions
{
    public static class ColorExtension
    {

        /// <summary>
        /// Converts the color to an array of 0-1 float values.
        /// </summary>
        /// <returns></returns>
        public static float[] ToFloatArray(this Color color)
        {
            return new[] { color.r, color.g, color.b, color.a };
        }

        public static Color Parse(string value)
        {
            var stringValue = value;
            stringValue = stringValue.Replace("RGBA", "");
            stringValue = stringValue.Replace("(", "");
            stringValue = stringValue.Replace(")", "");

            var splittedValues = stringValue.Split(',');
            return ColorFromArray(splittedValues.Select(x => float.Parse(x)).ToArray());
        }

        public static Color ColorFromArray(float[] array)
        {
            if (array == null) return Color.white;

            if (array.Length == 3)
                return new Color(array[0], array[1], array[2]);


            return new Color(array[0], array[1], array[2], array[3]);
        }
    }
}
