namespace Code.Scripts.Entity
{
    public class Spider : BasePatrollingEnemy
    {
        protected override void Start()
        {
            base.Start();
            StartCoroutine(JumpRandomly());
        }
    }
}