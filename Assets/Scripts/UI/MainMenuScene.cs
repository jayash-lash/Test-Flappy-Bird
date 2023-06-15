using System;
using Common;
using Cummon;
using IoC;
using Service;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

namespace UI
{
    public class MainMenuScene : MonoBehaviour
    {
        [SerializeField] private AudioClip _audioClip;
        [SerializeField] private LevelManager _levelManager;
        [SerializeField] private AudioMixerGroup _audioMixer;
        
        [Header("MainMenuButton")]
        [SerializeField] private Button _startButton;
        [SerializeField] private Button _optionButton;
        [SerializeField] private Button _exitButton;

        [Header("LevelsWindow")] 
        [SerializeField] private GameObject _levelWindow;
        [SerializeField] private Button _easyButton;
        [SerializeField] private Button _middleButton;
        [SerializeField] private Button _hardButton;
        [SerializeField] private Button _back1;

        [Header("OptionWindow")] 
        [SerializeField] private GameObject _optionWindow;
        [SerializeField] private Slider _slider;
        [SerializeField] private Button _back2;

        private SaveService _saveService;
        private GameSaves _gameSaves;
        private SoundManager _soundManager;
        
        private void Awake()
        {
            _soundManager = ServiceLocator.Resolve<SoundManager>();
            _saveService = ServiceLocator.Resolve<SaveService>();
            _gameSaves = _saveService.LoadFromCache<GameSaves>();
            _slider.value = _gameSaves.Volume;

            _slider.onValueChanged.AddListener(OnVolumeChanged);
            
            LevelsWindowListener();
            OptionWindowListener();
            _exitButton.onClick.AddListener(Exit);
        }
        
        private void LevelsWindowListener()
        {
            _startButton.onClick.AddListener(SetOnLevelsWindow);
            
            _easyButton.onClick.AddListener(LoadEasyLevel);
            _middleButton.onClick.AddListener(LoadNormalLevel);
            _hardButton.onClick.AddListener(LoadHardLevel);
            _back1.onClick.AddListener(SetOffLevelsManager);
        }

        private void OptionWindowListener()
        {
            _optionButton.onClick.AddListener(SetOnOptionWindow);
            _back2.onClick.AddListener(SetOffOptionWindow);
        }

        public void PlaySound() => _soundManager.PlaySoundClick(_audioClip);
        
        private void SetOnOptionWindow() => _optionWindow.SetActive(true);

        private void SetOffOptionWindow() => _optionWindow.SetActive(false);

        private void SetOnLevelsWindow() => _levelWindow.SetActive(true);

        private void LoadEasyLevel() => _levelManager.LoadLevel(DifficultyType.Easy);

        private void LoadNormalLevel() => _levelManager.LoadLevel(DifficultyType.Normal);

        private void LoadHardLevel() => _levelManager.LoadLevel(DifficultyType.Hard);

        private void SetOffLevelsManager() => _levelWindow.SetActive(false);

        private void Exit() => Application.Quit();

        private void OnVolumeChanged(float volume) => _gameSaves.Volume = volume;

        private void OnDestroy() => _saveService.Save(_gameSaves);
    }
}
