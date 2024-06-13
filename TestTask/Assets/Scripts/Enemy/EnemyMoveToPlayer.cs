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
        private EnemyAttack _enemyAttack;

        public void Construct(GameObject player)
        {
            _playerTransform = player.transform;
        }

        private void Start() => 
            _enemyAttack = GetComponent<EnemyAttack>();

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
    }
}
