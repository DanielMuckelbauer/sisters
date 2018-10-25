using System.Collections;
using UnityEngine;

namespace Code.Scripts
{
    public class Healer : MonoBehaviour
    {
        public AudioSource AnpanAudioSource;
        public AudioSource MainAudioSource;

        public delegate void HealingEventHandler();

        public static event HealingEventHandler OnHealingConsumed;

        private bool collisionTriggered;

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (!other.gameObject.tag.Contains("Player") || collisionTriggered)
                return;
            collisionTriggered = true;
            OnHealingConsumed?.Invoke();
            StartCoroutine(DelayedDestroy());
            MainAudioSource.volume = 0.1f;
            AnpanAudioSource.Play();
        }

        private IEnumerator DelayedDestroy()
        {
            yield return new WaitForSeconds(4.5f);
            MainAudioSource.volume = 1;
            Destroy(gameObject);
        }
    }
}