using System;

namespace Assets.Scripts.Logic
{
    public interface IScoreCounter
    {
        int CurrentScore { get; }

        event Action<int> OnScoreChanged;

        void IncreaseScore(int points);
    }
}