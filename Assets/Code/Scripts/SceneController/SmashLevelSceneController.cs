using System;
using System.Collections;
using Code.Classes;
using UnityEngine;

namespace Code.Scripts.SceneController
{
    public class SmashLevelSceneController : BaseSceneController
    {
        [SerializeField] private GameObject dani;
        private Vector3 daniCameraPosition;

        protected override void Start()
        {
            base.Start();
            DisableCameraAndMovement();
            daniCameraPosition = new Vector3(dani.transform.position.x, dani.transform.position.y,
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

        private IEnumerator PlayOpeningDialog()
        {
            yield return new WaitForSeconds(5);
            yield return SpeechBubbles[Character.Dani].ShowNextBubbleText(2);
            EnableCameraAndMovement();
        }

        private void EnableCameraAndMovement()
        {
            EnablePlayerMovement();
            EnableFollowingCamera();
        }

        protected override Vector3 FindClosestSpawnPoint()
        {
            return FindTransformNearestToCharacters(RespawnPoints);
        }
    }
}