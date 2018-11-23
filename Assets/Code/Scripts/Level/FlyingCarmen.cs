using UnityEngine;

namespace Code.Scripts.Level
{
    public class FlyingCarmen : HorizontallyMovingElement
    {
        protected override void Start()
        {
            base.Start();
            Direction = Vector3.left;
        }

        protected override void CalculateDirection()
        {
            base.CalculateDirection();
            transform.rotation *= Quaternion.Euler(0, 0, 180);
        }
    }
}