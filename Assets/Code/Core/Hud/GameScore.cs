using Core.EnemyCreation;
using TMPro;
using UnityEngine;

namespace Core.Hud
{
    public class GameScore : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _scoreText;

        public int Score { get; private set; }

        private void Start() => 
            UpdateScore();

        public void Construct(EnemyPool pool) =>
            pool.EnemyDied += UpdateScore;

        public void UpdateScore()
        {
            _scoreText.text = "Score: " + Score;
            Score++;
        }
    }
}