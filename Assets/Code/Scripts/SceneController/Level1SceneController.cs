using System.Collections;
using UnityEngine;

namespace Code.Scripts.SceneController
{
    public class Level1SceneController : BaseSceneController
    {
        public Transform CutsceneTarget;

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
            MainCamera.GetComponent<FollowingCamera>().Following = false;
            Vector3 targetPosition = new Vector3(CutsceneTarget.position.x, CutsceneTarget.position.y,
                MainCamera.transform.position.z);
            StartCoroutine(MoveCamera(targetPosition));
            yield return new WaitForSeconds(6);
            SetUpSpeechBubble();
            yield return ShowNextBubbleText(2);
        }
    }
}