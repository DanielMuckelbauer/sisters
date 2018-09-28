namespace Code.Classes
{
    public interface IMovementController
    {
        void Move(float horizontal);
        void Jump();
        bool LookingRight { get; set; }
    }
}
