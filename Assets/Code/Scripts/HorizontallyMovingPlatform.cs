using UnityEngine;

namespace Code.Scripts
{
    public class HorizontallyMovingPlatform : BaseMovingPlatform
    {
        protected override void Start()
        {
            Direction = Vector3.right;
            base.Start();
        }

        protected override void CalculateDirection()
        {
            Direction = ChangeBetweenTwoDirections(Vector3.right, Vector3.left);
        }
    }
}