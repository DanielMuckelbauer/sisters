using System;
using Code.Classes;
using Code.Scripts.Entity;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Code.Scripts.SceneController
{
    public class SmashLevelSceneController : BaseSceneController
    {
        [SerializeField] private List<Transform> addSpawnPositions;
        [SerializeField] private DaniBoss dani;
        private Vector3 daniCameraPosition;
        [SerializeField] private Transform flyTarget;
        private Stack<Action> phaseChangeMethods; 

        protected override void Start()
        {
            base.Start();
            DisableCameraAndMovement();
            MoveCameraToDani();
            StartCoroutine(PlayOpeningCutscene(1, 2));
            StartCoroutine(PlayOpeningDialog());
            InitializePhaseChangeMethods();
            dani.OnNextPhase += ChangeFightPhase;
        }

        private void AfterFirstPhase()
        {
            DisableCameraAndMovement();
            Vector3 cameraTarget =
                new Vector3(flyTarget.position.x, flyTarget.position.y, MainCamera.transform.position.z);
            StartCoroutine(MoveCameraSmoothly(cameraTarget));
        }

        private void AfterSecondPhase()
        {
            throw new NotImplementedException();
        }

        private void AfterThirdPhase()
        {
            throw new NotImplementedException();
        }

        private void ChangeFightPhase()
        {
            Action nextPhaseMethod = phaseChangeMethods.Pop();
            nextPhaseMethod.Invoke();
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

        private void InitializePhaseChangeMethods()
        {
            phaseChangeMethods = new Stack<Action>();
            phaseChangeMethods.Push(AfterThirdPhase);
            phaseChangeMethods.Push(AfterSecondPhase);
            phaseChangeMethods.Push(AfterFirstPhase);
        }
        private void MoveCameraToDani()
        {
            const int offset = 2;
            daniCameraPosition = new Vector3(dani.transform.position.x, dani.transform.position.y + offset,
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