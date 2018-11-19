using UnityEngine;

namespace Code.Classes.CombatController
{
    public class EnemyCombatController : BaseCombatController
    {
        public EnemyCombatController(GameObject go, int currentLife) : base(go, currentLife)
        {
        }

        public override void ReceiveHit()
        {
            base.ReceiveHit();
            BrieflyJumpUp();
        }

        private void BrieflyJumpUp()
        {
            Rigidbody2D rigidBody = GameObject.GetComponent<Rigidbody2D>();
            rigidBody.AddForce(Vector2.up * rigidBody.mass * 170);
        }
    }
}