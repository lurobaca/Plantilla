using System;
using System.Globalization;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Linq;

namespace OneSignal.RestAPIv3.Client.Resources.Notifications
{
    #region CustomDateTimeConverter
    /// <summary>
    /// Custom DateTime converter used to format date and time in order to comply with API requirement.
    /// </summary>
    public class CustomDateTimeConverter : IsoDateTimeConverter
    {
        /// <summary>
        /// Default constructor.
        /// </summary>
        public CustomDateTimeConverter()
        {
            base.DateTimeFormat = "yyyy-MM-dd HH:mm:ss \"GMT\"zzz";
        }
    }
    #endregion

    #region UnixDateTimeJsonConverter
    /// <summary>
    /// Converter used to serialize UnixDateTimeEnum as string.
    /// </summary>
    public class UnixDateTimeJsonConverter : StringEnumConverter
    {
        /// <summary>
        /// Deserializes object
        /// </summary>
        /// <param name="reader"></param>
        /// <param name="objectType"></param>
        /// <param name="existingValue"></param>
        /// <param name="serializer"></param>
        /// <returns></returns>
        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            bool isNullable = (Nullable.GetUnderlyingType(objectType) != null);
            var enumType = (Nullable.GetUnderlyingType(objectType) ?? objectType);

            if (reader.TokenType == JsonToken.Null)
            {
                if (!isNullable)
                    throw new JsonSerializationException();
                return null;
            }

            var token = JToken.Load(reader);
            if (token.Type == JTokenType.String)
            {
                token = (JValue)string.Join(", ", token.ToString().Split(',').Select(s => s.Trim()).Select(s =>
                {
                    var unixTime = double.Parse(s);
                    var dateTime = UnixTimeStampToDateTime(unixTime);

                    return dateTime.ToString("s");
                }).ToArray());
            }

            using (var subReader = token.CreateReader())
            {
                while (subReader.TokenType == JsonToken.None)
                    subReader.Read();
                return base.ReadJson(subReader, objectType, existingValue, serializer); // Use base class to convert
            }
        }

        /// <summary>
        /// Serializes object
        /// </summary>
        /// <param name="writer"></param>
        /// <param name="value"></param>
        /// <param name="serializer"></param>
        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            var array = new JArray();
            using (var tempWriter = array.CreateWriter())
                base.WriteJson(tempWriter, value, serializer);
            var token = array.Single();

            if (token.Type == JTokenType.String && value != null)
            {
                var enumType = value.GetType();
                token = string.Join(", ", token.ToString().Split(',').Select(s => s.Trim()).Select(s =>
                {
                    var dateTime = DateTime.Parse(s);
                    var unixTime = DateTimeToUnixTimeStamp(dateTime);

                    return unixTime.ToString(CultureInfo.InvariantCulture);
                }).ToArray());
            }

            token.WriteTo(writer);
        }

        /// <summary>
        /// Defines if converter can be used for deserialization.
        /// </summary>
        public override bool CanRead
        {
            get
            {
                return true;
            }
        }

        /// <summary>
        /// Defines if converter can be used for serialization.
        /// </summary>
        /// <param name="objectType"></param>
        /// <returns></returns>
        public override bool CanConvert(Type objectType)
        {
            return objectType == typeof(DateTime);
        }

        private DateTime UnixTimeStampToDateTime(double unixTimeStamp)
        {
            // Unix timestamp is seconds past epoch
            System.DateTime dtDateTime = new DateTime(1970, 1, 1, 0, 0, 0, 0, System.DateTimeKind.Utc);
            dtDateTime = dtDateTime.AddSeconds(unixTimeStamp);
            return dtDateTime;
        }

        private Double DateTimeToUnixTimeStamp(DateTime dateTime)
        {
            // Unix timestamp is seconds past epoch
            var dtDateTime = new DateTime(1970, 1, 1, 0, 0, 0, 0, System.DateTimeKind.Utc);
            var elapsed = dateTime.Subtract(dtDateTime).TotalSeconds;

            return elapsed;
        }
    }
    #endregion
}
