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

        public void FixBetween(Transform player1, Transform player2)
        {
            if (player1 == null || player2 == null)
                return;
            float cameraX = (player1.position.x + player2.position.x) / 2;
            float cameraY = (player1.position.y + player2.position.y) / 2;
            camera.transform.position = new Vector3(cameraX, cameraY, camera.transform.position.z);
        }
    }
}