using System.Collections;
using Code.Classes;
using UnityEngine;

namespace Code.Scripts.SceneController
{
    public class Level4SceneController : BaseSceneController
    {
        [SerializeField] private GameObject clown;
        private Animator clownAnimator;
        [SerializeField] private Transform clownCameraTarget;

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
        }

        private IEnumerator ClownCutscene()
        {
            yield return Talk();
            Vector3 targetPosition = new Vector3(clownCameraTarget.position.x, clownCameraTarget.position.y,
                MainCamera.transform.position.z);
            yield return MoveCameraSmoothly(targetPosition);
            yield return Dance();

        }

        private IEnumerator Dance()
        {
            clownAnimator.SetBool("Dance", true);
            yield return new WaitForSeconds(1);
            clownAnimator.SetBool("Dance", false);
        }

        private IEnumerator Talk()
        {
            yield return TextController.ShowCharactersNextBubbleText(Character.Pollin);
            yield return TextController.ShowCharactersNextBubbleText(Character.Muni);
        }
    }
}