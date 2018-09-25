using Code.Classes;
using UnityEngine;

namespace Code.Scripts
{
    public class PlayerCamera : MonoBehaviour
    {
        public Transform Player;

        private PlayerCameraController playerCameraController;

        private void Start()
        {
            playerCameraController = new PlayerCameraController(gameObject.transform);
        }

        private void FixedUpdate()
        {
            playerCameraController.FixOn(Player);
        }
    }
}