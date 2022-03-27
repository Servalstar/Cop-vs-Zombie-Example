using Core.Enemy;
using System;
using System.Collections.Generic;
using UnityEngine;
using System.Collections;
using Services;

namespace Core.EnemyCreation
{
    public class EnemyPool
    {
        public event Action EnemyDied;

        private readonly EnemyFactory _enemyFactory;
        private readonly Stack<GameObject> _pool = new Stack<GameObject>();
        private ICoroutineRunner _coroutineRunner;

        public EnemyPool(EnemyFactory enemyFactory) => 
            _enemyFactory = enemyFactory;

        public void Construct(ICoroutineRunner coroutineRunner) => 
            _coroutineRunner = coroutineRunner;

        public GameObject Get()
        {
            if (_pool.Count == 0)
                FillPool(10);

            return _pool.Pop();
        }

        private void FillPool(int count)
        {
            var enemies = _enemyFactory.CreateEnemies(count);

            foreach (var enemy in enemies)
            {
                enemy.GetComponent<EnemyDeath>().EnemyDied += ReturnToPool;
                _pool.Push(enemy);
            }
        }

        private void ReturnToPool(GameObject enemy)
        {
            EnemyDied?.Invoke();

            _coroutineRunner.StartCoroutine(ReturnToPoolWithDelay(enemy));
        }

        private IEnumerator ReturnToPoolWithDelay(GameObject enemy)
        {
            yield return new WaitForSeconds(3);

            enemy.SetActive(false);
            _pool.Push(enemy);
        }
    }
}