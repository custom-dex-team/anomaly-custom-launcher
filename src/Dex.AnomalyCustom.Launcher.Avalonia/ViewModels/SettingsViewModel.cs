using Dex.AnomalyCustom.Launcher.Avalonia.Models;

using ReactiveUI;

using System.Collections.ObjectModel;
using System.Windows.Input;

namespace Dex.AnomalyCustom.Launcher.Avalonia.ViewModels;

public class SettingsViewModel : ReactiveObject
{
    private bool _isDeleteShaderCache;

    public bool IsDeleteShaderCache
    {
        get => _isDeleteShaderCache;
        set => this.RaiseAndSetIfChanged(ref _isDeleteShaderCache, value);
    }

    public ObservableCollection<string> ShadowMapValues { get; }

    public ICommand DeleteShaderCacheCommand { get; }

    public SettingsViewModel()
    {
        ShadowMapValues = new(TechnicalSettings.ShadowMapValues);

        DeleteShaderCacheCommand = ReactiveCommand.Create(DeleteShaderCache);
    }

    private void DeleteShaderCache()
    {
    }
}
