using CompositionRoot.States;
using UnityEngine;
using UnityEngine.UI;

namespace Core.EndGame
{
    public class EndGameWindow : MonoBehaviour
    {
        [SerializeField] private Button _goToMemuButton;

        private GameStateMachine _stateMachine;

        private void Start() => 
            _goToMemuButton.onClick.AddListener(() => _stateMachine.Enter<MenuState>());

        public void Construct(GameStateMachine stateMachine) =>
            _stateMachine = stateMachine;

        public void Show() =>
            gameObject.SetActive(true);
    }
}