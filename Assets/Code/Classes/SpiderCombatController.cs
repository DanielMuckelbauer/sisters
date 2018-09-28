using System.Collections;
using UnityEngine;

namespace Code.Classes
{
    public class SpiderCombatController : ICombatController
    {
        private readonly GameObject gameObject;

        public SpiderCombatController(GameObject go)
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