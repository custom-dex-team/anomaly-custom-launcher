using System;

using Avalonia;
using Avalonia.ReactiveUI;

namespace Dex.AnomalyCustom.Launcher.Avalonia.Windows;

class Program
{
    [STAThread]
    public static void Main(string[] args) =>
        BuildAvaloniaApp()
            .StartWithClassicDesktopLifetime(args);

    public static AppBuilder BuildAvaloniaApp() =>
        AppBuilder.Configure<App>()
            .UsePlatformDetect()
            .WithInterFont()
            .LogToTrace()
            .UseReactiveUI()
            .UseWin32();
}
