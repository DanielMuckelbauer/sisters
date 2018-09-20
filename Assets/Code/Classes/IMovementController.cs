namespace Code.Classes
{
    public interface IMovementController
    {
        void Move(float horizontal);
        bool LookingRight { get; set; }
        void Jump();
    }
}
