using Assets.Scripts.Logic;
using System;
using UnityEngine;

namespace Assets.Scripts.Player
{
    public class PlayerScoreCounter : MonoBehaviour, IScoreCounter
    {
        private int _currentScore;

        public event Action<int> OnScoreChanged;

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

        public void IncreaseScore(int points)
        {
            CurrentScore += points;

            Debug.Log(CurrentScore);
        }
    }
}
