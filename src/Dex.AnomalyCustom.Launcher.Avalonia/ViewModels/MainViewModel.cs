using ReactiveUI;

using System.Windows.Input;

namespace Dex.AnomalyCustom.Launcher.Avalonia.ViewModels;

public class MainViewModel : ReactiveObject
{
    public ICommand OpenVKCommand { get; }

    public ICommand PlayCustomCommand { get; }

    public MainViewModel()
    {
        OpenVKCommand = ReactiveCommand.Create(OpenVK);

        PlayCustomCommand = ReactiveCommand.Create(PlayCustom);
    }

    private void OpenVK()
    {
    }

    private void PlayCustom()
    {
    }
}
