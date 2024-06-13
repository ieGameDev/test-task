using Assets.Scripts.Infrastructure.Services;
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

        private void Awake() => 
            _inputService = DependencyContainer.Container.Single<IInputService>();

        private void Update()
        {
            Move();
            Rotate();
        }

        private void Move()
        {
            Vector2 input = _inputService.Axis;
            Vector3 move = new Vector3(input.x, 0, input.y);
            move += Physics.gravity;

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
