using Sceelix.Collections;
using Sceelix.Conversion;
using Sceelix.Core.Annotations;
using UnityEngine;
using KVPair = System.Collections.Generic.KeyValuePair<string, object>;

namespace Sceelix.Mathematics.Graphs.Functions
{
    [ExpressionFunctions("Color")]
    public class ColorFunctions
    {
        public static object Color(dynamic red, dynamic green, dynamic blue)
        {
            return new Color(ConvertHelper.Convert<byte>(red), ConvertHelper.Convert<byte>(green), ConvertHelper.Convert<byte>(blue));
        }



        public static object Color(dynamic red, dynamic green, dynamic blue, dynamic alpha)
        {
            return new Color(ConvertHelper.Convert<byte>(red), ConvertHelper.Convert<byte>(green), ConvertHelper.Convert<byte>(blue), ConvertHelper.Convert<byte>(alpha));
        }



        /*public static Object Rgba(dynamic red, dynamic green, dynamic blue)
        {
            return new SceeList(new KVPair("Red", red), new KVPair("Green", green), new KVPair("Blue", blue), new KVPair("Alpha", (byte)255));
        }


        public static Object Rgba(dynamic red, dynamic green, dynamic blue, dynamic alpha)
        {
            return new SceeList(new KVPair("Red", red), new KVPair("Green", green), new KVPair("Blue", blue), new KVPair("Alpha", alpha));
        }*/



        public static object Hsva(dynamic hue, dynamic saturation, dynamic value)
        {
            Color color = UnityEngine.Color.HSVToRGB(hue, saturation, value);

            return new SceeList(new KVPair("Red", color.r), new KVPair("Green", color.g), new KVPair("Blue", color.b), new KVPair("Alpha", (byte) 255));
        }



        public static object Hsva(dynamic hue, dynamic saturation, dynamic value, dynamic alpha)
        {
            Color color = UnityEngine.Color.HSVToRGB(hue, saturation, value);

            return new SceeList(new KVPair("Red", color.r), new KVPair("Green", color.g), new KVPair("Blue", color.b), new KVPair("Alpha", alpha));
        }
    }
}