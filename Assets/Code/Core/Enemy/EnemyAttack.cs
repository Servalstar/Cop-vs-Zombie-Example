using Core.Common;
using UnityEngine;

namespace Core.Enemy
{
    public class EnemyAttack : MonoBehaviour
    {
        [SerializeField] private CharacterAnimator _animator;
        [SerializeField] private Health _health;
        [SerializeField] private float _damage;
        [SerializeField] private float _minimalAttackDistance;

        private Transform _heroTransform;
        private IHealth _heroHealth;
        private bool _isAttacking;

        private void Start() => 
            _heroHealth = _heroTransform.GetComponent<IHealth>();

        public void Construct(Transform heroTransform) =>
            _heroTransform = heroTransform;

        private void Update()
        {
            if (CanAttack())
            {
                _animator.PlayAttack();
                _isAttacking = true;
            }
        }

        private void OnAttack()
        {
            if (EnemyIsAlive() && IsHeroReached())
                _heroHealth.TakeDamage(_damage);
        }

        private void OnAttackFinished() =>
            _isAttacking = false;

        private bool CanAttack() =>
            _heroTransform != null && EnemyIsAlive() && IsHeroReached() && _isAttacking == false;

        private bool EnemyIsAlive() =>
            Mathf.Approximately(0, _health.Current) == false;

        private bool IsHeroReached() =>
            Vector3.Distance(transform.position, _heroTransform.position) <= _minimalAttackDistance;
    }
}