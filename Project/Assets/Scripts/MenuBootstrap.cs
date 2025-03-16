using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class MenuBootstrap : Bootstrap {
    [SerializeField] private UIInputHandler _uiInputHandler;
    [SerializeField] private LevelConfig _levelConfig;
    [SerializeField] private AudioSource _musicSource;
    [SerializeField] private List<AudioSource> _effectsSources = new List<AudioSource>();
    [SerializeField] private AudioMixer _mixer;
    [Header("UI")] 
    [SerializeField] private Slider _rowsSlider;
    [SerializeField] private Slider _columnsSlider;
    [SerializeField] private Slider _upperHintsIntervalSlider;
    [SerializeField] private RadialSlider _musicSlider;
    [SerializeField] private RadialSlider _effectsSlider;
    [SerializeField] private Slider _sensitivitySlider;
    [SerializeField] private Slider _fovSlider;
    [SerializeField] private LanguagesDropdown _languagesDropdown;

    private void Start() {
        var quickGameView = new QuickGameView(_rowsSlider, _columnsSlider, _upperHintsIntervalSlider);
        var levelConfigurator = new LevelConfigurator(quickGameView, _levelConfig);
        var menuScenesNavigator = new MenuScenesNavigator(_uiInputHandler, levelConfigurator);

        var settingsView = new SettingsView(_musicSlider, _effectsSlider, _sensitivitySlider, _fovSlider, _languagesDropdown);
        var settingsApplier = new MenuSettingsApplier(_mixer);
        var settingsSaver = new SettingsSaver("Settings");
        var settingsLoader = new SettingsLoader(settingsSaver.Path);
        var settingsController = new SettingsController(_uiInputHandler, settingsView, settingsApplier, settingsSaver, settingsLoader);
        
        settingsController.ConfigurateSettings();

        SetDisposables(new List<IDisposable>() {
            menuScenesNavigator,
            settingsController
        });
    }
}