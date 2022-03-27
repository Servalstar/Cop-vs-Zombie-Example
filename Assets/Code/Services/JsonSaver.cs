using System.IO;
using UnityEngine;

namespace Services
{
    public class JsonSaver
    {
        public T Load<T>(string filename) where T : class
        {
            var path = GetFilePath(filename);

            return FileExists(path) ? JsonUtility.FromJson<T>(File.ReadAllText(path)) : default;
        }

        public void Save<T>(string filename, T data) where T : class
        {
            var path = GetFilePath(filename);
            File.WriteAllText(path, JsonUtility.ToJson(data));
        }

        public bool FileExists(string path) =>
            File.Exists(path);

        private string GetFilePath(string filename)
        {
#if UNITY_STANDALONE || UNITY_EDITOR
            var path = Path.Combine(Application.dataPath, filename);
#elif UNITY_IOS || UNITY_ANDROID
            var path = Path.Combine(Application.persistentDataPath, filename);
#endif
            return path;
        }
    }
}