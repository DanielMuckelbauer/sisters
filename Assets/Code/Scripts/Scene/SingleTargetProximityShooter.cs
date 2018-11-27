using System.Collections;
using UnityEngine;

namespace Code.Scripts.Scene
{
    public class SingleTargetProximityShooter : MonoBehaviour
    {
        public AudioSource AudioSource;
        public GameObject Projectile;
        public Transform SpawnPoint;
        private GameObject player;
        private Coroutine shooting;

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (!other.gameObject.tag.Contains("Player") || shooting != null)
                return;
            player = other.gameObject;
            shooting = StartCoroutine(ToggleShooting());
        }

        private void OnTriggerExit2D(Collider2D other)
        {
            if (!other.gameObject.tag.Contains("Player") || shooting == null)
                return;
            StopCoroutine(shooting);
            shooting = null;
        }

        private IEnumerator ToggleShooting()
        {
            while (true)
            {
                Vector3 offset = new Vector3(Random.value * 3, 0 , 0);
                yield return new WaitForSeconds(2);
                AudioSource.Play();
                BaseProjectile projectile =
                    Instantiate(Projectile, SpawnPoint.position + offset, new Quaternion()).GetComponent<BaseProjectile>();
                if (player != null)
                    projectile.Shoot(player.transform.position);
                yield return new WaitForSeconds(2);
            }
        }
    }
}