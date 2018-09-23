using System.Collections;
using Code.Classes;
using UnityEngine;

namespace Code.Scripts
{
    public class Enemy : MonoBehaviour
    {
        public GameObject Explosion;

        private ICombatController combatController;

        private void Start()
        {
            combatController = new EnemeyCombatController(gameObject);
        }

        private void OnCollisionEnter2D(Collision2D collision)
        {
            StartCoroutine(CreateAndDestroyExplosion(collision));
            StartCoroutine(combatController.ReceiveHit());
        }

        private IEnumerator CreateAndDestroyExplosion(Collision2D collision)
        {
            GameObject explosion = Instantiate(Explosion, collision.transform.position, new Quaternion());
            yield return new WaitForSeconds(2);
            Destroy(explosion);
        }
    }
}