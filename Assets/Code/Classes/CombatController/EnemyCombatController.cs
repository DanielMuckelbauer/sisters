using System.Collections.Generic;
using System.Linq;
using System.Threading;
using UnityEngine;

namespace Code.Classes.CombatController
{
    public class EnemyCombatController : BaseCombatController
    {
        private readonly List<SpriteRenderer> allRenderers;
        private readonly Collider2D collider;

        public EnemyCombatController(GameObject go, int currentLife) : base(go, currentLife)
        {
            allRenderers = GameObject.GetComponentsInChildren<SpriteRenderer>().ToList();
            collider = GameObject.GetComponent<Collider2D>();
        }

        public override void ReceiveHit(Collider2D col)
        {
            Thread thread = new Thread(BrieflyTurnInvincibleAndBlink);
            thread.Start();
            base.ReceiveHit(col);
        }

        private void BrieflyTurnInvincibleAndBlink()
        {
            collider.enabled = false;
            Blink(10);
            collider.enabled = true;
        }

        private void Blink(int times)
        {
            for (int i = 0; i < times; i++)
            {
                float opacity = i % 2 == 0 ? 0.5f : 1;
                allRenderers.ForEach(r => r.color = new Color(1f, 1f, 1f, opacity));
                Thread.Sleep(100);
            }
        }
    }
}