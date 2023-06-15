using System;
using Common;
using Cummon;
using IoC;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Service
{
    public class LevelManager : MonoBehaviour, IService
    {
        public Type ServiceType => typeof(LevelManager);

        public DifficultyType CurrentDifficulty => GetDifficultyType();

        public void LoadLevel(DifficultyType difficultyType)
        {
            int sceneIndex = DifficultyToSceneIndex(difficultyType);
            SceneManager.LoadScene(sceneIndex);
        }

        private DifficultyType GetDifficultyType()
        {
            var sceneIndex = SceneManager.GetActiveScene().buildIndex;

            switch (sceneIndex)
            {
                case 1: return DifficultyType.Easy;
                case 2: return DifficultyType.Normal;
                case 3: return DifficultyType.Hard;
            }

            throw new NotImplementedException();
        }

        public int DifficultyToSceneIndex(DifficultyType difficultyType)
        {
            switch (difficultyType)
            {
                case DifficultyType.Easy: return 1;
                case DifficultyType.Normal: return 2;
                case DifficultyType.Hard: return 3;
            }

            throw new NotImplementedException();
        }

        public void RestartCurrentLevel()
        {
            var currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
            SceneManager.LoadScene(currentSceneIndex);
        }
    }
}