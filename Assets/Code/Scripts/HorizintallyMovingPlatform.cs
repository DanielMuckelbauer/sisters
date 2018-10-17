using System.Collections.Generic;
using UnityEngine;

namespace Code.Scripts
{
    public class HorizintallyMovingPlatform : BaseMovingPlatform
    {
        private HashSet<GameObject> childList;

        private void Awake()
        {
            childList = new HashSet<GameObject>();
        }

        private void OnTriggerStay2D(Collider2D col)
        {
            if (childList.Contains(col.gameObject))
                return;
            childList.Add(col.gameObject);
            col.gameObject.transform.parent = transform;
        }

        private void OnTriggerExit2D(Collider2D col)
        {
            childList.Remove(col.gameObject);
            col.gameObject.transform.parent = null;
        }
    }
}