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

        private HashSet<GameObject> entitiesStandingOnPlatform;


        protected abstract void CalculateDirection();

        protected Vector3 ChangeBetweenTwoDirections(Vector3 first, Vector3 second)
        {
            return Direction == first ? second : first;
        }

        protected virtual void Start()
        {
            StartCoroutine(DirectionChangeLoop());
            entitiesStandingOnPlatform = new HashSet<GameObject>();
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

        private void Move()
        {
            transform.position += Direction * Step * Time.deltaTime;
        }

        private void OnTriggerExit2D(Collider2D col)
        {
            entitiesStandingOnPlatform.Remove(col.gameObject);
            col.gameObject.transform.parent = null;
        }

        private void OnTriggerStay2D(Collider2D col)
        {
            if (entitiesStandingOnPlatform.Contains(col.gameObject))
                return;
            entitiesStandingOnPlatform.Add(col.gameObject);
            col.gameObject.transform.parent = transform;
        }

        private void Update()
        {
            Move();
        }
    }
}