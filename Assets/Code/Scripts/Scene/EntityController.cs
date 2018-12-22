using System;
using Code.Classes;
using Code.Scripts.Entity;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Code.Scripts.Scene
{
    public class EntityController : MonoBehaviour
    {
        [SerializeField] private Dictionary<Character, Player> characters;
        [SerializeField] private GameObject respawnPointParent;
        private List<Transform> respawnPoints;
        public List<Player> PlayerList;

        public void Awake()
        {
            respawnPoints = InitializeRespawnPoints();
            InitializeCharacterDictionary();
        }

        public void BeamPlayersTo(Vector3 target)
        {
            BeamPlayerTo(target, PlayerList[0].transform, -0.5f);
            BeamPlayerTo(target, PlayerList[1].transform, 0.5f);
        }

        public void DisablePlayerMovement()
        {
            foreach (Player player in characters.Values)
                player.SetMovement(false);
        }

        public void EnablePlayerMovement()
        {
            foreach (Player player in characters.Values)
                player.SetMovement(true);
        }

        public Player GetCharacter(Character character)
        {
            return characters[character];
        }

        public IEnumerator MovePlayersToOppositePositions(Transform point1, Transform point2)
        {
            Transform leftPoint = FindLeft(point1, point2);
            Transform rightPoint = FindRight(point1, point2);
            Transform leftPlayer = FindLeft(PlayerList[0].transform, PlayerList[1].transform);
            Transform rightPlayer = FindRight(PlayerList[0].transform, PlayerList[1].transform);
            PlayerList.ForEach(p => MoveObjectToTargetIfToFarAway(leftPlayer, rightPlayer.position));
            yield return leftPlayer.GetComponent<Player>().GoTo(leftPoint.position);
            yield return rightPlayer.GetComponent<Player>().GoTo(rightPoint.position);
            yield return PlayerList[0].TurnTo(PlayerList[1].transform.position);
            yield return PlayerList[1].TurnTo(PlayerList[0].transform.position);
            yield return new WaitForSeconds(0.3f);
            PlayerList.ForEach(player => player.GetComponent<Rigidbody2D>().velocity = Vector2.zero);
        }

        public IEnumerator MoveTo(Transform obj, Transform target, float stepSize = 3f)
        {
            while (obj.position != target.position)
            {
                float step = stepSize * Time.deltaTime;
                obj.position = Vector3.MoveTowards(obj.position, target.position, step);
                yield return null;
            }
        }

        public void RespawnBoth()
        {
            Vector3 closest = FindClosestLeftSpawnPoint();
            BeamPlayersTo(closest);
        }

        public void RespawnOne(GameObject player)
        {
            Vector3 closest = FindClosestRespawnPoint(respawnPoints);
            BeamPlayerTo(closest, player.transform);
        }

        private static Transform FindLeft(Transform point1, Transform point2)
        {
            return point1.position.x < point2.position.x ? point1 : point2;
        }

        private static Transform FindRight(Transform point1, Transform point2)
        {
            return point1.position.x > point2.position.x ? point1 : point2;
        }

        private void BeamPlayerTo(Vector3 target, Transform obj, float xOffset = 0.0f)
        {
            Vector3 offset = new Vector3(xOffset, 0, 0);
            obj.transform.position = target + offset;
        }

        private Vector3 FindClosestLeftSpawnPoint()
        {
            try
            {
                List<Transform> leftRespawnPoints = respawnPoints
                    .FindAll(rp => rp.position.x <= characters[Character.Pollin].transform.position.x);
                Vector3 closest = FindClosestRespawnPoint(leftRespawnPoints);
                return closest;
            }
            catch (Exception e)
            {
                Debug.Log(e);
                Transform leftest = respawnPoints[0];
                foreach (Transform respawnPoint in respawnPoints)
                {
                    if (respawnPoint.position.x < leftest.position.x)
                        leftest = respawnPoint;
                }

                return leftest.position;
            }
        }

        //TODO Change for two players
        private Vector3 FindClosestRespawnPoint(List<Transform> transforms)
        {
            Vector3 middlePoint =
                (characters[Character.Pollin].transform.position + characters[Character.Muni].transform.position) / 2;
            float minDistance = transforms.Min(rp => Vector3.Distance(middlePoint, rp.position));
            Vector3 closest = transforms.First(rp => Vector3.Distance(middlePoint, rp.position) == minDistance)
                .position;
            return closest;
            //float minDistance = transforms.Min(rp =>
            //    Vector3.Distance(characters[Character.Pollin].transform.position, rp.position));
            //Vector3 closest = transforms.First(rp =>
            //        Vector3.Distance(characters[Character.Pollin].transform.position, rp.position) == minDistance)
            //    .position;
            //return closest;
        }

        private void InitializeCharacterDictionary()
        {
            characters = new Dictionary<Character, Player>();
            if (PlayerList == null || PlayerList.Count == 0)
                return;
            Player muni = PlayerList.First(p => p.gameObject.name.Contains("Muni"));
            characters.Add(Character.Muni, muni);
            Player pollin = PlayerList.First(p => p.gameObject.name.Contains("Pollin"));
            characters.Add(Character.Pollin, pollin);
        }

        private List<Transform> InitializeRespawnPoints()
        {
            return respawnPointParent != null ? respawnPointParent.GetComponentsInChildren<Transform>().ToList() : null;
        }

        private void MoveObjectToTargetIfToFarAway(Transform obj, Vector3 target, float maxDistance = 3f)
        {
            if (Vector3.Distance(obj.position, target) > maxDistance)
                obj.transform.position = target - new Vector3(1, 0, 0);
        }
    }
}