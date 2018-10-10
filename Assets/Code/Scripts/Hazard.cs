using Code.Scripts.Entity;
using UnityEngine;

namespace Code.Scripts
{
    public class Hazard : MonoBehaviour {
        private void OnCollisionEnter2D(Collision2D other)
        {
            BaseEntity baseEntity = other.gameObject.GetComponent<BaseEntity>();
            baseEntity?.Die();
        }
    }
}
