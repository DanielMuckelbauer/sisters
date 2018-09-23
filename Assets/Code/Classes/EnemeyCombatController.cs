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
            gameObject = go;
        }

        public IEnumerator ReceiveHit()
        {
            yield return new WaitForSeconds(0.5f);
            Object.Destroy(gameObject);
        }
    }
}
