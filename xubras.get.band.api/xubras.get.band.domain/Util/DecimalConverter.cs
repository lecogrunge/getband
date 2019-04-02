namespace xubras.get.band.domain.Util
{
    using Newtonsoft.Json;
    using Newtonsoft.Json.Linq;
    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using System.Text;

    public class DecimalConverter : JsonConverter
    {
        public override bool CanConvert(Type objectType)
        {
            return (objectType == typeof(decimal) || objectType == typeof(decimal?));
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            JToken token = JToken.Load(reader);
            if (token.Type == JTokenType.Float || token.Type == JTokenType.Integer)
            {
                return token.ToObject<decimal>();
            }
            if (token.Type == JTokenType.String && token.ToString().Contains(","))
            {
                return Decimal.Parse(token.ToString(), CultureInfo.GetCultureInfo("pt-BR"));
            }
            else if (token.Type == JTokenType.String && token.ToString().Contains(".") || !token.ToString().Equals("0"))
            {
                return Convert.ToDecimal(token.ToString(), CultureInfo.GetCultureInfo("en-US"));
            }
            else if (token.Type == JTokenType.String && token.ToString().Equals("0"))
            {
                return default(decimal);
            }
            if (token.Type == JTokenType.Null && objectType == typeof(decimal?))
            {
                return null;
            }
            throw new JsonSerializationException("Unexpected token type: " +
                                                  token.Type.ToString());
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            decimal decimalValue = (decimal)value;
            var decimalStringValue = decimalValue != 0m ? decimalValue.ToString("#.##").Replace(".", ",") : "0,0";
            writer.WriteValue(decimalStringValue);
        }
    }
}
