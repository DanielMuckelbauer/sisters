using Code.Classes;
using Code.Scripts.Entity;
using System.Collections;
using UnityEngine;

namespace Code.Scripts.SceneController
{
    public class Level2SceneController : BaseSceneController
    {
        public AudioSource AudioSource;
        public AudioClip BalletMusic;
        public Transform EndbossSpawnPoint;

        public override void SceneTriggerEntered()
        {
            DisablePlayerMovement();
            StartCoroutine(TalkingCutScene());
        }

        protected override void Start()
        {
            base.Start();
            StartCoroutine(PlayOpeningCutscene(5, 2));
        }

        private void PlayBalletMusic()
        {
            AudioSource.clip = BalletMusic;
            AudioSource.volume = 0f;
            AudioSource.Play();
            StartCoroutine(SlowlyIncreaseVolume());
        }

        private IEnumerator SlowlyIncreaseVolume()
        {
            while (AudioSource.volume < 0.1)
            {
                yield return new WaitForSeconds(2);
                AudioSource.volume += 0.01f;
            }
        }

        private IEnumerator TalkingCutScene()
        {
            SpeechBubble munisBubble = SpeechBubbles[Character.Muni];
            SpeechBubble pollinsBubble = SpeechBubbles[Character.Pollin];
            munisBubble.ShowSpeechBubble();
            AudioSource.Stop();
            PlayBalletMusic();
            yield return munisBubble.ShowNextBubbleText();
            pollinsBubble.ShowSpeechBubble();
            yield return pollinsBubble.ShowNextBubbleText();
            munisBubble.HideSpeechBubble();
            pollinsBubble.HideSpeechBubble();
            foreach (Player player in Characters.Values)
            {
                player.transform.position = EndbossSpawnPoint.position;
            }

            EnablePlayerMovement();
        }
    }
}