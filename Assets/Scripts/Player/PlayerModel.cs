using System;
using IoC;
using Service;
using UnityEngine;

namespace Player
{
    public class PlayerModel : IService
    {
        public Type ServiceType => typeof(PlayerModel);

        public event Action OnModelChanged;

        public int ScoreAmount { get; private set; }
        public bool IsAlive { get; private set; } = true;
        public bool IsMoving { get; private set; }
    
        public int HighScore { get; private set; } 

        private LevelManager _levelManager;
        private SaveService _saveService;

        public PlayerModel(LevelManager levelManager, int highScore, SaveService saveService)
        {
            _levelManager = levelManager;
            HighScore = highScore;
            _saveService = saveService;
        }
    
        public void SetIsAlive(bool isAlive)
        {
            if (IsAlive == isAlive) return;
        
            IsAlive = isAlive;
            OnModelChanged?.Invoke();

            if (!IsAlive)
            {
                var gameSaves = _saveService.LoadFromCache<GameSaves>();
                
                var scores = Mathf.Max(HighScore, ScoreAmount);
                gameSaves.SetScore(scores, _levelManager.CurrentDifficulty);

                _saveService.Save(gameSaves);
            }
        }

        public void SetIsMoving(bool isMoving)
        {
            if (IsMoving == isMoving) return;
        
            IsMoving = isMoving;
            OnModelChanged?.Invoke();
        }
    
    

        public void IncreaseScoreAmount(int amount = 1)
        {
            if (amount <= 0) return;
            ScoreAmount += amount;
            OnModelChanged?.Invoke();
        }
    }
}