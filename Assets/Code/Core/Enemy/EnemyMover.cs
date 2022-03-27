using Core.Common;
using UnityEngine;
using UnityEngine.AI;

namespace Core.Enemy
{
    public class EnemyMover : Mover
    {
        [SerializeField] private NavMeshAgent _navMeshAgent;
        [SerializeField] private Health _health;
        [SerializeField] private float _minimalDistance;

        private Transform _heroTransform;

        public void Construct(Transform heroTransform) =>
            _heroTransform = heroTransform;

        private void Update()
        {
            if (CanMove())
            {
                _navMeshAgent.destination = _heroTransform.position;
                CurrentSpeed = _navMeshAgent.velocity.magnitude;
            }
            else if (Mathf.Approximately(0, CurrentSpeed) == false)
            {
                CurrentSpeed = 0;
                _navMeshAgent.ResetPath();
            }
        }

        private bool CanMove() =>
            _heroTransform != null && EnemyIsAlive() && IsHeroNotReached();

        private bool EnemyIsAlive() =>
            Mathf.Approximately(0, _health.Current) == false;

        private bool IsHeroNotReached() =>
            Vector3.Distance(transform.position, _heroTransform.position) > _minimalDistance;
    }
}