using System.Collections;

namespace Code.Scripts.Entity
{
    public abstract class BaseEnemy : BaseEntity
    {
        protected abstract IEnumerator Patrol();

        public void StartPatroling()
        {
            StartCoroutine(Patrol());
        }
    }
}