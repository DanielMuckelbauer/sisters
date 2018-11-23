using UnityEngine;

namespace Code.Scripts.Level
{
    public class SimpleProjectile : BaseProjectile
    {
        protected override void OnCollisionEnter2D(Collision2D collision)
        {
            base.OnCollisionEnter2D(collision);
            Destroy(gameObject);
        }
    }
}