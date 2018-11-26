using Code.Classes;
using Code.Scripts.Entity;
using Code.Scripts.Scene;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Code.Scripts.SceneController
{
    public abstract class BaseSceneController : MonoBehaviour
    {
        [SerializeField] protected Dictionary<Character, Player> Characters;
        [SerializeField] protected GameObject GameElements;
        protected bool IgnoreTrigger;
        [SerializeField] protected GameObject MainCamera;
        [SerializeField] protected PlayerRepositioningController PlayerRepositioningController;
        [SerializeField] protected TextController TextController;
        [SerializeField] private List<Player> playerList;

        public void SceneTriggerEntered()
        {
            if (IgnoreTrigger)
                return;
            HandleTrigger();
        }

        protected void DisableFollowingCamera()
        {
            MainCamera.GetComponent<FollowingCamera>().Following = false;
        }

        protected void DisablePlayerMovement()
        {
            foreach (Player player in Characters.Values)
                player.SetMovement(false);
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

        protected void EnablePlayerMovement()
        {
            foreach (Player player in Characters.Values)
                player.SetMovement(true);
        }

        protected IEnumerator Fade(SpriteRenderer sprite, int from, int to, float duration = 5)
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
                .ForEach(s => { StartCoroutine(Fade(s, 1, 0)); });
        }

        protected virtual void HandleTrigger()
        {
        }

        protected IEnumerator MoveCameraSmoothly(Vector3 targetPosition)
        {
            Transform cameraTransform = MainCamera.transform;
            const float smoothTime = 2;
            Vector3 velocity = Vector3.zero;
            while (cameraTransform.position != targetPosition)
            {
                cameraTransform.position = Vector3.SmoothDamp(cameraTransform.position, targetPosition,
                    ref velocity, smoothTime);
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
            Player.OnDie += GameOverScreen;
            InitializePlayerDictionary();
        }

        private static void ResetScene()
        {
            UnsubscribeAllDelegatesFromStaticEvents();
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }

        private static void SaveGame()
        {
            Directory.CreateDirectory("Savegame");
            string path = MenuController.SaveGamePath;
            File.Delete(path);
            string level = (SceneManager.GetActiveScene().buildIndex + 1).ToString();
            XmlWriter xmlWriter = XmlWriter.Create(path);
            xmlWriter.WriteStartDocument();
            xmlWriter.WriteStartElement("Level");
            xmlWriter.WriteString(level);
            xmlWriter.Close();
        }

        private static void UnsubscribeAllDelegatesFromStaticEvents()
        {
            Player.ResetOnDieEvent();
            Healer.ResetOnHealingConsumedEvent();
        }

        private void GameOverScreen()
        {
            DisableFollowingCamera();
            DisablePlayerMovement();
            StartCoroutine(ShowDied());
        }

        private void InitializePlayerDictionary()
        {
            Characters = new Dictionary<Character, Player>();
            if (playerList == null || playerList.Count == 0)
                return;
            Player muni = playerList.First(p => p.gameObject.name.Contains("Muni"));
            Characters.Add(Character.Muni, muni);
            Player pollin = playerList.First(p => p.gameObject.name.Contains("Pollin"));
            Characters.Add(Character.Pollin, pollin);
        }

        private IEnumerator LoadNextSceneOnInput()
        {
            while (!Input.anyKeyDown)
            {
                yield return null;
            }

            UnsubscribeAllDelegatesFromStaticEvents();
            SaveGame();
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }

        private IEnumerator ShowDied()
        {
            FadeSceneOut();
            yield return new WaitForSeconds(3);
            TextController.ShowDiedText();
            yield return new WaitForSeconds(5);
            ResetScene();
        }
    }
}