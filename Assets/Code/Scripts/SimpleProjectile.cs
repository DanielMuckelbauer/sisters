using UnityEngine;

namespace Code.Scripts
{
    public class SimpleProjectile : BaseProjectile
    {
        protected override void Awake()
        {
            base.Awake();
            ShootForce = 2;
        }

        protected override void OnCollisionEnter2D(Collision2D collision)
        {
            base.OnCollisionEnter2D(collision);
            Destroy(gameObject);
        }
    }
}