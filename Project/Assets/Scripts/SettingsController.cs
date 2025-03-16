using System;

public class SettingsController : IDisposable {
    private UIInputHandler _handler;
    private SettingsView _view;
    private ISettingsApplier _applier;
    private SettingsSaver _saver;
    private SettingsLoader _loader;

    public SettingsController(UIInputHandler handler, SettingsView view, ISettingsApplier applier, SettingsSaver saver, SettingsLoader loader) {
        _handler = handler;
        _view = view;
        _applier = applier;
        _saver = saver;
        _loader = loader;
        _handler.ConfirmSettings += OnConfirm;
        _handler.CancelSettings += OnCancel;
    }

    public void Dispose()
    {
        _handler.ConfirmSettings -= OnConfirm;
        _handler.CancelSettings += OnCancel;
    }

    public void ConfigurateSettings() {
        var settings = new Settings();
        if(_loader.TryLoad(out Settings loadedSetting)) {
            settings = loadedSetting;
        } else {
            settings = _view.GetSettings();
        }
        _view.SetSettings(settings);
        _view.ConfirmSettings();
        _applier.Apply(settings);
    }

    private void OnConfirm() {
        var settings = _view.GetSettings();
        _view.ConfirmSettings();
        _applier.Apply(settings);
        _saver.Save(settings);
    }

    private void OnCancel() {
        _view.ResetSettings();
    }
}