using System.Collections;
using UnityEngine;

namespace Code.Scripts
{
    public abstract class BaseMovingPlatform : MonoBehaviour
    {
        public Rigidbody2D RigidBody;

        protected IEnumerator ToggleMoveHorizontally(int moveTime, int pauseTime = 0)
        {
            Vector3 direction = Vector3.right;
            while (true)
            {
                SetVelocityInDirection(direction);
                yield return new WaitForSeconds(moveTime);
                yield return new WaitForSeconds(pauseTime);
                direction = direction == Vector3.right ? Vector3.left : Vector3.right;
            }
        }

        private void SetVelocityInDirection(Vector3 direction, float velocity = 3)
        {
            RigidBody.velocity = direction * velocity;
        }
    }
}