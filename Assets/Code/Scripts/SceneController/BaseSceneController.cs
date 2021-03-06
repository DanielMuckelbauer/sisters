﻿using Code.Scripts.Entity;
using Code.Scripts.Scene;
using System.Collections;
using System.IO;
using System.Linq;
using System.Xml;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Code.Scripts.SceneController
{
    public abstract class BaseSceneController : MonoBehaviour
    {
        [SerializeField] protected EntityController EntityController;
        [SerializeField] protected GameObject GameElements;
        protected bool IgnoreTrigger;
        [SerializeField] protected GameObject MainCamera;
        [SerializeField] protected TextController TextController;

        public void SceneTriggerEntered()
        {
            if (IgnoreTrigger)
                return;
            HandleTrigger();
        }

        protected void DisableCameraAndMovement()
        {
            DisableFollowingCamera();
            EntityController.DisablePlayerMovement();
        }

        protected void DisableFollowingCamera()
        {
            if (MainCamera != null)
                MainCamera.GetComponent<FollowingCamera>().Following = false;
        }

        protected void EnableCameraAndMovement()
        {
            EntityController.EnablePlayerMovement();
            EnableFollowingCamera();
        }

        protected void EnableFollowingCamera()
        {
            MainCamera.GetComponent<FollowingCamera>().Following = true;
        }

        protected void EnableNextScene()
        {
            StartCoroutine(LoadNextSceneOnInput());
            TextController.ShowPressAnyKey();
        }

        protected IEnumerator Fade(SpriteRenderer sprite, int from = 1, int to = 0, float duration = 5)
        {
            float t = 0;
            float startTime = Time.time;
            while (t < 1)
            {
                if (sprite == null)
                    yield break;
                t = (Time.time - startTime) / duration;
                sprite.color = new Color(1f, 1f, 1f, Mathf.SmoothStep(from, to, t));
                yield return new WaitForSeconds(0.2f);
            }

            yield return new WaitForSeconds(3);
        }

        protected void FadeSceneOut()
        {
            GameElements.GetComponentsInChildren<SpriteRenderer>().ToList()
                .ForEach(s => { StartCoroutine(Fade(s)); });
        }

        protected virtual void HandleTrigger()
        {
        }

        protected IEnumerator MoveCameraSmoothly(Vector3 targetPosition, float smoothTime = 2, float tolerance = 0.2f)
        {
            Transform cameraTransform = MainCamera.transform;
            targetPosition = new Vector3(targetPosition.x, targetPosition.y,
                cameraTransform.position.z);
            Vector3 velocity = Vector3.zero;
            float distance = Vector3.Distance(cameraTransform.position, targetPosition);
            while (distance > tolerance)
            {
                cameraTransform.position = Vector3.SmoothDamp(cameraTransform.position, targetPosition,
                    ref velocity, smoothTime);
                distance = Vector3.Distance(cameraTransform.position, targetPosition);
                yield return null;
            }
        }

        protected IEnumerator PlayOpeningCutscene(int time, int times)
        {
            yield return ShowNextTextSection(time, times);
            TextController.ActivateCanvas(false);
            GameElements.SetActive(true);
        }

        protected IEnumerator ShowNextTextSection(int time, int times = 1)
        {
            for (int i = 0; i < times; i++)
            {
                TextController.SetCanvasTextToNextString();
                yield return new WaitForSeconds(time);
            }
        }

        protected virtual void Start()
        {
            Cursor.visible = false;
            Player.OnDie += GameOverScreen;
        }

        private void QuitOnEscape()
        {
            if (Input.GetKeyDown(KeyCode.Escape))
                BackToMenu();
        }

        protected virtual void BackToMenu()
        {
            SceneManager.LoadScene(0);
            UnsubscribeAllDelegatesFromStaticEvents();
        }

        private void ResetScene()
        {
            UnsubscribeAllDelegatesFromStaticEvents();
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }

        private static void SaveGame()
        {
            Directory.CreateDirectory("Savegame");
            string path = MenuController.SaveGamePath;
            File.Delete(path);
            string level = SceneManager.GetActiveScene().buildIndex.ToString();
            XmlWriter xmlWriter = XmlWriter.Create(path);
            xmlWriter.WriteStartDocument();
            xmlWriter.WriteStartElement("NextAvailableLevel");
            xmlWriter.WriteString(level);
            xmlWriter.Close();
        }

        protected virtual void UnsubscribeAllDelegatesFromStaticEvents()
        {
            Player.ResetOnDieEvent();
            Healer.ResetOnHealingConsumedEvent();
            BasePatrollingEnemy.ResetOnHitSound();
        }

        private void GameOverScreen()
        {
            DisableFollowingCamera();
            EntityController.DisablePlayerMovement();
            StartCoroutine(ShowDied());
        }

        private IEnumerator LoadNextSceneOnInput()
        {
            while (!Input.anyKeyDown)
            {
                yield return null;
            }

            UnsubscribeAllDelegatesFromStaticEvents();
            SaveGame();
            SceneManager.LoadScene((SceneManager.GetActiveScene().buildIndex + 1) %
                                   SceneManager.sceneCountInBuildSettings);
        }

        private IEnumerator ShowDied()
        {
            FadeSceneOut();
            yield return new WaitForSeconds(3);
            TextController.ShowDiedText();
            yield return new WaitForSeconds(5);
            ResetScene();
        }

        private void Update()
        {
            QuitOnEscape();
        }
    }
}