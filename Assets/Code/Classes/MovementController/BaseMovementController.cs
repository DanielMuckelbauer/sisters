using UnityEngine;

namespace Code.Classes.MovementController
{
    public abstract class BaseMovementController : IMovementController
    {
        protected const float JumpForce = 470;
        protected readonly Animator Animator;
        protected readonly int LayerMask;
        protected readonly Rigidbody2D RigidBody;
        protected readonly Transform Transform;
        protected float Speed = 4;
        public bool LookingRight { get; set; }

        protected BaseMovementController(GameObject gameObject)
        {
            RigidBody = gameObject.GetComponent<Rigidbody2D>();
            Transform = gameObject.GetComponent<Transform>();
            Animator = gameObject.GetComponent<Animator>();
            LayerMask = 1 << UnityEngine.LayerMask.NameToLayer("Ground");
            LookingRight = true;
        }

        public virtual bool CheckGrounded()
        {
            return true;
        }

        public abstract void Jump();

        public abstract void Move(float horizontal);
    }
}