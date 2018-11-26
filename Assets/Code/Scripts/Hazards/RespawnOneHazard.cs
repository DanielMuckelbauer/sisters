using UnityEngine;

namespace Code.Scripts.Hazards
{
    public class RespawnOneHazard : BaseHazard
    {
        private void OnCollisionStay2D(Collision2D other)
        {
            if (other.gameObject.tag.Contains("Player"))
                PlayerRepositioningController.RespawnOne(other.gameObject);
        }
    }
}