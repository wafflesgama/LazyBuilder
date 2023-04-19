using System.Collections.Generic;
using Sceelix.Collections;
using Sceelix.Conversion;
using Sceelix.Mathematics.Data;

namespace Sceelix.Mathematics.Conversions
{
    [ConversionFunctions]
    public class ColorConversions
    {
        public static SceeList ColorToSceeList(UnityEngine.Color color)
        {
            return new SceeList(new KeyValuePair<string, object>("Red", color.r), new KeyValuePair<string, object>("Green", color.g), new KeyValuePair<string, object>("Blue", color.b), new KeyValuePair<string, object>("Alpha", color.A));
        }



        public static string ToString(UnityEngine.Color color)
        {
            return string.Format("{0},{1},{2},{3}", ConvertHelper.Convert<string>(color.r), ConvertHelper.Convert<string>(color.g), ConvertHelper.Convert<string>(color.b), ConvertHelper.Convert<string>(color.A));
        }
    }
}