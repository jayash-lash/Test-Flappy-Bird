using System.Globalization;
using IoC;
using Player;
using Service;
using TMPro;
using UnityEngine;

namespace UI
{
    public class ScoreWindow : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _highScoreText;
        [SerializeField] private TextMeshProUGUI _scoreText;

        [SerializeField] private AudioClip _audioClip;

        private PlayerModel _playerModel;
        private SoundManager _soundManager;
    
        private void Start()
        {
            _soundManager = ServiceLocator.Resolve<SoundManager>();
            _playerModel = ServiceLocator.Resolve<PlayerModel>();
            _playerModel.OnModelChanged += OnPlayerModelChanged;
            UpdateHighScoreText();
        }

        private void OnDestroy()
        {
            _playerModel.OnModelChanged -= OnPlayerModelChanged;
        }

        private void OnPlayerModelChanged()
        {
            if (!_playerModel.IsAlive && gameObject.activeSelf)
            {
                Hide();
                return;
            }
        
            _scoreText.text = _playerModel.ScoreAmount.ToString(CultureInfo.InvariantCulture);
            UpdateHighScoreText();
        }

        private void UpdateHighScoreText()
        {
            _highScoreText.text = "TopScore: " + Mathf.Max(_playerModel.HighScore, _playerModel.ScoreAmount).
                ToString(CultureInfo.InvariantCulture);
            
            _soundManager.PlaySoundHighScore(_audioClip);
        }

        private void Hide() 
        {
            gameObject.SetActive(false);
        }

        private void Show() 
        {
            gameObject.SetActive(true);
        }
    }
}