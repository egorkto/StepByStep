using System;
using System.Collections.Generic;
using Unity.Cinemachine;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.Tilemaps;
using UnityEngine.UI;

public class LevelBootstrap : Bootstrap {
    [Header("First person controller")]
    [SerializeField] private CharacterController _controller;
    [SerializeField] private PlayerStats _stats;
    [SerializeField] private float _interactDistance;
    [Header("Level spawning")]
    [SerializeField] private LevelConfig _levelConfig;
    [SerializeField] private Tilemap _platesTilemap;
    [SerializeField] private Tilemap _signsTilemap;
    [SerializeField] private TileBase _solidPlateTile;
    [SerializeField] private TileBase _transparentPlateTile;
    [SerializeField] private TileBase _sideSignTile;
    [SerializeField] private TileBase _upperSignTile;
    [SerializeField] private TileBase _innerFloorTile;
    [SerializeField] private TileBase _edgeFloorTile;
    [SerializeField] private TileBase _cornerFloorTile;
    [SerializeField] private TileBase _platformOriginTile;
    [SerializeField] private GameObject _levelBorders;
    [Header("UI")]
    [SerializeField] private UIInputHandler _uIInputHandler;
    [SerializeField] private GameObject _pauseWindow;
    [SerializeField] private GameObject _settingsConfirmWindow;
    [SerializeField] private GameObject _loseWindow;
    [SerializeField] private GameObject _winWindow;
    [SerializeField] private GameObject _rulesWindow;
    [SerializeField] private RadialSlider _musicSlider;
    [SerializeField] private RadialSlider _effectsSlider;
    [SerializeField] private Slider _sensitivitySlider;
    [SerializeField] private Slider _fovSlider;
    [SerializeField] private Timer _timer;
    [SerializeField] private LanguagesDropdown _languagesDropdown;
    [Header("Settings targets")]
    [SerializeField] private CinemachineInputAxisController _cinemachineinputAxisController;
    [SerializeField] private CinemachineCamera _camera;
    [SerializeField] private AudioMixer _mixer;
    [Header("Level finisher")]
    [SerializeField] private Animator _finishLevelAnimator;
    [SerializeField] private PlayerTrigger _loseTriggerPrefab;
    [SerializeField] private AudioSource _winSource;
    [SerializeField] private AudioSource _loseSource;
    [SerializeField] private MusicTuner _musicTuner;

    private FirstPersonController _firstPersonController;

    private void Start()
    {
        var inputActionsHandler = new InputActionsHandler();
        _firstPersonController = new FirstPersonController(inputActionsHandler, _controller, Camera.main, _interactDistance, _stats);
        var platesFieldSpawner = new PlatesFieldSpawner(_platesTilemap, _signsTilemap, _solidPlateTile, _transparentPlateTile, _sideSignTile, _upperSignTile);
        var saveZoneSpawner = new SaveZoneSpawner(_platesTilemap, _innerFloorTile, _edgeFloorTile, _cornerFloorTile, _platformOriginTile);
        var levelSpawner = new LevelSpawner(_platesTilemap.layoutGrid, saveZoneSpawner, platesFieldSpawner, _levelBorders, _loseTriggerPrefab, _controller);

        levelSpawner.SpawnLevel(_levelConfig);

        var settingsView = new SettingsView(_musicSlider, _effectsSlider, _sensitivitySlider, _fovSlider, _languagesDropdown);
        var sensitivityBounds = new Vector2(_sensitivitySlider.minValue, _sensitivitySlider.maxValue);
        var settingsApplier = new LevelSettingsApplier(_mixer, _cinemachineinputAxisController, _camera, sensitivityBounds);
        var settingsSaver = new SettingsSaver("Settings");
        var settingsLoader = new SettingsLoader(settingsSaver.Path);
        var settingsController = new SettingsController(_uIInputHandler, settingsView, settingsApplier, settingsSaver, settingsLoader);

        settingsController.ConfigurateSettings();

        var uiWindowsPresenter = new UIWindowsPresenter(_pauseWindow, _settingsConfirmWindow, _loseWindow, _winWindow, _rulesWindow);

        var rulesShower = new RulesShower(inputActionsHandler, uiWindowsPresenter);

        var finishLevelEffector = new FinishLevelEffector(_winSource, _loseSource, _musicTuner, _finishLevelAnimator);
        var playerTracker = new PlayerTracker(levelSpawner);
        var statesGenerator = new LevelStatesGenerator(settingsView, _musicTuner, inputActionsHandler, finishLevelEffector, uiWindowsPresenter, _timer);
        var levelStateMachine = new LevelStateMachine(statesGenerator, inputActionsHandler, _uIInputHandler, playerTracker);

        var scenesNavigator = new LevelScenesNavigator(_uIInputHandler);

        _timer.Enable();

        SetDisposables(new List<IDisposable>() {
            inputActionsHandler,
            settingsController,
            playerTracker,
            levelStateMachine,
            scenesNavigator
        });
    }

    private void FixedUpdate()
    {
        _firstPersonController.FixedUpdate();
    }
}