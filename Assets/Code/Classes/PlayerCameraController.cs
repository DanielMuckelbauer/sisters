using Code.Scripts;
using UnityEngine;

namespace Code.Classes
{
    public class PlayerCameraController
    {
        private readonly Transform camera;

        public PlayerCameraController(Transform camera)
        {
            this.camera = camera;
        }

        public void FixOn(Transform player)
        {
            if (player == null)
                return;
            camera.transform.position = new Vector3(player.position.x, player.position.y,
                camera.transform.position.z);
        }
    }
}