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
            rigidBody.velocity = rigidBody.velocity * -1;
            tag = "Weapon";
            gameObject.layer = LayerMask.NameToLayer("ReflectedProjectile");
        }
    }
}