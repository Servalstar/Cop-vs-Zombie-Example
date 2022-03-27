using UnityEngine;

namespace Core.Common
{
    public abstract class Death : MonoBehaviour
    {
        [SerializeField] private Health _health;
        [SerializeField] private CharacterAnimator _animator;
        [SerializeField] private BoxCollider _collider;

        private void OnEnable() =>
            _collider.enabled = true;

        private void Start() =>
            _health.HealthChanged += CheckHealth;

        private void CheckHealth()
        {
            if (Mathf.Approximately(0, _health.Current))
            {
                _animator.PlayDeath();
                _collider.enabled = false;
                HandleDeath();
            }
        }

        protected abstract void HandleDeath();
    }
}