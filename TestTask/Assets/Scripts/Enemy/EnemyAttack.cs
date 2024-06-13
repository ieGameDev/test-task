using Assets.Scripts.Logic;
using UnityEngine;

namespace Assets.Scripts.Enemy
{
    public class EnemyAttack : MonoBehaviour
    {
        [SerializeField] private float _attackCooldown = 1f;
        [SerializeField] private float _bulletSpeed = 15f;
        [SerializeField] private GameObject _bulletPrefab;
        [SerializeField] private Transform _firePoint;

        [SerializeField] private float _visionRange = 20f;
        [SerializeField] private LayerMask _obstacleMask;

        private float _lastAttackTime;
        private Transform _playerTransform;

        public void Construct(GameObject player)
        {
            _playerTransform = player.transform;
        }

        private void Update()
        {
            if (_playerTransform != null && CanShoot() && CanSeePlayer())
                Attack();
        }

        public bool CanSeePlayer()
        {
            Vector3 directionToPlayer = (_playerTransform.position - _firePoint.position).normalized;
            float distanceToPlayer = Vector3.Distance(_firePoint.position, _playerTransform.position);

            if (Physics.Raycast(_firePoint.position, directionToPlayer, out RaycastHit hit, distanceToPlayer, _obstacleMask))
                return false;

            return distanceToPlayer <= _visionRange;
        }

        private bool CanShoot() =>
            Time.time >= _lastAttackTime + _attackCooldown;

        private void Attack()
        {
            transform.LookAt(_playerTransform);
            Shoot();

            _lastAttackTime = Time.time;
        }

        private void Shoot()
        {
            GameObject bullet = Instantiate(_bulletPrefab, _firePoint.position, _firePoint.rotation);
            Bullet bulletScript = bullet.GetComponent<Bullet>();
            bulletScript.Initialize(_firePoint.forward, _bulletSpeed);

            Debug.Log("EnemyShoot");
        }
    }
}
