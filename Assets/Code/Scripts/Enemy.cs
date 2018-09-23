using Code.Classes;
using UnityEngine;

namespace Code.Scripts
{
    public class Enemy : MonoBehaviour
    {
        private ICombatController combatController;

        private void Start()
        {
            combatController = new EnemeyCombatController(gameObject);
        }

        private void OnCollisionEnter2D(Collision2D collision)
        {
            combatController.ReceiveHit();
        }
    }
}