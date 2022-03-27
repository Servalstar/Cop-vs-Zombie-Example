using UnityEngine;

namespace Core.Common
{
    public class CharacterAnimator : MonoBehaviour
    {
        [SerializeField] private Animator _animator;
        [SerializeField] private Mover _mover;
        
        private const string Speed = "Speed";
        private const string Attack = "Attack";
        private const string Death = "Death";

        private void Update() => 
            _animator.SetFloat(Speed, _mover.CurrentSpeed);

        public void PlayAttack() => 
            _animator.SetTrigger(Attack);

        public void PlayDeath() => 
            _animator.SetTrigger(Death);
    }
}