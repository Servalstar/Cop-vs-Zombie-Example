using Core.Common;
using UnityEngine;

namespace Core.Hero
{
    public class HeroAttack : MonoBehaviour
    {
        [SerializeField] private CharacterAnimator _animator;
        [SerializeField] private HeroMover _heroMover;
        [SerializeField] private ParticleSystem _particles;
        [SerializeField] private Health _health;
        [SerializeField] private float _damage;
        [SerializeField] private float _cooldown;
        [SerializeField] private float _attackRadius;

        private const float MaxSpeedForAttack = 0.1f;

        private readonly Collider[] _hits = new Collider[20];
        private int _layerMask;
        private float _attackLeftTime;
        private bool _isAttacking;

        private void Awake() =>
          _layerMask = 1 << LayerMask.NameToLayer("Enemy");

        private void Update()
        {
            UpdateCooldown();

            if (CanAtack())
                TryAttack();
        }

        private void TryAttack()
        {
            if (GetNearestTarget(out Collider target) > 0)
            {
                _isAttacking = true;
                transform.LookAt(target.transform.position);
                _animator.PlayAttack();
                _particles.Play();
                _attackLeftTime = _cooldown;

                target.transform.GetComponent<IHealth>().TakeDamage(_damage);
            }
        }

        private void OnAttackFinished() => 
            _isAttacking = false;

        private void UpdateCooldown()
        {
            if (CooldownIsFinished() == false)
                _attackLeftTime -= Time.deltaTime;
        }

        private bool CanAtack() =>
            CooldownIsFinished() && HeroIsNotMoving() && HeroIsAlive() && _isAttacking == false;

        private bool CooldownIsFinished() =>
            _attackLeftTime <= 0f;

        private bool HeroIsNotMoving() =>
            _heroMover.CurrentSpeed < MaxSpeedForAttack;

        private bool HeroIsAlive() =>
            Mathf.Approximately(0, _health.Current) == false;

        private int GetNearestTarget(out Collider target)
        {
            var hitAmount = Physics.OverlapSphereNonAlloc(transform.position, _attackRadius, _hits, _layerMask);
            target = FindNearestTarget(_hits, hitAmount);

            return hitAmount;
        }

        private Collider FindNearestTarget(Collider[] targets, int hitAmount)
        {
            var distance = float.MaxValue;
            Collider nearestTarget = null;

            for (int i = 0; i < hitAmount; i++)
            {
                var currentDistance = Vector3.Distance(transform.position, targets[i].transform.position);

                if (currentDistance < distance)
                {
                    distance = currentDistance;
                    nearestTarget = targets[i];
                }
            }

            return nearestTarget;
        }
    }
}