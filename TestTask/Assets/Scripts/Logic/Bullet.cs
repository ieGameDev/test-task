using Assets.Scripts.Enemy;
using Assets.Scripts.Infrastructure.Services;
using Assets.Scripts.Infrastructure.Services.SaveLoad;
using Assets.Scripts.Player;
using System.Collections;
using UnityEngine;

namespace Assets.Scripts.Logic
{
    [RequireComponent(typeof(Rigidbody))]
    public abstract class Bullet : MonoBehaviour
    {
        private const string BoundTag = "Bound";

        protected abstract string TargetTag { get; }

        private Rigidbody _rigidbody;
        private ISaveLoadService _saveLoadService;
        private Vector3 _lastVelocity;

        private void Awake()
        {
            _rigidbody = GetComponent<Rigidbody>();
            _saveLoadService = DependencyContainer.Container.Single<ISaveLoadService>();
        }

        private void Update() =>
            _lastVelocity = _rigidbody.velocity;

        public void Initialize(Vector3 direction, float speed) =>
            _rigidbody.velocity = direction * speed;

        private void OnCollisionEnter(Collision collision) =>
            RicochetLogic(collision);

        private void OnTriggerEnter(Collider other)
        {
            if (other.tag == TargetTag)
            {
                if (TargetTag == "Player")
                {
                    PointToEnemy();

                    _saveLoadService.SaveProgress();
                    Destroy(gameObject);
                }

                else if (TargetTag == "Enemy")
                {
                    PointToPlayer();

                    _saveLoadService.SaveProgress();
                    Destroy(gameObject);
                }
            }

            else if (other.tag == BoundTag)
                StartCoroutine(DestroyBullet());
        }

        public void PointToPlayer()
        {
            IScoreCounter score = FindObjectOfType<PlayerScoreCounter>();
            if (score != null)
                score.IncreaseScore(1);
        }

        public void PointToEnemy()
        {
            IScoreCounter score = FindObjectOfType<EnemyScoreCounter>();
            if (score != null)
                score.IncreaseScore(1);
        }

        private void RicochetLogic(Collision collision)
        {
            float speed = _lastVelocity.magnitude;
            Vector3 direction = Vector3.Reflect(_lastVelocity.normalized, collision.contacts[0].normal);
            _rigidbody.velocity = direction * Mathf.Max(speed, 0f);
        }

        private IEnumerator DestroyBullet()
        {
            yield return new WaitForSeconds(1);
            Destroy(gameObject);
        }
    }
}
