using System.Collections;
using UnityEngine;

namespace Code.Classes.CombatController
{
    public interface ICombatController
    {
        void ReceiveHit(Collision2D collision);
    }
}
