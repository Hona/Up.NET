using System;
using System.Buffers;
using System.Buffers.Text;
using System.Net;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Up.NET.Converters
{
    public class StringToHttpStatusCodeConverter : JsonConverter<HttpStatusCode>
    {
        public override HttpStatusCode Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            if (reader.TokenType != JsonTokenType.String) throw new InvalidOperationException("Error object expected HTTP status code property to be a string");
            
            var span = reader.HasValueSequence ? reader.ValueSequence.ToArray() : reader.ValueSpan;
            if (Utf8Parser.TryParse(span, out int number, out var bytesConsumed) && span.Length == bytesConsumed)
            {
                return (HttpStatusCode)number;
            }

            if (int.TryParse(reader.GetString(), out number))
            {
                return (HttpStatusCode)number;
            }

            throw new InvalidOperationException("Error object expected HTTP status code property to be a string");
        }

        public override void Write(Utf8JsonWriter writer, HttpStatusCode value, JsonSerializerOptions options)
        {
            writer.WriteStringValue(((int)value).ToString());
        }
    }
}