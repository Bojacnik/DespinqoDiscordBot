using Newtonsoft.Json;

namespace DespinqoDiscordBot;

public static class ConfigJsonReader
{
    public static async Task<ConfigJsonDto?> ReadConfigJson()
    {
        using var sr = new StreamReader("config.json");
        var json = await sr.ReadToEndAsync();
        var result = JsonConvert.DeserializeObject<ConfigJsonDto>(json);
        return result;
    }
}