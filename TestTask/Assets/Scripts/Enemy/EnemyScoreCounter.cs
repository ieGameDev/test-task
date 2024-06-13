using Assets.Scripts.Logic;
using System;
using UnityEngine;

namespace Assets.Scripts.Enemy
{
    public class EnemyScoreCounter : MonoBehaviour, IScoreCounter
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
        }
    }
}
