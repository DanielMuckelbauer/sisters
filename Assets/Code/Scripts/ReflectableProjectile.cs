using UnityEngine;

namespace Code.Scripts
{
    public class ReflectableProjectile : BaseProjectile
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
    }
}