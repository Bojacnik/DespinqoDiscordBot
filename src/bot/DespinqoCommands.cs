using DespinqoDiscordBot.injection;
using DSharpPlus.CommandsNext;
using DSharpPlus.CommandsNext.Attributes;
using DSharpPlus.Entities;

namespace DespinqoDiscordBot;

public class DespinqoCommands : BaseCommandModule
{
    private readonly ImageManager _imageManager = (ImageManager)(InjectionContainer.Provider?.GetService(typeof(ImageManager)) ?? throw new Exception("Image manager service wasn't found"));
    private readonly Random _rdm = new(Environment.TickCount);

    private (int, (string?, Stream?)) _returnRandomImage(string directory)
    {
        int count;
        if ((count = _imageManager.GetCount(directory)) == 0)
            return (0, (null, null));
            
        var randomImageIndex = _rdm.Next(0, count);
        var stream = _imageManager.Get(directory, randomImageIndex);
        return (randomImageIndex, stream);
    }

    [Command("despina")]
    public async Task DespinaCommand(CommandContext commandContext)
    {
        var selectedImage = _returnRandomImage("images\\despina");
        if (selectedImage.Item2.Item1 == null || selectedImage.Item2.Item2 == null)
            return;

        await commandContext.Channel.SendMessageAsync(
            new DiscordMessageBuilder().AddFile(
                "despina" + selectedImage.Item2.Item1,
                selectedImage.Item2.Item2,
                true
                )
            );
    }

    [Command("napoleon")]
    public async Task NapoleonCommand(CommandContext commandContext)
    {
        var selectedImage = _returnRandomImage("images\\napoleon");
        if (selectedImage.Item2.Item1 == null || selectedImage.Item2.Item2 == null)
            return;
        
        await commandContext.Channel.SendMessageAsync(
            new DiscordMessageBuilder().AddFile(
                "napoleon" + selectedImage.Item2.Item1,
                selectedImage.Item2.Item2,
                true
                )
        );
    }
    
    [Command("help")]
    public async Task HelpCommand(CommandContext commandContext)
    {
        
        await commandContext.Channel.SendMessageAsync(">>> !despina - sends an image of Despina!\n!napoleon - randomly picks either yes or no");
    }
}