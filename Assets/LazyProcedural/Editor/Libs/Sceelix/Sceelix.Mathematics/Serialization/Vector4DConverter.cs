using System;
using System.Globalization;
using Newtonsoft.Json;
using Sceelix.Annotations;
using Sceelix.Mathematics.Data;
using UnityEngine;

namespace Sceelix.Mathematics.Serialization
{
    [StandardJsonConverter(typeof(UnityEngine.Vector4))]
    public class Vector4Converter : JsonConverter
    {
        public override bool CanConvert(Type objectType)
        {
            throw new NotImplementedException();
        }



        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            throw new NotImplementedException();
        }



        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            UnityEngine.Vector4 vector = (UnityEngine.Vector4) value;

            writer.WriteValue(vector.x.ToString(CultureInfo.InvariantCulture) + "," + vector.y.ToString(CultureInfo.InvariantCulture) + "," + vector.z.ToString(CultureInfo.InvariantCulture) + "," + vector.w.ToString(CultureInfo.InvariantCulture));
        }
    }
}