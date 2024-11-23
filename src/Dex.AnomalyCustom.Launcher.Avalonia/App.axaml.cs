using Avalonia;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Markup.Xaml;

using Dex.AnomalyCustom.Launcher.Avalonia.Extensions;
using Dex.AnomalyCustom.Launcher.Avalonia.ViewModels;
using Dex.AnomalyCustom.Launcher.Avalonia.Views;

using Microsoft.Extensions.DependencyInjection;

using System;

namespace Dex.AnomalyCustom.Launcher.Avalonia;

public partial class App : Application
{
    private IServiceProvider _serviceProvider = null!;

    public override void Initialize()
    {
        AvaloniaXamlLoader.Load(this);
    }

    public override void OnFrameworkInitializationCompleted()
    {
        if (ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
        {
            _serviceProvider = new ServiceCollection()
                .AddSingleton<MainWindow>()
                .AddViews()
                .AddViewModels()
                .BuildServiceProvider();

            var mainViewModel = _serviceProvider.GetRequiredService<MainViewModel>();
            var mainWindow = _serviceProvider.GetRequiredService<MainWindow>();
            mainWindow.DataContext = mainViewModel;

            desktop.MainWindow = mainWindow;
        }
        else if (ApplicationLifetime is ISingleViewApplicationLifetime singleViewPlatform)
        {
            _serviceProvider = new ServiceCollection()
                .AddViews()
                .AddViewModels()
                .BuildServiceProvider();

            var mainViewModel = _serviceProvider.GetRequiredService<MainViewModel>();
            var mainView = _serviceProvider.GetRequiredService<MainView>();
            mainView.DataContext = mainViewModel;

            singleViewPlatform.MainView = mainView;
        }

        base.OnFrameworkInitializationCompleted();
    }
}
