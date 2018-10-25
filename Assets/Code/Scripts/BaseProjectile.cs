using Code.Scripts.Entity;
using System.Collections;
using UnityEngine;

namespace Code.Scripts
{
    public abstract class BaseProjectile : MonoBehaviour
    {
        protected float ShootForce = 2f;
        private const float RotationForce = 10;
        private Rigidbody2D rigidBody;

        protected virtual void Awake()
        {
            rigidBody = GetComponent<Rigidbody2D>();
            StartCoroutine(DelayedDestroy());
        }

        public void Shoot(Vector3 target)
        {
            Vector2 force = (target - gameObject.transform.position).normalized * ShootForce;
            rigidBody.AddForce(force);
            rigidBody.AddTorque(RotationForce, ForceMode2D.Impulse);
        }

        protected virtual void OnCollisionEnter2D(Collision2D collision)
        {
            BaseEntity baseEntity = collision.gameObject.GetComponent<BaseEntity>();
            if (baseEntity != null)
                baseEntity.HitByProjectile(gameObject);
        }

        private IEnumerator DelayedDestroy()
        {
            yield return new WaitForSeconds(7);
            Destroy(gameObject);
        }
    }
}