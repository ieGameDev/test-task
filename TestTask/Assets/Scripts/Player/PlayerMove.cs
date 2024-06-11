using Assets.Scripts.Camera;
using Assets.Scripts.Infrastructure.Services.Input;
using UnityEngine;


namespace Assets.Scripts.Player
{
    [RequireComponent(typeof(CharacterController))]
    public class PlayerMove : MonoBehaviour
    {
        [SerializeField] private float _playerSpeed = 5.0f;
        [SerializeField] private float _rotationSpeed = 10.0f;
        [SerializeField] private CharacterController _characterController;

        private IInputService _inputService;
        private UnityEngine.Camera _camera;

        private void Awake()
        {
            _inputService = new StandaloneInput();
        }

        private void Start()
        {
            _camera = UnityEngine.Camera.main;

            CameraFollow();
        }

        private void CameraFollow()
        {
            _camera.GetComponent<CameraFollow>().Follow(gameObject);
        }

        private void Update()
        {
            Move();
            Rotate();
        }

        private void Move()
        {
            Vector2 input = _inputService.Axis;
            Vector3 move = new Vector3(input.x, 0, input.y);
            Vector3 worldMove = transform.TransformDirection(move);
            _characterController.Move(move * _playerSpeed * Time.deltaTime);
        }

        private void Rotate()
        {
            float mouseX = _inputService.RotateX;
            Vector3 rotation = new Vector3(0, mouseX, 0) * _rotationSpeed;
            transform.Rotate(rotation);
        }
    }
}
