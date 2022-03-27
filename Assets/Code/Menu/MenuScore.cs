using Services;
using TMPro;
using UnityEngine;

namespace Menu
{
    public class MenuScore : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _scoreText;

        private PlayerProgress _playerProgress;

        private void Start() =>
            _scoreText.text = "Best score: " + _playerProgress.BestResult;

        public void Construct(PlayerProgress playerProgress) =>
            _playerProgress = playerProgress;
    }
}