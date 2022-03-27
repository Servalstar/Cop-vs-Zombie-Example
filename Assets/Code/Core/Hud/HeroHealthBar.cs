using Core.Common;
using UnityEngine;
using UnityEngine.UI;

namespace Core.Hud
{
    public class HeroHealthBar : MonoBehaviour
    {
        [SerializeField] private Image _healthBarImage;
        [SerializeField] private Gradient _gradient;

        private IHealth _health;

        private void Start()
        {
            _health.HealthChanged += SetValue;
            SetValue();
        }

        public void Construct(IHealth health) =>
            _health = health;

        private void SetValue()
        {
            var fillAmount = _health.Current / _health.Max;
            _healthBarImage.fillAmount = fillAmount;
            _healthBarImage.color = _gradient.Evaluate(fillAmount);
        }
    }
}