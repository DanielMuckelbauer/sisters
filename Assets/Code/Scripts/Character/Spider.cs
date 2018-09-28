using System.Linq;
using Code.Classes;
using Code.Classes.MovementController;
using UnityEngine;

namespace Code.Scripts.Character
{
    public class Spider : BaseCharacter
    {
        public Animator ExplosionAnimator;

        private void Start()
        {
            CombatController = new SpiderCombatController(gameObject);
            MovementController = new SpiderMovementController(gameObject);
        }

        private void OnCollisionEnter2D(Collision2D collision)
        {
            if (!collision.gameObject.tag.Contains("Weapon"))
                return;
            GetComponentsInChildren<SpriteRenderer>().ToList().ForEach(r => r.enabled = true);
            ExplosionAnimator.SetTrigger("Explosion");
            StartCoroutine(CombatController.ReceiveHit());
        }
    }
}