using Code.Classes;
using Code.Scripts.Entity;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Code.Scripts.SceneController
{
    public abstract class BaseSceneController : MonoBehaviour
    {
        public static int CutsceneStringCounter;
        public static List<string> CutsceneStrings;
        public TMP_Text CanvasText;
        public GameObject GameElements;
        public GameObject MainCamera;
        public List<GameObject> Npcs;
        public List<Player> PlayerList;
        public SpriteRenderer PressAnyKeySprite;
        public GameObject RespawnPointParent;
        public TextAsset TextAsset;
        public GameObject TextCanvas;
        protected Dictionary<Character, Player> Characters;
        protected Dictionary<Character, SpeechBubble> SpeechBubbles;
        private List<Transform> respawnPoints;
        public static event Action OnRespawn;

        public static void InvokeRespawnBoth()
        {
            OnRespawn?.Invoke();
        }

        public abstract void SceneTriggerEntered();

        protected void BeamPlayersTo(Transform target)
        {
            foreach (Player player in Characters.Values)
            {
                player.transform.position = target.position;
            }
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

        protected void EnableNextScene()
        {
            StartCoroutine(LoadNextSceneOnInput());
            CanvasText.text = string.Empty;
            TextCanvas.SetActive(true);
            PressAnyKeySprite.enabled = true;
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

        protected void MovePlayersToSpeakPosition(Transform point1, Transform point2)
        {
            Transform leftPoint = FindLeft(point1, point2);
            Transform rightPoint = FindRight(point1, point2);
            Transform leftPlayer = FindLeft(PlayerList[0].transform, PlayerList[1].transform);
            Transform rightPlayer = FindRight(PlayerList[0].transform, PlayerList[1].transform);
            leftPlayer.GetComponent<Player>().GoTo(leftPoint.position);
            rightPlayer.GetComponent<Player>().GoTo(rightPoint.position);
        }

        protected IEnumerator PlayOpeningCutscene(int time, int times)
        {
            yield return ShowNextTextSection(time, times);
            TextCanvas.SetActive(false);
            GameElements.SetActive(true);
        }

        protected void SetNextCutSceneString()
        {
            CanvasText.text = CutsceneStrings[CutsceneStringCounter++];
        }

        protected IEnumerator ShowNextTextSection(int time, int times = 1)
        {
            for (int i = 0; i < times; i++)
            {
                SetNextCutSceneString();
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

        private static void UnsubscribeAllDelegatesFromStaticEvents()
        {
            Player.ResetOnDie();
            Healer.ResetOnHealingConsumed();
            OnRespawn = null;
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
            string completeString = TextAsset.text;
            CutsceneStrings = completeString.Split('\n').ToList();
            CutsceneStringCounter = 0;
        }

        private void InitializeNpcBubbles()
        {
            GameObject dani = Npcs.FirstOrDefault(npc => npc.name.Contains("Dani"));
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
            if (PlayerList == null || PlayerList.Count == 0)
                return;
            Player muni = PlayerList.First(p => p.gameObject.name.Contains("Muni"));
            Characters.Add(Character.Muni, muni);
            Player pollin = PlayerList.First(p => p.gameObject.name.Contains("Pollin"));
            Characters.Add(Character.Pollin, pollin);
        }

        private List<Transform> InitializeRespawnPoints()
        {
            return RespawnPointParent != null ? RespawnPointParent.GetComponentsInChildren<Transform>().ToList() : null;
        }

        private IEnumerator LoadNextSceneOnInput()
        {
            while (!Input.anyKeyDown)
            {
                yield return null;
            }

            UnsubscribeAllDelegatesFromStaticEvents();
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }

        private void RespawnBoth()
        {
            Vector3 closest = FindClosestSpawnPoint();
            Vector3 offset = new Vector3(1, 0, 0);
            Characters[Character.Pollin].transform.position = closest + offset;
            Characters[Character.Muni].transform.position = closest - offset;
        }

        private IEnumerator ShowDied()
        {
            FadeSceneOut();
            yield return new WaitForSeconds(3);
            CanvasText.color = Color.red;
            CanvasText.text = "You Died";
            TextCanvas.SetActive(true);
            yield return new WaitForSeconds(5);
            ResetScene();
        }
    }
}