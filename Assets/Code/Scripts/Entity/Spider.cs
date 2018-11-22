namespace Code.Scripts.Entity
{
    public class Spider : BasePatrolingEnemy
    {
        protected override void Start()
        {
            base.Start();
            StartCoroutine(JumpRandomly());
        }
    }
}