using System.Collections;
using Code.Classes;
using UnityEngine;

namespace Code.Scripts.SceneController
{
    public class Level1SceneController : BaseSceneController
    {
        public Transform CameraTarget;
        public Transform WalkTarget;
        public Transform JumpTarget;

        protected override void Start()
        {
            base.Start();
            StartCoroutine(PlayOpeningCutscene());
        }

        private IEnumerator PlayOpeningCutscene()
        {
            yield return ShowNextTextSection(1, 4);
            UiCanvas.SetActive(false);
            GameElements.SetActive(true);
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            StartCoroutine(PlayEndingCutscene());
        }

        private IEnumerator PlayEndingCutscene()
        {
            DisablePlayerMovement();
            DisableFollowingCamera();
            Vector3 targetPosition = new Vector3(CameraTarget.position.x, CameraTarget.position.y,
                MainCamera.transform.position.z);
            StartCoroutine(MoveCamera(targetPosition));
            yield return new WaitForSeconds(6);
            SetUpSpeechBubble();
            yield return ShowNextBubbleText(2);
            yield return GoToDiaperChanger();
            yield return JumpUpToDiaperChanger();
        }

        private IEnumerator JumpUpToDiaperChanger()
        {
            Transform pollin = Players[Character.Pollin].transform;
            while (pollin.position != JumpTarget.position)
            {
                float step = 1 * Time.deltaTime;
                pollin.position = Vector3.MoveTowards(pollin.position, JumpTarget.position, step);
                yield return null;
            }
        }

        private IEnumerator GoToDiaperChanger()
        {
            Players[Character.Pollin].GoTo(WalkTarget.position, 1.2f);
            yield return new WaitForSeconds(4);
        }
    }
}