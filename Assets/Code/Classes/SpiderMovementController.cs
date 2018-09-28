namespace Code.Classes
{
    public class SpiderMovementController : IMovementController
    {
        public bool LookingRight { get; set; }
        public bool Grounded { get; set; }

        public void Move(float horizontal)
        {
            throw new System.NotImplementedException();
        }

        public void Jump()
        {
            throw new System.NotImplementedException();
        }
    }
}
