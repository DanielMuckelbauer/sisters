using UnityEngine;

namespace Code.Classes.CombatController
{
    public abstract class BaseCombatController : ICombatController
    {
        protected readonly GameObject GameObject;
        protected int MaxLife;

        protected BaseCombatController(GameObject go, int maxLife)
        {
            GameObject = go;
            MaxLife = maxLife;
        }

        public virtual void ReceiveHit(Collision2D collision)
        {
            MaxLife -= 1;
            if (MaxLife <= 0)
                Object.Destroy(GameObject);
        }
    }
}