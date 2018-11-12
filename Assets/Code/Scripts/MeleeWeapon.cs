using UnityEngine;

namespace Code.Scripts
{
    public class MeleeWeapon : MonoBehaviour
    {
        private void OnTriggerEnter2D(Collider2D other)
        {
            BaseProjectile incomingProjectile = other.GetComponent<BaseProjectile>();
            if (incomingProjectile == null)
                return;
            incomingProjectile.HitByWeapon();
        }
    }
}