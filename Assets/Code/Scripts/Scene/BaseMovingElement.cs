using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Code.Scripts.Scene
{
    public abstract class BaseMovingElement : MonoBehaviour
    {
        protected Vector3 Direction;
        [SerializeField] private float delay;
        private Dictionary<GameObject, GameObject> entitiesStandingOnPlatformAndTheirParent;
        [SerializeField] private float moveTime;
        private bool movingEnabled;
        [SerializeField] private float pause;
        [SerializeField] private float step;

        protected abstract void CalculateDirection();

        protected Vector3 ChangeBetweenTwoDirections(Vector3 first, Vector3 second)
        {
            return Direction == first ? second : first;
        }

        protected virtual void Start()
        {
            StartCoroutine(EnableMovingAfterDelay());
            entitiesStandingOnPlatformAndTheirParent = new Dictionary<GameObject, GameObject>();
        }

        private IEnumerator EnableMovingAfterDelay()
        {
            yield return new WaitForSeconds(delay);
            StartCoroutine(DirectionChangeLoop());
            movingEnabled = true;
        }

        private IEnumerator DirectionChangeLoop()
        {
            while (true)
            {
                yield return new WaitForSeconds(moveTime);
                movingEnabled = false;
                yield return new WaitForSeconds(pause);
                movingEnabled = true;
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
            if (movingEnabled)
                transform.position += Direction * step * Time.deltaTime;
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