using UnityEngine;

namespace Code.Classes.MovementController
{
    public class SpiderMovementController : BaseMovementController
    {
        public SpiderMovementController(GameObject gameObject, Transform groundCheck) : base(gameObject, groundCheck)
        {
        }

        public override void Move(float horizontal)
        {
            throw new System.NotImplementedException();
        }

        public override void Jump()
        {
            throw new System.NotImplementedException();
        }
    }
}
