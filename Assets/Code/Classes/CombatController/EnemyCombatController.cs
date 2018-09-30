using System.Collections;
using UnityEngine;

namespace Code.Classes.CombatController
{
    public class EnemyCombatController : ICombatController
    {
        private readonly GameObject gameObject;
        private int maxLife;

        public EnemyCombatController(GameObject go, int maxLife)
        {
            gameObject = go;
            this.maxLife = maxLife;
        }

        public void ReceiveHit()
        {
            maxLife -= 1;
            if (maxLife <= 0)
                Object.Destroy(gameObject);
        }
    }
}