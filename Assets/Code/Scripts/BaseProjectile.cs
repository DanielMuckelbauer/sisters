using Code.Scripts.Entity;
using System.Collections;
using UnityEngine;

namespace Code.Scripts
{
    public abstract class BaseProjectile : MonoBehaviour
    {
        //public float ShootForce = 2f;
        //public float RotationForce = 10;
        [SerializeField] private float shootForce = 2f;
        [SerializeField] private float rotationForce = 10;
        private Rigidbody2D rigidBody;

        protected virtual void Awake()
        {
            rigidBody = GetComponent<Rigidbody2D>();
            StartCoroutine(DelayedDestroy());
        }

        public void Shoot(Vector3 target)
        {
            Vector2 force = (target - gameObject.transform.position).normalized * shootForce;
            rigidBody.AddForce(force);
            rigidBody.AddTorque(rotationForce, ForceMode2D.Impulse);
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

        public virtual void HitByWeapon()
        {
            Destroy(gameObject);
        }
    }
}