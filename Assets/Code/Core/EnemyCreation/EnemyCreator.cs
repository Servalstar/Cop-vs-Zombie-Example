using Services;
using System.Collections;
using UnityEngine;

namespace Core.EnemyCreation
{
    public class EnemyCreator : MonoBehaviour, ICoroutineRunner
    {
        private EnemyPool _objectPool;
        private Vector3 _spawnPoint;
        private float _frenquency;

        private void Start()
        {
            _objectPool.Construct(this);
            StartCoroutine(CreateEnemy());
        }

        public void Construct(EnemyPool objectPool, Vector3 spawnPoint, float frenquency)
        {
            _objectPool = objectPool;
            _spawnPoint = spawnPoint;
            _frenquency = frenquency;
        }

        private IEnumerator CreateEnemy()
        {
            var delay = new WaitForSeconds(_frenquency);

            while (true)
            {
                var enemy = _objectPool.Get();
                enemy.transform.position = _spawnPoint;
                enemy.SetActive(true);

                yield return delay;
            }
        }
    }
}