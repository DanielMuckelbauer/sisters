using System.Collections;
using System.Collections.Generic;
using Code.Scripts;
using UnityEngine;

namespace Code.Classes
{
    public class EnemeyCombatController : ICombatController
    {
        private readonly GameObject gameObject;

        public EnemeyCombatController(GameObject go)
        {
            this.gameObject = go;
        }

        public void ReceiveHit()
        {
            Object.Destroy(gameObject);
        }
    }
}
