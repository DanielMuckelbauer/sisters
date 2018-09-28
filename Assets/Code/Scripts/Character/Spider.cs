using Code.Classes;
using System.Linq;
using Code.Classes.MovementController;
using UnityEngine;

namespace Code.Scripts
{
    public class Spider : MonoBehaviour
    {
        public Animator ExplosionAnimator;

        private ICombatController combatController;
        private IMovementController movementController;

        private void Start()
        {
            combatController = new SpiderCombatController(gameObject);
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