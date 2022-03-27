using UnityEditor;
using UnityEngine;

namespace Core.EnemyCreation.Editor
{
    [CustomEditor(typeof(SpawnData))]
    public class SpawnPointFinder : UnityEditor.Editor
    {
        private const string HeroSpawnPoint = "HeroSpawnPoint";
        private const string EnemiesSpawnPoint = "EnemiesSpawnPoint";

        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();

            SpawnData spawnData = (SpawnData)target;

            if (GUILayout.Button("Find spawn points"))
            {
                spawnData.SpawnHeroPosition = GameObject.Find(HeroSpawnPoint).transform.position;
                spawnData.SpawnEnemiesPosition = GameObject.Find(EnemiesSpawnPoint).transform.position;
            }

            EditorUtility.SetDirty(target);
        }
    }
}