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
        private Vector3 _lastVelocity;

        private void Awake() => 
            _rigidbody = GetComponent<Rigidbody>();

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
                Destroy(gameObject);
                //DestroyEnemyLogic
            }

            else if (other.tag == BoundTag)
                StartCoroutine(DestroyBullet());
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
