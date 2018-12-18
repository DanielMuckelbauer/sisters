using Code.Classes;
using System.Collections;
using Code.Scripts.Entity;
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
        [SerializeField] private Transform innerWalkTarget1;
        [SerializeField] private Transform innerWalkTarget2;
        [SerializeField] private Transform endingCameraTarget;
        [SerializeField] private Ballerina ballerina;

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
            ballerina.OnDestroyed += PlayEndingCutscene;
        }

        private void PlayEndingCutscene()
        {
            DisableCameraAndMovement();
            StartCoroutine(MoveCameraSmoothly(endingCameraTarget.position));
            StartCoroutine(EntityController.MovePlayersToOppositePositions(innerWalkTarget1, innerWalkTarget2));
            StartCoroutine(TalkAfterBoss());
        }

        private IEnumerator TalkAfterBoss()
        {
            yield return TextController.ShowCharactersNextBubbleText(Character.Muni);
            yield return TextController.ShowCharactersNextBubbleText(Character.Pollin);
            FadeSceneOut();
            yield return new WaitForSeconds(5);
            EnableNextScene();
        }

        private void ChangeMusic()
        {
            audioSource.clip = balletMusic;
            audioSource.Play();
        }

        private IEnumerator Talk()
        {
            yield return TextController.ShowCharactersNextBubbleText(Character.Muni);
            yield return TextController.ShowCharactersNextBubbleText(Character.Pollin);
        }

        private IEnumerator TalkingCutScene()
        {
            ChangeMusic();
            yield return EntityController.MovePlayersToOppositePositions(walkTarget1, walkTarget2);
            yield return Talk();
            EntityController.BeamPlayersTo(endbossSpawnPoint.position);
            EntityController.EnablePlayerMovement();
        }
    }
}