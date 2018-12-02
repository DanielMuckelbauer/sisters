using Code.Classes;
using Code.Scripts.Scene;
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

        protected override void HandleTrigger()
        {
            IgnoreTrigger = true;
            EntityController.DisablePlayerMovement();
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
            yield return TextController.ShowCharactersNextBubbleText(Character.Muni);
            yield return TextController.ShowCharactersNextBubbleText(Character.Pollin);
        }

        private IEnumerator TalkingCutScene()
        {
            ChangeMusic();
            yield return EntityController.MovePlayersToSpeakPosition(walkTarget1, walkTarget2);
            yield return Talk();
            EntityController.BeamPlayersTo(endbossSpawnPoint.position);
            EntityController.EnablePlayerMovement();
        }
    }
}