using System;
using Code.Classes;
using System.Collections;
using System.Collections.Generic;
using Code.Scripts.Entity;
using UnityEngine;

namespace Code.Scripts.SceneController
{
    public class SmashLevelSceneController : BaseSceneController
    {
        [SerializeField] private List<Transform> addSpawnPositions;
        [SerializeField] private DaniBoss dani;
        private Vector3 daniCameraPosition;
        [SerializeField] private Transform flyTarget;
        protected override Vector3 FindClosestSpawnPoint()
        {
            return FindTransformNearestToCharacters(RespawnPoints);
        }

        protected override void Start()
        {
            base.Start();
            DisableCameraAndMovement();
            MoveCameraToDani();
            StartCoroutine(PlayOpeningCutscene(1, 2));
            StartCoroutine(PlayOpeningDialog());
            dani.EndFirstStage += EndFirstStage;
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

        private void EndFirstStage()
        {
            DisableCameraAndMovement();
            Vector3 cameraTarget = new Vector3(flyTarget.position.x, flyTarget.position.y, MainCamera.transform.position.z);
            StartCoroutine(MoveCameraSmoothly(cameraTarget));
        }

        private void MoveCameraToDani()
        {
            daniCameraPosition = new Vector3(dani.transform.position.x, dani.transform.position.y + 2,
                            MainCamera.transform.position.z);
            MainCamera.transform.position = daniCameraPosition;
        }
        private IEnumerator PlayOpeningDialog()
        {
            yield return new WaitForSeconds(1);
            yield return SpeechBubbles[Character.Dani].ShowNextBubbleText(2);
            dani.StartFight();
            yield return MoveTo(dani.transform, flyTarget, 1f);
            yield return new WaitForSeconds(1);
            EnableCameraAndMovement();
        }
    }
}