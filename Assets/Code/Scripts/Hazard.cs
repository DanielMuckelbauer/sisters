using Code.Scripts.SceneController;
using UnityEngine;

namespace Code.Scripts
{
    public class Hazard : MonoBehaviour
    {
        private void OnCollisionStay2D(Collision2D other)
        {
            if (other.gameObject.tag.Contains("Player"))
                BaseSceneController.InvokeRespawnBoth();
        }
    }
}