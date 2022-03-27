using Services;
using CompositionRoot.SceneLoad;
using CompositionRoot.States;
using UnityEngine;

namespace CompositionRoot
{
    public class EntryPoint : MonoBehaviour, ICoroutineRunner
    {
        [SerializeField] private ScreenTransition _screenTransitionPrefab;

        private GameStateMachine _stateMachine;

        void Awake()
        {
            DontDestroyOnLoad(this);

            var locator = new Locator();
            var screenTransition = Instantiate(_screenTransitionPrefab);
            var sceneLoader = new SceneLoader(this, screenTransition);
            _stateMachine = new GameStateMachine(locator, sceneLoader);

            _stateMachine.Enter<BootstrapState>();
        }
    }
}