namespace Code.Classes.MovementController
{
    public interface IMovementController
    {
        bool LookingRight { get; set; }

        bool CheckGrounded();

        void Jump();

        void Move(float horizontal);
        void SlowJump();
    }
}