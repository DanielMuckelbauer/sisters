﻿using System.Collections;
using UnityEngine;

namespace Code.Scripts
{
    public class SingleTargetProximityShooter : MonoBehaviour
    {
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
                BaseProjectile projectile =
                    Instantiate(Projectile, SpawnPoint.position, new Quaternion()).GetComponent<BaseProjectile>();
                if (player != null)
                    projectile.Shoot(player.transform.position);
                yield return new WaitForSeconds(4);
            }
        }
    }
}