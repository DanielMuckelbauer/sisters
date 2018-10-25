using System.Collections;
using UnityEngine;

namespace Code.Scripts
{
    public class ProximityShooter : MonoBehaviour
    {
        public GameObject Projectile;
        public Transform SpawnPoint;

        private Coroutine shooting;
        private GameObject player;

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (!other.gameObject.tag.Contains("Player"))
                return;
            player = other.gameObject;
            shooting = StartCoroutine(ToggleShooting());
        }

        private void OnTriggerExit2D(Collider2D other)
        {
            if (!other.gameObject.tag.Contains("Player") || shooting == null)
                return;
            StopCoroutine(shooting);
        }

        private IEnumerator ToggleShooting()
        {
            while (true)
            {
                BaseProjectile projectile =
                    Instantiate(Projectile, SpawnPoint.position, new Quaternion()).GetComponent<BaseProjectile>();
                projectile.Shoot(player.transform.position);
                yield return new WaitForSeconds(4);
            }
        }
    }
}