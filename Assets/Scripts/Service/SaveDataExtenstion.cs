using System;
using Common;
using Cummon;
using UnityEngine;

namespace Service
{
    public static class SaveDataExtenstion
    {
        public static int GetScore(this GameSaves saves, DifficultyType type)
        {
            switch (type)
            {
                case DifficultyType.Easy:
                    return saves.HighScores[0];
                case DifficultyType.Normal:
                    return saves.HighScores[1];
                case DifficultyType.Hard:
                    return saves.HighScores[2];
            }

            throw new NotImplementedException();
        }

        public static void SetScore(this GameSaves saves, int scores, DifficultyType difficultyType)
        {
            switch (difficultyType)
            {
                case DifficultyType.Easy:
                    saves.HighScores[0] = scores;
                    return;
                case DifficultyType.Normal:
                    saves.HighScores[1] = scores;
                    return;
                case DifficultyType.Hard:
                    saves.HighScores[2] = scores;
                    return;
            }

            throw new NotImplementedException("Unhandled difficulty:" + difficultyType);
        }
    }
}