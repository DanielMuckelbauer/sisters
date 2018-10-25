using UnityEngine;

namespace Code.Classes.CombatController
{
    public class EnemyCombatController : BaseCombatController
    {
        public EnemyCombatController(GameObject go, int currentLife) : base(go, currentLife)
        {
        }

        public override void ReceiveHit(Collision2D collision)
        {
            JumpUpAndDown();
            base.ReceiveHit(collision);
        }

        private void JumpUpAndDown()
        {
            const int magnitude = 100;
            GameObject.GetComponent<Rigidbody2D>().AddForce(Vector2.up * magnitude);
        }
    }
}