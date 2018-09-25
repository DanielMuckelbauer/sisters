using UnityEngine;

namespace Code.Scripts
{
    public class Camera : MonoBehaviour
    {
        public Transform Player;

        private void FixedUpdate()
        {
            FollowPlayer();
        }

        private void FollowPlayer()
        {
            gameObject.transform.position = new Vector3(Player.position.x, Player.position.y,
                gameObject.transform.position.z);
        }
    }
}