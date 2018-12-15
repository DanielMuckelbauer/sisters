using System.Collections;
using Code.Classes;
using UnityEngine;

namespace Code.Scripts.SceneController
{
    public class Level3SceneController : BaseSceneController
    {
        [SerializeField] private Transform innerWalkTarget1;
        [SerializeField] private Transform innerWalkTarget2;
        [SerializeField] private Transform walkTarget1;
        [SerializeField] private Transform walkTarget2;
        [SerializeField] private Transform cameraTarget;

        protected override void HandleTrigger()
        {
            IgnoreTrigger = true;
            EntityController.DisablePlayerMovement();
            StartCoroutine(TalkingCutScene());
        }

        protected override void Start()
        {
            base.Start();
            StartCoroutine(PlayOpeningCutscene(1, 3));
        }

        private IEnumerator GoIntoDoor()
        {
            yield return EntityController.MovePlayersToOppositePositions(innerWalkTarget1, innerWalkTarget2);
            yield return new WaitForSeconds(2);
            EntityController.PlayerList.ForEach(player => player.gameObject.SetActive(false));
            StartAndShowFireWorks();
            yield return new WaitForSeconds(9);
            EnableNextScene();
        }

        private void StartAndShowFireWorks()
        {
            Vector3 targetPosition = new Vector3(cameraTarget.position.x, cameraTarget.position.y,
                MainCamera.transform.position.z);
            StartCoroutine(MoveCameraSmoothly(targetPosition, 6));
            cameraTarget.GetComponent<AudioSource>().Play();
        }

        private IEnumerator Talk()
        {
            yield return TextController.ShowCharactersNextBubbleText(Character.Pollin);
            yield return TextController.ShowCharactersNextBubbleText(Character.Muni);
            DisableFollowingCamera();
        }

        private IEnumerator TalkingCutScene()
        {
            yield return EntityController.MovePlayersToOppositePositions(walkTarget1, walkTarget2);
            yield return Talk();
            yield return GoIntoDoor();
        }
    }
}