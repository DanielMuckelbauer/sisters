using UnityEngine;

namespace Code.Classes.CombatController
{
    public class PlayerCombatController : BaseCombatController
    {
        public PlayerCombatController(GameObject go, int maxLife) : base(go, maxLife)
        {
        }

        public override void ReceiveHit(Collision2D collision)
        {
            base.ReceiveHit(collision);
            Vector3 force = (GameObject.transform.position - collision.transform.position
                             + Vector3.up).normalized * 500;
            GameObject.GetComponent<Rigidbody2D>().AddForce(force, ForceMode2D.Force);
        }
    }
}