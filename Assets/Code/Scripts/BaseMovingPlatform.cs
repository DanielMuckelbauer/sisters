using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Code.Scripts
{
    public abstract class BaseMovingPlatform : MonoBehaviour
    {
        public float MoveTime;
        public float Pause;
        public float Step;
        public float Delay;

        protected Vector3 Direction;
        private HashSet<GameObject> childList;


        protected virtual void Start()
        {
            StartCoroutine(DirectionChangeLoop());
            childList = new HashSet<GameObject>();
        }

        private void Update()
        {
            Move();
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

        protected abstract void CalculateDirection();

        private void Move()
        {
            transform.position += Direction * Step * Time.deltaTime;
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

        protected Vector3 ChangeBetweenTwoDirections(Vector3 first, Vector3 second)
        {
            return Direction == first ? second : first;
        }
    }
}