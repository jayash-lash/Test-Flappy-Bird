using IoC;
using Player;
using Service;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace UI
{
    public class GameOverWindow : MonoBehaviour
    {
        [SerializeField] private AudioClip _audioClip;
        
        [SerializeField] private TextMeshProUGUI _highScoreText;
        [SerializeField] private TextMeshProUGUI _scoreText;
        
        [SerializeField] private Button _restartButton;
        [SerializeField] private Button _mainMenuButton;
        
        private SoundManager _soundManager;
        private PlayerModel _playerModel;
        private LevelManager _levelManager;
        private int _initialHighScore;

        private void Awake()
        {
            _playerModel = ServiceLocator.Resolve<PlayerModel>();
            _levelManager = ServiceLocator.Resolve<LevelManager>();
            _soundManager = ServiceLocator.Resolve<SoundManager>();
            
            _initialHighScore = _playerModel.HighScore;
            _playerModel.OnModelChanged += ShowGameResult;
            _restartButton.onClick.AddListener(Restart);
            _mainMenuButton.onClick.AddListener(LoadMainMenu);
            gameObject.SetActive(false);
        }

        private void ShowGameResult()
        {
            if (!_playerModel.IsAlive)
            {
                _soundManager.PlaySoundLose(_audioClip);
                Show();
            }
            
            if (!gameObject.activeSelf) return;
            _scoreText.text = _playerModel.ScoreAmount.ToString();

            var prefix = _playerModel.ScoreAmount > _initialHighScore ? "New TopScore: " : "TopScore: ";
            _highScoreText.text = prefix + _playerModel.HighScore;
        }

        private void Restart()
        {
           _levelManager.RestartCurrentLevel();
        }

        private void LoadMainMenu()
        {
            SceneManager.LoadScene("Scenes/MainMenu");
        }

        private void Show()
        {
            gameObject.SetActive(true);
        }
        private void OnDestroy()
        {
            _playerModel.OnModelChanged -= ShowGameResult;
        }
    }
}