using System.Collections.Generic;
using Sceelix.Collections;
using Sceelix.Conversion;
using Sceelix.Mathematics.Data;

namespace Sceelix.Mathematics.Conversions
{
    [ConversionFunctions]
    public class Vector3Conversions
    {
        public static SceeList ToSceeList(UnityEngine.Vector3 vector3D)
        {
            return new SceeList(new KeyValuePair<string, object>("X", vector3D.x), new KeyValuePair<string, object>("Y", vector3D.y), new KeyValuePair<string, object>("Z", vector3D.z));
        }



        public static string ToString(UnityEngine.Vector3 vector3D)
        {
            return string.Format("{0},{1},{2}", ConvertHelper.Convert<string>(vector3D.x), ConvertHelper.Convert<string>(vector3D.y), ConvertHelper.Convert<string>(vector3D.z));
        }
    }
}