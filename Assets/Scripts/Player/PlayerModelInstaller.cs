using IoC;
using Service;
using UnityEngine;

namespace Player
{
    [DefaultExecutionOrder(-10)]
    public class PlayerModelInstaller : MonoBehaviour
    {
        private PlayerModel _playerModel;

        private void Awake()
        {
            var levelManager = ServiceLocator.Resolve<LevelManager>();
            var saveService = ServiceLocator.Resolve<SaveService>();
            var gameSaves = saveService.LoadFromCache<GameSaves>();
            var highScore = gameSaves.GetScore(levelManager.CurrentDifficulty);
        
            _playerModel = new PlayerModel(levelManager, highScore, saveService);
            ServiceLocator.Register(_playerModel);
        }

        private void OnDestroy()
        {
            ServiceLocator.Remove(_playerModel);
        }
    }
}