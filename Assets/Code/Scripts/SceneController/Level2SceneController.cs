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
        public Transform WalkTarget1;
        public Transform WalkTarget2;

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
            AudioSource.Stop();
            PlayBalletMusic();
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
            while (AudioSource.volume < 0.4)
            {
                yield return new WaitForSeconds(2);
                AudioSource.volume += 0.01f;
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
            yield return MovePlayersToSpeakPosition(WalkTarget1, WalkTarget2);
            yield return Talk();
            BeamPlayersTo(EndbossSpawnPoint.position);
            EnablePlayerMovement();
        }
    }
}