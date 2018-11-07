using Code.Classes;
using System.Collections;
using UnityEngine;

namespace Code.Scripts.SceneController
{
    public class Level1SceneController : BaseSceneController
    {
        public Transform CameraTarget;
        public Transform JumpTarget;
        public Transform WalkTarget;

        public override void SceneTriggerEntered()
        {
            StartCoroutine(PlayEndingCutscene());
        }

        protected override void Start()
        {
            base.Start();
            StartCoroutine(PlayOpeningCutscene(5, 4));
        }

        private IEnumerator GoToDiaperChanger()
        {
            Characters[Character.Pollin].GoTo(WalkTarget.position);
            yield return new WaitForSeconds(2);
        }

        private IEnumerator JumpUpToDiaperChanger()
        {
            Transform pollin = Characters[Character.Pollin].transform;
            pollin.GetComponent<Rigidbody2D>().isKinematic = true;
            while (pollin.position != JumpTarget.position)
            {
                float step = 3f * Time.deltaTime;
                pollin.position = Vector3.MoveTowards(pollin.position, JumpTarget.position, step);
                yield return null;
            }

            pollin.transform.Rotate(0, 0, 90);
        }

        private IEnumerator PlayEndingCutscene()
        {
            DisablePlayerMovement();
            DisablePlayerMovement();
            DisableFollowingCamera();
            Vector3 targetPosition = new Vector3(CameraTarget.position.x, CameraTarget.position.y,
                MainCamera.transform.position.z);
            StartCoroutine(MoveCamera(targetPosition));
            yield return new WaitForSeconds(6);
            SpeechBubbles[Character.Dani].ShowSpeechBubble();
            yield return SpeechBubbles[Character.Dani].ShowNextBubbleText(2);
            yield return GoToDiaperChanger();
            SpeechBubbles[Character.Dani].HideSpeechBubble();
            yield return JumpUpToDiaperChanger();
            FadeSceneOut();
            yield return new WaitForSeconds(10);
            EnableNextScene();
        }
    }
}