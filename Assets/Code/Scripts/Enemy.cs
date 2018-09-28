using Code.Classes;
using System.Linq;
using UnityEngine;

namespace Code.Scripts
{
    public class Enemy : MonoBehaviour
    {
        public Animator ExplosionAnimator;

        private ICombatController combatController;

        private void Start()
        {
            combatController = new EnemeyCombatController(gameObject);
        }

        private void OnCollisionEnter2D(Collision2D collision)
        {
            if (!collision.gameObject.tag.Contains("Weapon"))
                return;
            GetComponentsInChildren<SpriteRenderer>().ToList().ForEach(r => r.enabled = true);
            ExplosionAnimator.SetTrigger("Explosion");
            StartCoroutine(combatController.ReceiveHit());
        }
    }
}