using Assets.Scripts.Infrastructure.Services.Input;
using Assets.Scripts.Infrastructure.Services;
using UnityEngine;
using Assets.Scripts.Logic;

namespace Assets.Scripts.Player
{
    public class PlayerAttack : MonoBehaviour
    {
        [SerializeField] private float _attackCooldown = 1f;
        [SerializeField] private float _bulletSpeed = 15f;
        [SerializeField] private GameObject _bulletPrefab;
        [SerializeField] private Transform _firePoint;

        private float _lastAttackTime;
        private IInputService _inputService;

        private void Awake() =>
            _inputService = DependencyContainer.Container.Single<IInputService>();

        private void Update() => 
            Attack();

        private void Attack()
        {
            if (CanShoot() && _inputService.AttackButtonPressed)
            {
                Shoot();

                _lastAttackTime = Time.time;
            }
        }

        private bool CanShoot() =>
            Time.time >= _lastAttackTime + _attackCooldown;

        private void Shoot()
        {
            GameObject bullet = Instantiate(_bulletPrefab, _firePoint.position, _firePoint.rotation);
            Bullet bulletScript = bullet.GetComponent<Bullet>();
            bulletScript.Initialize(_firePoint.forward, _bulletSpeed);

            Debug.Log("Shoot");
        }
    }
}
