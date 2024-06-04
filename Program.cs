using DespinqoDiscordBot.injection;
using DSharpPlus;
using DSharpPlus.CommandsNext;

namespace DespinqoDiscordBot;

internal static class Program
{
    private static DiscordClient? Client { get; set; }
    private static CommandsNextExtension? Commands { get; set; }

    private static async Task Main()
    {
        InjectionContainer.Load();
        
        var result = await ConfigJsonReader.ReadConfigJson();

        var discordConfiguration = new DiscordConfiguration()
        {
            Intents = DiscordIntents.All,
            Token = result?.Token ?? throw new Exception("Received token was NULL!"),
            TokenType = TokenType.Bot,
            AutoReconnect = true,
        };

        Client = new DiscordClient(discordConfiguration);

        var commandConfig = new CommandsNextConfiguration()
        {
            StringPrefixes = new[] { result.Prefix ?? throw new Exception("Received command prefix was NULL!") },
            EnableMentionPrefix = true,
            IgnoreExtraArguments = true,
            EnableDefaultHelp = false,
        };

        Commands = Client.UseCommandsNext(commandConfig);
        Commands.RegisterCommands<DespinqoCommands>();
        await Client.ConnectAsync();
        await Task.Delay(-1);
    }
}




