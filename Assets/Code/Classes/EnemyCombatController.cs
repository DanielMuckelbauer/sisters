using System.Collections;
using UnityEngine;

namespace Code.Classes
{
    public class EnemyCombatController : ICombatController
    {
        private readonly GameObject gameObject;

        public EnemyCombatController(GameObject go)
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