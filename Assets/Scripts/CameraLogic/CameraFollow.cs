using UnityEngine;

namespace Assets.Scripts.CameraLogic
{
    public class CameraFollow : MonoBehaviour
    {
        [SerializeField] private float _rotationAngleX;
        [SerializeField] private float _distance;

        private Transform _player;

        private void LateUpdate()
        {
            if (_player == null)
                return;

            Quaternion rotation = Quaternion.Euler(_rotationAngleX, 0, 0);
            Vector3 position = rotation * new Vector3(0, 0, -_distance) + _player.position;

            transform.rotation = rotation;
            transform.position = position;
        }

        public void Follow(GameObject following) =>
            _player = following.transform;
    }
}

