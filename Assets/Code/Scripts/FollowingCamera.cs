using Code.Classes;
using Code.Scripts.Entity;
using UnityEngine;

namespace Code.Scripts
{
    public class FollowingCamera : MonoBehaviour
    {
        public Transform Player1;
        public Transform Player2;

        public bool Following { get; set; }
        private PlayerCameraController playerCameraController;

        private void Start()
        {
            Following = true;
            playerCameraController = new PlayerCameraController(gameObject.transform);
        }

        private void Update()
        {
            if (Following)
                playerCameraController.FixBetween(Player1, Player2);
        }
    }
}