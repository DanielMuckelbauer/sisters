using UnityEngine;

namespace Code.Classes
{
    public abstract class BaseMovementController : IMovementController
    {
        public bool LookingRight { get; set; }

        protected readonly Transform Transform;
        protected readonly Rigidbody2D RigidBody;
        protected readonly Transform GroundCheck;
        protected readonly int LayerMask;


        protected BaseMovementController(GameObject gameObject, Transform groundCheck)
        {
            RigidBody = gameObject.GetComponent<Rigidbody2D>();
            Transform = gameObject.GetComponent<Transform>();
            LayerMask = 1 << UnityEngine.LayerMask.NameToLayer("Ground");
            GroundCheck = groundCheck;
            LookingRight = true;
        }

        public abstract void Move(float horizontal);
        public abstract void Jump();
    }
}
