using Dex.AnomalyCustom.Launcher.Avalonia.ViewModels;
using Dex.AnomalyCustom.Launcher.Avalonia.Views;

using Microsoft.Extensions.DependencyInjection;

namespace Dex.AnomalyCustom.Launcher.Avalonia.Extensions;

internal static class ServiceCollectionExtensions
{
    public static IServiceCollection AddViews(this IServiceCollection serviceCollection)
    {
        serviceCollection.AddSingleton<MainView>();

        serviceCollection.AddTransient<SettingsView>();

        return serviceCollection;
    }

    public static IServiceCollection AddViewModels(this IServiceCollection serviceCollection)
    {
        serviceCollection.AddSingleton<MainViewModel>();

        serviceCollection.AddTransient<SettingsViewModel>();

        return serviceCollection;
    }
}
