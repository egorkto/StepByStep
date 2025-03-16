using System;

public class RulesShower : IDisposable {
    private InputActionsHandler _inputHandler;
    private UIWindowsPresenter _windowsPresenter;

    public RulesShower(InputActionsHandler inputHandler, UIWindowsPresenter windowsPresenter) {
        _inputHandler = inputHandler;
        _windowsPresenter = windowsPresenter;
        _inputHandler.OpenRules += OnOpenRules;
        _inputHandler.CloseRules += OnCloseRules;
    }

    public void Dispose()
    {
        _inputHandler.OpenRules -= OnOpenRules;
        _inputHandler.CloseRules -= OnCloseRules;
    }

    private void OnOpenRules() {
        _windowsPresenter.ShowRules();
    }

    private void OnCloseRules() {
        _windowsPresenter.HideRules();
    }
}