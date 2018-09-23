using System.Collections;
using Code.Classes;
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
            ExplosionAnimator.SetTrigger("Explosion");
            StartCoroutine(combatController.ReceiveHit());
        }
    }
}