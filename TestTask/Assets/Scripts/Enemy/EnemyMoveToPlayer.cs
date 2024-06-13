using Assets.Scripts.Infrastructure.Factory;
using Assets.Scripts.Infrastructure.Services;
using UnityEngine;
using UnityEngine.AI;

namespace Assets.Scripts.Enemy
{
    [RequireComponent(typeof(NavMeshAgent))]
    public class EnemyMoveToPlayer : MonoBehaviour
    {
        private const float ShootDistance = 5f;

        [SerializeField] private NavMeshAgent _agent;

        private Transform _playerTransform;
        private IGameFactory _gameFactory;
        private EnemyAttack _enemyAttack;

        private void Start()
        {
            _gameFactory = DependencyContainer.Container.Single<IGameFactory>();
            _enemyAttack = GetComponent<EnemyAttack>();

            if (_gameFactory.PlayerGameObject != null)
                InitializePlayerTransform();
            else
                _gameFactory.PlayerCreated += PlayerCreated;
        }

        private void Update()
        {
            if (_playerTransform != null && PlayerNotReached())
            {
                if (_enemyAttack.CanSeePlayer())                
                    _agent.isStopped = true;                
                else                
                    MoveToPositionWithVisibility();                
            }
        }

        private void MoveToPositionWithVisibility()
        {
            Vector3 directionToPlayer = (_playerTransform.position - transform.position).normalized;
            NavMeshHit hit;

            if (NavMesh.SamplePosition(transform.position + directionToPlayer * ShootDistance, out hit, ShootDistance, NavMesh.AllAreas))
            {
                _agent.isStopped = false;
                _agent.SetDestination(hit.position);
            }
        }

        private bool PlayerNotReached() =>
            Vector3.Distance(_agent.transform.position, _playerTransform.position) >= ShootDistance;

        private void PlayerCreated() =>
            InitializePlayerTransform();

        private void InitializePlayerTransform() =>
            _playerTransform = _gameFactory.PlayerGameObject.transform;
    }
}
