using System.Text.Json;

namespace MTGtrade.API.Helpers
{
    public static class MtgJsonExtractor
    {
        public static async Task ExtractSetFromAllPrintings(string allPrintingsPath, string outputPath, string setCode = "LEA")
        {
            using FileStream fs = new FileStream(allPrintingsPath, FileMode.Open, FileAccess.Read);
            using JsonDocument doc = await JsonDocument.ParseAsync(fs);

            JsonElement data = doc.RootElement.GetProperty("data");
            if (!data.TryGetProperty(setCode, out JsonElement set))
            {
                return;
            }

            var options = new JsonSerializerOptions { WriteIndented = true };
            string json = JsonSerializer.Serialize(set, options);

            await File.WriteAllTextAsync(outputPath, json);
        }
    }
}