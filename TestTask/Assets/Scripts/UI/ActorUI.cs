using Assets.Scripts.Infrastructure.Services.ProgressService;
using Assets.Scripts.Logic;
using UnityEngine;

namespace Assets.Scripts.UI
{
    public class ActorUI : MonoBehaviour
    {
        [SerializeField] private Score _score;

        private IScoreCounter _playerScoreCounter;
        private IScoreCounter _enemyScoreCounter;
        private ISavedProgress _savedProgress;

        public void Construct(IScoreCounter playerScoreCounter, IScoreCounter enemyScoreCounter)
        {
            _playerScoreCounter = playerScoreCounter;
            _enemyScoreCounter = enemyScoreCounter;

            _playerScoreCounter.OnScoreChanged += UpdatePlayerScore;
            _enemyScoreCounter.OnScoreChanged += UpdateEnemyScore;
        }

        private void OnDisable()
        {
            _playerScoreCounter.OnScoreChanged -= UpdatePlayerScore;
            _enemyScoreCounter.OnScoreChanged -= UpdateEnemyScore;
        }

        private void UpdatePlayerScore(int score) =>
            _score.PlayerSetValue(score);

        private void UpdateEnemyScore(int score) =>
            _score.EnemySetValue(score);
    }
}
