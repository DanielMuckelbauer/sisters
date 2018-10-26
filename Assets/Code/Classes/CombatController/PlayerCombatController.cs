using Code.Scripts;
using Code.Scripts.Entity;
using System.Collections.Generic;
using UnityEngine;

namespace Code.Classes.CombatController
{
    public class PlayerCombatController : BaseCombatController
    {
        private readonly List<GameObject> hearts;
        private int heartIndex;

        public PlayerCombatController(GameObject go, int maxLife) : base(go, maxLife)
        {
            hearts = go.GetComponent<Player>().Hearts;
            heartIndex = hearts.Count - 1;
            Healer.OnHealingConsumed += RefillHearts;
        }

        public override void ReceiveHit(Collision2D collision)
        {
            base.ReceiveHit(collision);
            PushBackward(collision);
            RemoveHeart();
        }

        private void PushBackward(Collision2D collision)
        {
            const int magnitude = 1000;
            GameObject.GetComponent<Rigidbody2D>().AddForce(Vector2.left * magnitude);
        }

        private void RefillHearts()
        {
            CurrentLife = MaxLife;
            heartIndex = 4;
            Debug.Log(hearts.Count);
            hearts.ForEach(h => h.GetComponent<SpriteRenderer>().enabled = true);
        }

        private void RemoveHeart()
        {
            GameObject currentHeart = hearts[heartIndex--];
            currentHeart.GetComponent<SpriteRenderer>().enabled = false;
        }
    }
}