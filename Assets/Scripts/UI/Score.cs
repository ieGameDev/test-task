using System;
using TMPro;
using UnityEngine;

namespace Assets.Scripts.UI
{
    public class Score : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _enemyScore;
        [SerializeField] private TextMeshProUGUI _playerScore;

        public void PlayerSetValue(int score) =>
            _playerScore.text = Convert.ToString(score);

        public void EnemySetValue(int score) =>
            _enemyScore.text = Convert.ToString(score);
    }
}
