using UnityEngine;

namespace Code.Scripts.Level
{
    public class UnBouncyReflectableProjectile : BaseProjectile
    {
        public override void HitByWeapon()
        {
            Rigidbody2D rigidBody = GetComponent<Rigidbody2D>();
            if (rigidBody == null)
                return;
            rigidBody.velocity = Vector3.Reflect(rigidBody.velocity, Vector3.right);
            tag = "Weapon";
            gameObject.layer = LayerMask.NameToLayer("ReflectedProjectile");
        }

        protected override void OnCollisionEnter2D(Collision2D collision)
        {
            int groundLayer = LayerMask.NameToLayer("Ground");
            if (groundLayer == collision.gameObject.layer)
                Destroy(gameObject);
            base.OnCollisionEnter2D(collision);
        }
    }
}