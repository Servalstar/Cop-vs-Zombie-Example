using System;
using UnityEngine;

namespace Core.Common
{
    public class Health : MonoBehaviour, IHealth
    {
        [SerializeField] private int _maxHealth;

        public event Action HealthChanged;

        public float Max => _maxHealth;
        public float Current { get; private set; }

        private void OnEnable()
        {
            Current = _maxHealth;
            HealthChanged?.Invoke();
        }

        public void TakeDamage(float damage)
        {
            if (Mathf.Approximately(0, Current) == false)
            {
                Current = Mathf.Clamp(Current - damage, 0, _maxHealth);
                HealthChanged?.Invoke();
            }
        }
    }
}