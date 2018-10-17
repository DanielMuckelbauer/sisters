namespace Code.Scripts
{
    public class HorizintallyMovingPlatform : BaseMovingPlatform
    {
        private void Awake()
        {
            StartCoroutine(ToggleMoveHorizontally(2));
        }
    }
}
