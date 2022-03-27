using Core.Common;
using System;
using UnityEngine;

namespace Core.Enemy
{
    public class EnemyDeath : Death
    {
        public event Action<GameObject> EnemyDied;

        protected override void HandleDeath() =>
            EnemyDied?.Invoke(gameObject);
    }
}