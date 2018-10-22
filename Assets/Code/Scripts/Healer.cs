using System;
using System.Collections;
using UnityEngine;

namespace Code.Scripts
{
    public class Healer : MonoBehaviour
    {
        public AudioSource AudioSource;
        public delegate void HealingEventHandler();
        public static event HealingEventHandler OnHealingConsumed;

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (!other.gameObject.tag.Contains("PLayer"))
                return;
            OnHealingConsumed?.Invoke();
            StartCoroutine(DelayedDestroy());
            AudioSource.Play();
        }

        private IEnumerator DelayedDestroy()
        {
            yield return new WaitForSeconds(3);
            Destroy(gameObject);
        }
    }
}