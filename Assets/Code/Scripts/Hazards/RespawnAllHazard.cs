using Code.Scripts.Scene;
using UnityEngine;

namespace Code.Scripts.Hazards
{
    public class RespawnAllHazard : BaseHazard
    {
        private void OnCollisionStay2D(Collision2D other)
        {
            if (other.gameObject.tag.Contains("Player"))
                PlayerController.RespawnBoth();
        }
    }
}