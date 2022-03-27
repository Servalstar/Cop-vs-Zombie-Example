using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace CompositionRoot.SceneLoad
{
    public class ScreenTransition : MonoBehaviour
    {
        [SerializeField] private Image _shadow;

        private const float FadeSpeedIn = 2f;
        private const float FadeSpeedOut = 2f;

        private void Awake() => 
            DontDestroyOnLoad(this);

        public void Show(Action action)
        {
            gameObject.SetActive(true);
            _shadow.color = new Color(0, 0, 0, 0);
            StartCoroutine(DoFadeIn(action));
        }

        public void Hide()
        {
            _shadow.color = new Color(0, 0, 0, 1);
            StartCoroutine(DoFadeOut());
        }

        private IEnumerator DoFadeIn(Action action)
        {
            while (_shadow.color.a < 0.99f)
            {
                _shadow.color += new Color(0, 0, 0, FadeSpeedIn * Time.deltaTime);
                yield return null;
            }

            action?.Invoke();
        }

        private IEnumerator DoFadeOut()
        {
            while (_shadow.color.a > 0.01f)
            {
                _shadow.color -= new Color(0, 0, 0, FadeSpeedOut * Time.deltaTime);
                yield return null;
            }

            gameObject.SetActive(false);
        }
    }
}