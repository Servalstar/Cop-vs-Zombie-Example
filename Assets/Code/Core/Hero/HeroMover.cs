using Core.Common;
using Services.Input;
using UnityEngine;

namespace Core.Hero
{
    public class HeroMover : Mover
    {
        [SerializeField] private CharacterController _characterController;
        [SerializeField] private Health _health;
        [SerializeField] protected float _movementSpeed;

        private IInputService _inputService;
        private Camera _camera;

        private void Start() =>
            _camera = Camera.main;

        public void Construct(IInputService inputService) =>
            _inputService = inputService;

        private void Update()
        {
            Vector3 movementVector = Vector3.zero;

            if (CanMove())
            {
                movementVector = _camera.transform.TransformDirection(_inputService.Direction);
                movementVector.y = 0;
                movementVector.Normalize();

                transform.forward = movementVector;
            }

            _characterController.Move(_movementSpeed * movementVector * Time.deltaTime);

            CurrentSpeed = _characterController.velocity.magnitude;
        }

        private bool CanMove() =>
            _inputService.Direction.sqrMagnitude > Mathf.Epsilon && HeroIsAlive();

        private bool HeroIsAlive() =>
            Mathf.Approximately(0, _health.Current) == false;
    }
}