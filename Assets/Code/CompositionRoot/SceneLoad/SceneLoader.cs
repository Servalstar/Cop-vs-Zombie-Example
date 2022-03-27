using Services;
using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace CompositionRoot.SceneLoad
{
    public class SceneLoader
    {
        private readonly ICoroutineRunner _coroutineRunner;
        private readonly ScreenTransition _screenTransition;

        public SceneLoader(ICoroutineRunner coroutineRunner, ScreenTransition screenTransition)
        {
            _coroutineRunner = coroutineRunner;
            _screenTransition = screenTransition;
        }

        public void Load(string name, Action onLoaded = null)
        {
            if (SceneManager.GetActiveScene().name == name)
                onLoaded?.Invoke();
            else
                _screenTransition.Show(() => _coroutineRunner.StartCoroutine(LoadScene(name, onLoaded)));
        }

        private IEnumerator LoadScene(string nextScene, Action onLoaded = null)
        {
            AsyncOperation waitNextScene = SceneManager.LoadSceneAsync(nextScene);

            while (!waitNextScene.isDone)
                yield return null;

            onLoaded?.Invoke();
            _screenTransition.Hide();
        }
    }
}