using Code.Classes;
using Code.Scripts.Entity;
using System.Collections;
using UnityEngine;

namespace Code.Scripts.SceneController
{
    public class Level4SceneController : BaseSceneController
    {
        [SerializeField] private Clown clown;
        private Animator clownAnimator;
        [SerializeField] private Transform clownCameraTarget;
        [SerializeField] private AudioClip baepsaeClip;
        [SerializeField] private AudioSource mainSource;
        [SerializeField] private Transform walkTarget1;
        [SerializeField] private Transform walkTarget2;
        [SerializeField] private Transform endingTarget;
        [SerializeField] private Transform walkTargetBeforeBoss1;
        [SerializeField] private Transform walkTargetBeforeBoss2;

        protected override void HandleTrigger()
        {
            IgnoreTrigger = true;
            DisableCameraAndMovement();
            StartCoroutine(ClownCutscene());
        }

        protected override void Start()
        {
            base.Start();
            StartCoroutine(PlayOpeningCutscene(1, 3));
            clownAnimator = clown.GetComponent<Animator>();
            clown.OnDestroyed += EndingCutscene;
        }

        private void EndingCutscene()
        {
            DisableCameraAndMovement();
            StartCoroutine(MoveCameraSmoothly(clownCameraTarget.position));
            StartCoroutine(EntityController.MovePlayersToOppositePositions(walkTarget1, walkTarget2));
            StartCoroutine(TalkAfterBoss());
        }

        private IEnumerator TalkAfterBoss()
        {
            yield return TextController.ShowCharactersNextBubbleText(Character.Muni);
            yield return TextController.ShowCharactersNextBubbleText(Character.Pollin);
            yield return new WaitForSeconds(3);
            yield return MoveCameraSmoothly(endingTarget.position);
            FadeSceneOut();
            EnableNextScene();
        }

        private IEnumerator ClownCutscene()
        {
            yield return EntityController.MovePlayersToOppositePositions(walkTargetBeforeBoss1, walkTargetBeforeBoss2);
            yield return Talk();
            ChangeMusic();
            yield return MoveCameraSmoothly(clownCameraTarget.position);
            yield return Dance();
            clown.StartPunching();
        }

        private void ChangeMusic()
        {
            mainSource.clip = baepsaeClip;
            mainSource.Play();
        }

        private IEnumerator Dance()
        {
            clownAnimator.SetBool("Dance", true);
            yield return new WaitForSeconds(7);
            clownAnimator.SetBool("Dance", false);
            EnableCameraAndMovement();
        }

        private IEnumerator Talk()
        {
            yield return TextController.ShowCharactersNextBubbleText(Character.Pollin, 2);
            yield return TextController.ShowCharactersNextBubbleText(Character.Muni, 2);
        }
    }
}