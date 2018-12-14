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

        protected override void HandleTrigger()
        {
            IgnoreTrigger = true;
            StartCoroutine(PlayEndingCutscene());
        }

        protected override void Start()
        {
            base.Start();
            StartCoroutine(PlayOpeningCutscene(1, 4));
        }

        private IEnumerator GoToDiaperChanger()
        {
            yield return EntityController.GetCharacter(Character.Pollin).GoTo(WalkTarget.position);
            yield return new WaitForSeconds(1);
        }

        private IEnumerator JumpUpToDiaperChanger()
        {
            Transform pollin = EntityController.GetCharacter(Character.Pollin).transform;
            pollin.GetComponent<Rigidbody2D>().isKinematic = true;
            yield return EntityController.MoveTo(pollin, JumpTarget);
            pollin.transform.Rotate(0, 0, 90);
        }

        private IEnumerator PlayEndingCutscene()
        {
            DisableCameraAndMovement();
            Vector3 targetPosition = new Vector3(CameraTarget.position.x, CameraTarget.position.y,
                MainCamera.transform.position.z);
            StartCoroutine(MoveCameraSmoothly(targetPosition));
            yield return new WaitForSeconds(2);
            yield return TextController.ShowCharactersNextBubbleText(Character.Dani, 2);
            yield return GoToDiaperChanger();
            yield return JumpUpToDiaperChanger();
            FadeSceneOut();
            yield return new WaitForSeconds(4);
            EnableNextScene();
        }
    }
}