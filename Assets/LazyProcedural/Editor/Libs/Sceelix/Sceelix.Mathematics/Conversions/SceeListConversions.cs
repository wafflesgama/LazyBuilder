using System;
using Sceelix.Collections;
using Sceelix.Conversion;
using Sceelix.Mathematics.Data;

namespace Sceelix.Mathematics.Conversions
{
    [ConversionFunctions]
    public class SceeListConversions
    {
        public static BoxScope SceelistToBoxScope(SceeList sceelist)
        {
            if (sceelist.IsAssociative)
                return new BoxScope(
                    ConvertHelper.Convert<UnityEngine.Vector3>(sceelist["XAxis"]),
                    ConvertHelper.Convert<UnityEngine.Vector3>(sceelist["YAxis"]),
                    ConvertHelper.Convert<UnityEngine.Vector3>(sceelist["ZAxis"]),
                    ConvertHelper.Convert<UnityEngine.Vector3>(sceelist["Translation"]),
                    ConvertHelper.Convert<UnityEngine.Vector3>(sceelist["Sizes"]));
            return new BoxScope(ConvertHelper.Convert<UnityEngine.Vector3>(sceelist[0]),
                ConvertHelper.Convert<UnityEngine.Vector3>(sceelist[1]),
                ConvertHelper.Convert<UnityEngine.Vector3>(sceelist[2]),
                ConvertHelper.Convert<UnityEngine.Vector3>(sceelist[3]),
                ConvertHelper.Convert<UnityEngine.Vector3>(sceelist[4]));
        }



        /// <summary>
        /// Converts a List to a struct of type UnityEngine.Color.
        /// </summary>
        /// <param name="sceelist"></param>
        /// <returns></returns>
        public static UnityEngine.Color SceelistToColorConversion(SceeList sceelist)
        {
            if (sceelist.IsAssociative)
            {
                var red = sceelist["Red"] ?? 255;
                var green = sceelist["Green"] ?? 255;
                var blue = sceelist["Blue"] ?? 255;
                var alpha = sceelist["Alpha"] ?? 255;

                return new UnityEngine.Color(ConvertHelper.Convert<byte>(red), ConvertHelper.Convert<byte>(green), ConvertHelper.Convert<byte>(blue), ConvertHelper.Convert<byte>(alpha));
            }

            if (sceelist.Count >= 3)
                return new UnityEngine.Color(ConvertHelper.Convert<byte>(sceelist[0]), ConvertHelper.Convert<byte>(sceelist[1]), ConvertHelper.Convert<byte>(sceelist[2]));
            if (sceelist.Count >= 4) return new UnityEngine.Color(ConvertHelper.Convert<byte>(sceelist[0]), ConvertHelper.Convert<byte>(sceelist[1]), ConvertHelper.Convert<byte>(sceelist[2]), ConvertHelper.Convert<byte>(sceelist[3]));

            throw new Exception("Could not convert 'Sceelist' to 'UnityEngine.Color'.");
        }



        public static UnityEngine.Vector2 SceelistToVector2Conversion(SceeList sceelist)
        {
            if (sceelist.IsAssociative)
                return new UnityEngine.Vector2(
                    ConvertHelper.Convert<float>(sceelist["X"]),
                    ConvertHelper.Convert<float>(sceelist["Y"]));

            return new UnityEngine.Vector2(
                ConvertHelper.Convert<float>(sceelist[0]),
                ConvertHelper.Convert<float>(sceelist[1]));
        }



        public static UnityEngine.Vector3 SceelistToVectorConversion(SceeList sceelist)
        {
            if (sceelist.IsAssociative)
                return new UnityEngine.Vector3(
                    ConvertHelper.Convert<float>(sceelist["X"]),
                    ConvertHelper.Convert<float>(sceelist["Y"]),
                    ConvertHelper.Convert<float>(sceelist["Z"]));

            return new UnityEngine.Vector3(
                ConvertHelper.Convert<float>(sceelist[0]),
                ConvertHelper.Convert<float>(sceelist[1]),
                ConvertHelper.Convert<float>(sceelist[2]));
        }
    }
}