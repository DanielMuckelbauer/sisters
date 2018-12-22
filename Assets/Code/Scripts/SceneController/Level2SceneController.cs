using Code.Classes;
using Code.Scripts.Entity;
using System.Collections;
using UnityEngine;

namespace Code.Scripts.SceneController
{
    public class Level2SceneController : BaseSceneController
    {
        [SerializeField] private AudioSource audioSource;
        [SerializeField] private Ballerina ballerina;
        [SerializeField] private AudioClip balletMusic;
        [SerializeField] private Transform cameraTargetOutside;
        [SerializeField] private Transform endbossSpawnPoint;
        [SerializeField] private Transform endingCameraTarget;
        [SerializeField] private Transform innerWalkTarget1;
        [SerializeField] private Transform innerWalkTarget2;
        [SerializeField] private Transform walkTarget1;
        [SerializeField] private Transform walkTarget2;

        protected override void HandleTrigger()
        {
            IgnoreTrigger = true;
            DisableCameraAndMovement();
            StartCoroutine(MoveCameraSmoothly(cameraTargetOutside.position));
            StartCoroutine(TalkingCutScene());
        }

        protected override void Start()
        {
            base.Start();
            StartCoroutine(PlayOpeningCutscene(5, 2));
            ballerina.OnDestroyed += PlayEndingCutscene;
        }

        private void ChangeMusic()
        {
            audioSource.clip = balletMusic;
            audioSource.volume *= 2f;
            audioSource.Play();
        }

        private void PlayEndingCutscene()
        {
            DisableCameraAndMovement();
            StartCoroutine(MoveCameraSmoothly(endingCameraTarget.position));
            StartCoroutine(EntityController.MovePlayersToOppositePositions(innerWalkTarget1, innerWalkTarget2));
            StartCoroutine(TalkAfterBoss());
        }

        private IEnumerator Talk()
        {
            yield return TextController.ShowCharactersNextBubbleText(Character.Muni);
            yield return TextController.ShowCharactersNextBubbleText(Character.Pollin);
            yield return new WaitForSeconds(2);
        }

        private IEnumerator TalkAfterBoss()
        {
            yield return TextController.ShowCharactersNextBubbleText(Character.Muni);
            yield return TextController.ShowCharactersNextBubbleText(Character.Pollin);
            FadeSceneOut();
            yield return new WaitForSeconds(5);
            EnableNextScene();
        }

        private IEnumerator TalkingCutScene()
        {
            ChangeMusic();
            yield return EntityController.MovePlayersToOppositePositions(walkTarget1, walkTarget2);
            yield return Talk();
            EntityController.BeamPlayersTo(endbossSpawnPoint.position);
            ballerina.gameObject.SetActive(true);
            EnableCameraAndMovement();
        }

        protected override void ResetScene()
        {
            ballerina.ResetOnDestroy();
            base.ResetScene();
        }
    }
}