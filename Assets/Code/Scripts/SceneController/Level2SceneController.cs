using Code.Classes;
using System.Collections;
using UnityEngine;

namespace Code.Scripts.SceneController
{
    public class Level2SceneController : BaseSceneController
    {
        [SerializeField] private AudioSource audioSource;
        [SerializeField] private AudioClip balletMusic;
        [SerializeField] private Transform endbossSpawnPoint;
        [SerializeField] private Transform walkTarget1;
        [SerializeField] private Transform walkTarget2;

        private bool endTriggerActivated;

        public override void SceneTriggerEntered()
        {
            if (endTriggerActivated)
                return;
            endTriggerActivated = true;
            DisablePlayerMovement();
            StartCoroutine(TalkingCutScene());
        }

        protected override void Start()
        {
            base.Start();
            StartCoroutine(PlayOpeningCutscene(5, 2));
        }

        private void ChangeMusic()
        {
            audioSource.Stop();
            PlayBalletMusic();
        }

        private void PlayBalletMusic()
        {
            audioSource.clip = balletMusic;
            audioSource.volume = 0f;
            audioSource.Play();
            StartCoroutine(SlowlyIncreaseVolume());
        }

        private IEnumerator SlowlyIncreaseVolume()
        {
            while (audioSource.volume < 0.4)
            {
                yield return new WaitForSeconds(2);
                audioSource.volume += 0.01f;
            }
        }

        private IEnumerator Talk()
        {
            SpeechBubble munisBubble = SpeechBubbles[Character.Muni];
            SpeechBubble pollinsBubble = SpeechBubbles[Character.Pollin];
            yield return munisBubble.ShowNextBubbleText();
            yield return pollinsBubble.ShowNextBubbleText();
        }

        private IEnumerator TalkingCutScene()
        {
            ChangeMusic();
            yield return MovePlayersToSpeakPosition(walkTarget1, walkTarget2);
            yield return Talk();
            BeamPlayersTo(endbossSpawnPoint.position);
            EnablePlayerMovement();
        }
    }
}