using System;
using System.Globalization;
using Newtonsoft.Json;
using Sceelix.Annotations;
using Sceelix.Mathematics.Data;

namespace Sceelix.Mathematics.Serialization
{
    [StandardJsonConverter(typeof(UnityEngine.Vector2))]
    public class Vector2Converter : JsonConverter
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
            UnityEngine.Vector2 vector = (UnityEngine.Vector2) value;

            writer.WriteValue(vector.x.ToString(CultureInfo.InvariantCulture) + "," + vector.y.ToString(CultureInfo.InvariantCulture));
        }
    }
}