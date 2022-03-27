using Core.Common;
using UnityEngine;
using UnityEngine.UI;

namespace Core.Hud.EnemyHealthBar
{
    public class EnemyHealthBar : MonoBehaviour
    {
        [SerializeField] private Image _healthBarImage;
        [SerializeField] private Health _health;

        private void Start() =>
            _health.HealthChanged += SetValue;

        private void SetValue() =>
            _healthBarImage.fillAmount = _health.Current / _health.Max;
    }
}