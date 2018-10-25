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
            camera.transform.position = new Vector3(player1.transform.position.x, player1.transform.position.y,
                camera.transform.position.z);
            //float cameraX = (player1.position.x + player2.position.x) / 2;
            //float cameraY = (player1.position.y + player2.position.y) / 2;
            //camera.transform.position = new Vector3(cameraX, cameraY, camera.transform.position.z);
        }
    }
}