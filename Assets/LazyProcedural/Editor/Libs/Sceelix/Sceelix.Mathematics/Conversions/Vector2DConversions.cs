using System.Collections.Generic;
using Sceelix.Collections;
using Sceelix.Conversion;
using Sceelix.Mathematics.Data;

namespace Sceelix.Mathematics.Conversions
{
    [ConversionFunctions]
    public class Vector2Conversions
    {
        public static SceeList ToSceeList(UnityEngine.Vector2 vector2D)
        {
            return new SceeList(new KeyValuePair<string, object>("X", vector2D.x), new KeyValuePair<string, object>("Y", vector2D.y));
        }



        public static string ToString(UnityEngine.Vector2 vector2D)
        {
            return string.Format("{0},{1}", ConvertHelper.Convert<string>(vector2D.x), ConvertHelper.Convert<string>(vector2D.y));
        }
    }
}