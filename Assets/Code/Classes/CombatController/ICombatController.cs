using UnityEngine;

namespace Code.Classes.CombatController
{
    public interface ICombatController
    {
        void ReceiveHit(Collider2D col);
    }
}