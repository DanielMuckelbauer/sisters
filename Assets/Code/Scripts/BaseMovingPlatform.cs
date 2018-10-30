using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Code.Scripts
{
    public abstract class BaseMovingPlatform : MonoBehaviour
    {
        public float Delay;
        public float MoveTime;
        public float Pause;
        public float Step;

        protected Vector3 Direction;

        private Dictionary<GameObject, GameObject> entitiesStandingOnPlatformAndTheirParent;

        protected abstract void CalculateDirection();

        protected Vector3 ChangeBetweenTwoDirections(Vector3 first, Vector3 second)
        {
            return Direction == first ? second : first;
        }

        protected virtual void Start()
        {
            StartCoroutine(DirectionChangeLoop());
            entitiesStandingOnPlatformAndTheirParent = new Dictionary<GameObject, GameObject>();
        }

        private IEnumerator DirectionChangeLoop()
        {
            yield return new WaitForSeconds(Delay);
            while (true)
            {
                yield return new WaitForSeconds(MoveTime);
                yield return new WaitForSeconds(Pause);
                CalculateDirection();
            }
        }

        private void MakePlatformParentOf(Collider2D col)
        {
            if (col.transform.parent == null)
                return;
            entitiesStandingOnPlatformAndTheirParent.Add(col.gameObject, col.transform.parent.gameObject);
            col.gameObject.transform.parent = transform;
        }

        private void Move()
        {
            transform.position += Direction * Step * Time.deltaTime;
        }

        private void OnTriggerExit2D(Collider2D col)
        {
            if (!entitiesStandingOnPlatformAndTheirParent.ContainsKey(col.gameObject))
                return;
            ReturnToOriginalParent(col);
        }

        private void OnTriggerStay2D(Collider2D col)
        {
            if (entitiesStandingOnPlatformAndTheirParent.ContainsKey(col.gameObject))
                return;
            MakePlatformParentOf(col);
        }

        private void ReturnToOriginalParent(Collider2D col)
        {
            col.gameObject.transform.parent = entitiesStandingOnPlatformAndTheirParent[col.gameObject].transform;
            entitiesStandingOnPlatformAndTheirParent.Remove(col.gameObject);
        }

        private void Update()
        {
            Move();
        }
    }
}