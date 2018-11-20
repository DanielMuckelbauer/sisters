using Code.Classes;
using Code.Scripts.Entity;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Code.Scripts.SceneController
{
    public abstract class BaseSceneController : MonoBehaviour
    {
        public static int CutsceneStringCounter;
        public static List<string> CutsceneStrings;
        [SerializeField] protected Dictionary<Character, Player> Characters;
        [SerializeField] protected GameObject GameElements;
        protected bool IgnoreTrigger;
        [SerializeField] protected GameObject MainCamera;
        [SerializeField] protected Dictionary<Character, SpeechBubble> SpeechBubbles;
        [SerializeField] protected GameObject TextCanvas;
        [SerializeField] private TMP_Text canvasText;
        [SerializeField] private List<GameObject> npcs;
        [SerializeField] private List<Player> playerList;
        [SerializeField] private SpriteRenderer pressAnyKeySprite;
        [SerializeField] private GameObject respawnPointParent;
        private List<Transform> respawnPoints;
        [SerializeField] private TextAsset textAsset;
        public static event Action OnRespawn;

        public static void InvokeRespawnBoth()
        {
            OnRespawn?.Invoke();
        }

        public void SceneTriggerEntered()
        {
            if (IgnoreTrigger)
                return;
            HandleTrigger();
        }

        protected void BeamPlayersTo(Vector3 target)
        {
            BeamPlayerTo(target, playerList[0].transform, -0.5f);
            BeamPlayerTo(target, playerList[1].transform, 0.5f);
        }

        protected void DisableFollowingCamera()
        {
            MainCamera.GetComponent<FollowingCamera>().Following = false;
        }

        protected void EnableFollowingCamera()
        {
            MainCamera.GetComponent<FollowingCamera>().Following = true;
        }

        protected void DisablePlayerMovement()
        {
            foreach (Player player in Characters.Values)
                player.SetMovement(false);
        }

        protected void EnableNextScene()
        {
            StartCoroutine(LoadNextSceneOnInput());
            canvasText.text = string.Empty;
            TextCanvas.SetActive(true);
            pressAnyKeySprite.enabled = true;
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

        protected IEnumerator MoveCamera(Vector3 targetPosition)
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

        protected IEnumerator MovePlayersToSpeakPosition(Transform point1, Transform point2)
        {
            playerList.ForEach(p => MoveObjectToTargetIfToFarAway(p.transform, point1.position));
            Transform leftPoint = FindLeft(point1, point2);
            Transform rightPoint = FindRight(point1, point2);
            Transform leftPlayer = FindLeft(playerList[0].transform, playerList[1].transform);
            Transform rightPlayer = FindRight(playerList[0].transform, playerList[1].transform);
            yield return leftPlayer.GetComponent<Player>().GoTo(leftPoint.position);
            yield return rightPlayer.GetComponent<Player>().GoTo(rightPoint.position);
            yield return playerList[0].TurnTo(playerList[1].transform.position);
            yield return playerList[1].TurnTo(playerList[0].transform.position);
        }

        protected IEnumerator PlayOpeningCutscene(int time, int times)
        {
            yield return ShowNextTextSection(time, times);
            TextCanvas.SetActive(false);
            GameElements.SetActive(true);
        }

        protected void SetCanvasTextToNextString()
        {
            canvasText.text = CutsceneStrings[CutsceneStringCounter++];
        }

        protected IEnumerator ShowNextTextSection(int time, int times = 1)
        {
            for (int i = 0; i < times; i++)
            {
                SetCanvasTextToNextString();
                yield return new WaitForSeconds(time);
            }
        }

        protected virtual void Start()
        {
            respawnPoints = InitializeRespawnPoints();
            OnRespawn += RespawnBoth;
            Player.OnDie += GameOverScreen;
            InitializeCutsceneStrings();
            InitializePlayerDictionary();
            InitializeBubbles();
        }

        private static Transform FindLeft(Transform point1, Transform point2)
        {
            return point1.position.x < point2.position.x ? point1 : point2;
        }

        private static Transform FindRight(Transform point1, Transform point2)
        {
            return point1.position.x > point2.position.x ? point1 : point2;
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
            OnRespawn = null;
        }

        private void BeamPlayerTo(Vector3 target, Transform obj, float xOffset = 0.0f)
        {
            Vector3 offset = new Vector3(xOffset, 0, 0);
            obj.transform.position = target + offset;
        }

        //TODO Change for two players
        private Vector3 FindClosestSpawnPoint()
        {
            List<Transform> leftRespawnPoints = respawnPoints
                .FindAll(rp => rp.position.x <= Characters[Character.Pollin].transform.position.x);
            //Vector3 middlePoint =
            //    (Players[Character.Pollin].transform.position + Players[Character.Muni].transform.position) / 2;
            //float minDistance = respawnPoints.Min(rp => Vector3.Distance(middlePoint, rp.position));
            //Vector3 closest = respawnPoints.First(rp => Vector3.Distance(middlePoint, rp.position) == minDistance)
            //    .position;
            //return closest;
            float minDistance = leftRespawnPoints.Min(rp =>
                Vector3.Distance(Characters[Character.Pollin].transform.position, rp.position));
            Vector3 closest = leftRespawnPoints.First(rp =>
                    Vector3.Distance(Characters[Character.Pollin].transform.position, rp.position) == minDistance)
                .position;
            return closest;
        }

        private void GameOverScreen()
        {
            DisableFollowingCamera();
            DisablePlayerMovement();
            StartCoroutine(ShowDied());
        }

        private void InitializeBubbles()
        {
            SpeechBubbles = new Dictionary<Character, SpeechBubble>();
            InitializePlayerBubbles();
            InitializeNpcBubbles();
        }

        private void InitializeCutsceneStrings()
        {
            string completeString = textAsset.text;
            CutsceneStrings = completeString.Split('\n').ToList();
            CutsceneStringCounter = 0;
        }

        private void InitializeNpcBubbles()
        {
            GameObject dani = npcs.FirstOrDefault(npc => npc.name.Contains("Dani"));
            if (dani != null)
                SpeechBubbles.Add(Character.Dani, dani.GetComponentInChildren<SpeechBubble>());
        }

        private void InitializePlayerBubbles()
        {
            foreach (KeyValuePair<Character, Player> player in Characters)
            {
                SpeechBubbles.Add(player.Key, player.Value.GetComponentInChildren<SpeechBubble>());
            }
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

        private List<Transform> InitializeRespawnPoints()
        {
            return respawnPointParent != null ? respawnPointParent.GetComponentsInChildren<Transform>().ToList() : null;
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
        private void MoveObjectToTargetIfToFarAway(Transform obj, Vector3 target, float maxDistance = 20f)
        {
            if (Vector3.Distance(obj.position, target) > maxDistance)
                BeamPlayerTo(target, obj, -3f);
        }

        private void RespawnBoth()
        {
            Vector3 closest = FindClosestSpawnPoint();
            BeamPlayersTo(closest);
        }

        private IEnumerator ShowDied()
        {
            FadeSceneOut();
            yield return new WaitForSeconds(3);
            canvasText.color = Color.red;
            canvasText.text = "You Died";
            TextCanvas.SetActive(true);
            yield return new WaitForSeconds(5);
            ResetScene();
        }
    }
}