﻿using Assets.Scripts.Logic;
using System;
using UnityEngine;
using Assets.Scripts.Data;
using Assets.Scripts.Infrastructure.Services.ProgressService;

namespace Assets.Scripts.Enemy
{
    public class EnemyScoreCounter : ScoreCounter, IScoreCounter, ISavedProgress
    {
        private int _currentScore;

        public int CurrentScore
        {
            get => _currentScore;
            private set
            {
                if (_currentScore != value)
                {
                    _currentScore = value;
                    OnScoreChanged?.Invoke(_currentScore);
                }
            }
        }

        public event Action<int> OnScoreChanged;

        public void IncreaseScore(int points)
        {
            CurrentScore += points;
            Debug.Log(CurrentScore);

            StartCoroutine(RestartGame());
        }

        public void UpdateProgress(Progress progress) => 
            progress.EnemyScore = CurrentScore;

        public void LoadProgress(Progress progress)
        {
            CurrentScore = progress.EnemyScore;
            OnScoreChanged?.Invoke(CurrentScore);
        }
    }
}
