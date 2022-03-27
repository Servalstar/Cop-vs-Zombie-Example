using UnityEngine;

namespace Core.EnemyCreation
{
    [CreateAssetMenu(fileName = "SpawnData", menuName = "Spawn Data")]
    public class SpawnData : ScriptableObject
    {
        public Vector3 SpawnHeroPosition;
        public Vector3 SpawnEnemiesPosition;
        public float SpawnDelay;
    }
}