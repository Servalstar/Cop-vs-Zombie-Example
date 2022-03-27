using CompositionRoot.States;
using UnityEngine;
using UnityEngine.UI;

namespace Menu
{
    public class BattleButton : MonoBehaviour
    {
        [SerializeField] private Button _button;

        private GameStateMachine _stateMachine;

        private void Start() =>
            _button.onClick.AddListener(() => _stateMachine.Enter<GameState>());

        public void Construct(GameStateMachine stateMachine) =>
            _stateMachine = stateMachine;
    }
}