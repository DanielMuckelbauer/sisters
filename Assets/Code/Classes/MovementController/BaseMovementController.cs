using UnityEngine;

namespace Code.Classes.MovementController
{
    public abstract class BaseMovementController : IMovementController
    {
        public bool LookingRight { get; set; }

        protected readonly Transform Transform;
        protected readonly Rigidbody2D RigidBody;
        protected readonly int LayerMask;


        protected BaseMovementController(GameObject gameObject)
        {
            RigidBody = gameObject.GetComponent<Rigidbody2D>();
            Transform = gameObject.GetComponent<Transform>();
            LayerMask = 1 << UnityEngine.LayerMask.NameToLayer("Ground");
            LookingRight = true;
        }

        public abstract void Move(float horizontal);
        public abstract void Jump();
    }
}
