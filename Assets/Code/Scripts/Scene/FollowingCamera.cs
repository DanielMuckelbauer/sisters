using Code.Classes;
using UnityEngine;

namespace Code.Scripts.Scene
{
    public class FollowingCamera : MonoBehaviour
    {
        public Transform Player1;
        public Transform Player2;

        private PlayerCameraController playerCameraController;
        public bool Following { get; set; }
        private void Awake()
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