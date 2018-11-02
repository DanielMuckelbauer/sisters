using System.Collections.Generic;
using System.Linq;
using System.Threading;
using UnityEngine;

namespace Code.Classes.CombatController
{
    public class EnemyCombatController : BaseCombatController
    {
        public EnemyCombatController(GameObject go, int currentLife) : base(go, currentLife)
        {
        }
    }
}