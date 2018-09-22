using UnityEngine;

namespace Code.Scripts
{
    public class MeleeWeapon : MonoBehaviour
    {
        private Collider collider;

        private void Start()
        {
            collider = GetComponent<Collider>();
        }

        
    }
}