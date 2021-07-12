using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace InterviewSchedulingSystem.Helpers
{
    public static class JsonCalendarsCreate
    {
        public static JsonObjectPostCreate Deserialize(string json)
        {
            JsonSerializerOptions ff = new JsonSerializerOptions();
            ff.Converters.Add(new DateTimeConverter());
            JsonObjectPostCreate jsonObjectPostCreate = 
                JsonSerializer.Deserialize<JsonObjectPostCreate>(json, ff);
            return jsonObjectPostCreate;
        }

        public static JsonObjectPostCreateTempl DeserializeTempl(string json)
        {
            JsonSerializerOptions ff = new JsonSerializerOptions();
            ff.Converters.Add(new DateTimeConverter());
            JsonObjectPostCreateTempl jsonObjectPostCreate =
                JsonSerializer.Deserialize<JsonObjectPostCreateTempl>(json, ff);
            return jsonObjectPostCreate;
        }

    }
    public class JsonObjectPostCreate
    {
        public string Name { get; set; }

        public Dictionary<DateTime, List<DateTime>> DaysScheduleDict { get; set; }
        public List<string> Vacancies { get; set; }
    }

    public class JsonObjectPostCreateTempl
    {
        public string Name { get; set; }

        public Dictionary<DateTime, List<DateTime>> DaysScheduleDict { get; set; }
    }

    public class DateTimeConverter : JsonConverter<DateTime>
    {
        public override DateTime Read(ref Utf8JsonReader reader, 
            Type typeToConvert, JsonSerializerOptions options)
        {
            Debug.Assert(typeToConvert == typeof(DateTime));
            return DateTime.Parse(reader.GetString());
        }

        public override void Write(Utf8JsonWriter writer, DateTime value, JsonSerializerOptions options)
        {
            writer.WriteStringValue(value.ToUniversalTime().ToString("yyyy'-'MM'-'dd'T'HH':'mm':'ssZ"));
        }
    }
}
