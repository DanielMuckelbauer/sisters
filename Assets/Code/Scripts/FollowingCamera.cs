using Code.Classes;
using Code.Scripts.Entity;
using UnityEngine;

namespace Code.Scripts
{
    public class FollowingCamera : MonoBehaviour
    {
        public Transform Player1;
        public Transform Player2;

        private PlayerCameraController playerCameraController;

        private void Start()
        {
            playerCameraController = new PlayerCameraController(gameObject.transform);
        }

        private void Update()
        {
            playerCameraController.FixBetween(Player1, Player2);
        }
    }
}