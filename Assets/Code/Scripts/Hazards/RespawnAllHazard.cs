using Code.Scripts.Hazards;
using UnityEngine;

namespace Code.Scripts
{
    public class RespawnAllHazard : BaseHazard
    {
        private void OnCollisionStay2D(Collision2D other)
        {
            if (other.gameObject.tag.Contains("Player"))
                SceneController.RespawnBoth();
        }
    }
}