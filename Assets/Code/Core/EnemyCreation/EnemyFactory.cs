using Core.Enemy;
using UnityEngine;

namespace Core.EnemyCreation
{
    public class EnemyFactory
    {
        private readonly GameObject _enemyPrefab;
        private readonly GameObject _hero;
        private GameObject _parent;

        public EnemyFactory(GameObject enemyPrefab, GameObject hero)
        {
            _enemyPrefab = enemyPrefab;
            _hero = hero;
        }

        public GameObject[] CreateEnemies(int count)
        {
            if (_parent == null)
                CreateParent();

            var enemiesArray = new GameObject[count];

            for (int i = 0; i < count; i++)
            {
                var enemy = Object.Instantiate(_enemyPrefab, _parent.transform);
                enemy.GetComponent<EnemyAttack>().Construct(_hero.transform);
                enemy.GetComponent<EnemyMover>().Construct(_hero.transform);
                enemiesArray[i] = enemy;
            }

            return enemiesArray;
        }

        private void CreateParent()
        {
            _parent = Object.Instantiate(new GameObject());
            _parent.name = "EnemiesPool";
        }
    }
}