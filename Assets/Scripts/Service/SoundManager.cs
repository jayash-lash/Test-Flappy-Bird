using System;
using IoC;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

namespace Service
{
    public class SoundManager : MonoBehaviour, IService
    {
        public Type ServiceType => typeof(SoundManager);

        [SerializeField] private AudioMixerGroup _master;
        [SerializeField] private AudioSource _effectSourceJump;
        [SerializeField] private AudioSource _effectSourceClick;
        [SerializeField] private AudioSource _effectSourceLose;
        [SerializeField] private AudioSource _effectSourceHighScore;
        [SerializeField] private Slider _slider;

        private float _soundEffectsVolume;
        private GameSaves _gameSaves;
        private SaveService _saveService;

        private void Awake()
        {
            _saveService = ServiceLocator.Resolve<SaveService>();
            _gameSaves = _saveService.LoadFromCache<GameSaves>();

            _slider.onValueChanged.AddListener(UpdateMixerVolume);
        }

        private void Start()
        {
            _soundEffectsVolume = _gameSaves.Volume;
            UpdateMixerVolume(_soundEffectsVolume);
            _slider.value = _soundEffectsVolume;
        }

        public void PlaySoundJump(AudioClip clip) => _effectSourceJump.PlayOneShot(clip);

        public void PlaySoundClick(AudioClip clip) => _effectSourceClick.PlayOneShot(clip);

        public void PlaySoundLose(AudioClip clip) => _effectSourceLose.PlayOneShot(clip);

        public void PlaySoundHighScore(AudioClip clip) => _effectSourceHighScore.PlayOneShot(clip);

        private void UpdateMixerVolume(float value)
        {
            value = Mathf.Clamp(value, 0.0001f, 1f);
            _master.audioMixer.SetFloat("MasterVolume", Mathf.Log10(value) * 20);
        }
    }
}
