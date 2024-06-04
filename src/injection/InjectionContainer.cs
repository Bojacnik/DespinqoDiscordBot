using Microsoft.Extensions.DependencyInjection;

namespace DespinqoDiscordBot.injection;

public static class InjectionContainer
{
    public static ServiceProvider? Provider { get; private set; }

    public static void Load()
    {
        var serviceCollection = new ServiceCollection();
        serviceCollection.AddSingleton<ImageManager>(new ImageManagerImpl("images"));
        Provider = serviceCollection.BuildServiceProvider();
    }
}