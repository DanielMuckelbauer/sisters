using System.Collections;
using UnityEngine;

namespace Code.Scripts
{
    public class Healer : MonoBehaviour
    {
        public AudioSource AnpanAudioSource;
        public AudioSource MainAudioSource;

        private bool collisionTriggered;

        private float originalVolume;

        public delegate void HealingEventHandler();

        public static event HealingEventHandler OnHealingConsumed;

        public static void ResetOnHealingConsumed()
        {
            OnHealingConsumed = null;
        }

        private IEnumerator DelayedDestroy()
        {
            yield return new WaitForSeconds(4.5f);
            MainAudioSource.volume = originalVolume;
            Destroy(gameObject);
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (!other.gameObject.tag.Contains("Player") || collisionTriggered)
                return;
            collisionTriggered = true;
            OnHealingConsumed?.Invoke();
            StartCoroutine(DelayedDestroy());
            PlayMusic();
        }

        private void PlayMusic()
        {
            originalVolume = MainAudioSource.volume;
            MainAudioSource.volume = 0.1f;
            AnpanAudioSource.Play();
        }
    }
}