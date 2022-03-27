using UnityEngine;

namespace Core.Hero
{
    public class CameraMover : MonoBehaviour
    {
        [SerializeField] private float _rotationAngleX;
        [SerializeField] private float _rotationAngleY;
        [SerializeField] private float _distance;
        [SerializeField] private float _offsetY;

        private Transform _following;

        private void LateUpdate()
        {
            if (_following == null)
                return;

            var rotation = Quaternion.Euler(_rotationAngleX, _rotationAngleY, 0);
            var position = rotation * new Vector3(0, 0, -_distance) + FollowingPointPosition();

            transform.SetPositionAndRotation(position, rotation);
        }

        public void Follow(GameObject following) =>
            _following = following.transform;

        private Vector3 FollowingPointPosition()
        {
            var followingPosition = _following.position;
            followingPosition.y += _offsetY;

            return followingPosition;
        }
    }
}