using Assets.Scripts.Infrastructure.Factory;
using UnityEngine;
using UnityEngine.AI;

namespace Assets.Scripts.Enemy
{
    [RequireComponent(typeof(NavMeshAgent))]
    public class EnemyMoveToPlayer : MonoBehaviour
    {
        private const float MinimalDistance = 5f;

       [SerializeField] private NavMeshAgent _agent;

        private Transform _playerTransform;

        public void Construct(GameObject player) =>
            _playerTransform = player.transform;    

        private void Update()
        {
            if (Initialized() && PlayerNotReached())
                _agent.destination = _playerTransform.position;
        }

        private bool Initialized() =>
            _playerTransform != null;

        private bool PlayerNotReached() =>
            Vector3.Distance(_agent.transform.position, _playerTransform.position) >= MinimalDistance;

    }
}
