using Sceelix.Conversion;
using Sceelix.Mathematics.Data;
using UnityEngine;

namespace Sceelix.Mathematics.Conversions
{
    [ConversionFunctions]
    public class StringConversions
    {
        public static UnityEngine.Color StringToColor(string str)
        {
            var components = str.Split(',');
            return new UnityEngine.Color(ConvertHelper.Convert<byte>(components[0]), ConvertHelper.Convert<byte>(components[1]), ConvertHelper.Convert<byte>(components[2]), ConvertHelper.Convert<byte>(components[3]));
        }



        public static UnityEngine.Vector2 StringToVector2Conversion(string str)
        {
            var components = str.Split(',');
            return new UnityEngine.Vector2(ConvertHelper.Convert<float>(components[0]), ConvertHelper.Convert<float>(components[1]));
        }



        public static UnityEngine.Vector3 StringToVector3Conversion(string str)
        {
            var components = str.Split(',');
            return new UnityEngine.Vector3(ConvertHelper.Convert<float>(components[0]), ConvertHelper.Convert<float>(components[1]), ConvertHelper.Convert<float>(components[2]));
        }
    }
}