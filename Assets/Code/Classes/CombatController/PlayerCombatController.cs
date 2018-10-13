using System.Collections.Generic;
using Code.Scripts.Entity;
using UnityEngine;

namespace Code.Classes.CombatController
{
    public class PlayerCombatController : BaseCombatController
    {
        private readonly List<GameObject> hearts;
        private int heartCounter;

        public PlayerCombatController(GameObject go, int maxLife) : base(go, maxLife)
        {
            hearts = go.GetComponent<Player>().Hearts;
            heartCounter = hearts.Count - 1;
        }

        public override void ReceiveHit(Collision2D collision)
        {
            base.ReceiveHit(collision);
            PushBackward(collision);
            RemoveHeart();
        }

        private void PushBackward(Collision2D collision)
        {
            Debug.Log("Collision triffered");
            const int magnitude = 1000;
            GameObject.GetComponent<Rigidbody2D>().AddForce(Vector2.left * magnitude);
        }

        private void RemoveHeart()
        {
            GameObject currentHeart = hearts[heartCounter--];
            currentHeart.GetComponent<SpriteRenderer>().enabled = false;
        }
    }
}