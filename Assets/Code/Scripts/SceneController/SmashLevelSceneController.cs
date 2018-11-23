using Code.Classes;
using System.Collections;
using Code.Scripts.Entity;
using UnityEngine;

namespace Code.Scripts.SceneController
{
    public class SmashLevelSceneController : BaseSceneController
    {
        [SerializeField] private DaniBoss dani;
        [SerializeField] private Transform flyTarget;
        private Vector3 daniCameraPosition;

        protected override Vector3 FindClosestSpawnPoint()
        {
            return FindTransformNearestToCharacters(RespawnPoints);
        }

        protected override void Start()
        {
            base.Start();
            DisableCameraAndMovement();
            daniCameraPosition = new Vector3(dani.transform.position.x, dani.transform.position.y + 2,
                MainCamera.transform.position.z);
            MainCamera.transform.position = daniCameraPosition;
            StartCoroutine(PlayOpeningCutscene(1, 2));
            StartCoroutine(PlayOpeningDialog());
        }

        private void DisableCameraAndMovement()
        {
            DisableFollowingCamera();
            DisablePlayerMovement();
        }

        private void EnableCameraAndMovement()
        {
            EnablePlayerMovement();
            EnableFollowingCamera();
        }

        private IEnumerator PlayOpeningDialog()
        {
            yield return new WaitForSeconds(5);
            yield return SpeechBubbles[Character.Dani].ShowNextBubbleText(2);
            dani.StartFight();
            yield return MoveTo(dani.transform, flyTarget, 1f);
            yield return new WaitForSeconds(10);
            EnableCameraAndMovement();
        }
    }
}