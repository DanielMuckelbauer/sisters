using System.Collections;
using UnityEngine;

namespace Code.Scripts
{
    public abstract class BaseMovingPlatform : MonoBehaviour
    {
        public float MoveTime;
        public float Pause;
        public float Step;

        private Vector3 direction;

        private void Start()
        {
            StartCoroutine(ChangeDirection());
            direction = Vector3.right;
        }

        private IEnumerator ChangeDirection()
        {
            while (true)
            {
                yield return new WaitForSeconds(MoveTime);
                yield return new WaitForSeconds(Pause);
                direction = direction == Vector3.right ? Vector3.left : Vector3.right;
            }
        }

        private void Update()
        {
            Move();
        }

        private void Move()
        {
            transform.position += direction * Step;
        }
    }
}