using System;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace MTGtrade.API.Helpers;

public class ParseIntFromStringConverter : JsonConverter<int?>
{
    public override int? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        if (reader.TokenType == JsonTokenType.Number && reader.TryGetInt32(out var number))
            return number;

        if (reader.TokenType == JsonTokenType.String && int.TryParse(reader.GetString(), out var strVal))
            return strVal;

        return null;
    }

    public override void Write(Utf8JsonWriter writer, int? value, JsonSerializerOptions options)
    {
        if (value.HasValue)
            writer.WriteNumberValue(value.Value);
        else
            writer.WriteNullValue();
    }
}