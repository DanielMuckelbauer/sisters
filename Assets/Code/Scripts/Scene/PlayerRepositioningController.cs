using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Code.Classes;
using Code.Scripts.Entity;
using UnityEngine;

namespace Code.Scripts.Scene
{
    public class PlayerRepositioningController : MonoBehaviour
    {
        [SerializeField] private Dictionary<Character, Player> characters;
        [SerializeField] private List<Player> playerList;
        [SerializeField] private GameObject respawnPointParent;
        private List<Transform> respawnPoints;

        public void BeamPlayersTo(Vector3 target)
        {
            BeamPlayerTo(target, playerList[0].transform, -0.5f);
            BeamPlayerTo(target, playerList[1].transform, 0.5f);
        }

        public IEnumerator MovePlayersToSpeakPosition(Transform point1, Transform point2)
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

        public void Start()
        {
            respawnPoints = InitializeRespawnPoints();
            InitializeCharacterDictionary();
        }

        private void InitializeCharacterDictionary()
        {
            characters = new Dictionary<Character, Player>();
            if (playerList == null || playerList.Count == 0)
                return;
            Player muni = playerList.First(p => p.gameObject.name.Contains("Muni"));
            characters.Add(Character.Muni, muni);
            Player pollin = playerList.First(p => p.gameObject.name.Contains("Pollin"));
            characters.Add(Character.Pollin, pollin);
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

        //TODO Change for two players
        private Vector3 FindClosestLeftSpawnPoint()
        {
            List<Transform> leftRespawnPoints = respawnPoints
                .FindAll(rp => rp.position.x <= characters[Character.Pollin].transform.position.x);
            Vector3 closest = FindClosestRespawnPoint(leftRespawnPoints);
            return closest;
        }

        private Vector3 FindClosestRespawnPoint(List<Transform> transforms)
        {
            //Vector3 middlePoint =
            //    (Players[Character.Pollin].transform.position + Players[Character.Muni].transform.position) / 2;
            //float minDistance = RespawnPoints.Min(rp => Vector3.Distance(middlePoint, rp.position));
            //Vector3 closest = RespawnPoints.First(rp => Vector3.Distance(middlePoint, rp.position) == minDistance)
            //    .position;
            //return closest;
            float minDistance = transforms.Min(rp =>
                Vector3.Distance(characters[Character.Pollin].transform.position, rp.position));
            Vector3 closest = transforms.First(rp =>
                    Vector3.Distance(characters[Character.Pollin].transform.position, rp.position) == minDistance)
                .position;
            return closest;
        }
        private List<Transform> InitializeRespawnPoints()
        {
            return respawnPointParent != null ? respawnPointParent.GetComponentsInChildren<Transform>().ToList() : null;
        }

        private void MoveObjectToTargetIfToFarAway(Transform obj, Vector3 target, float maxDistance = 20f)
        {
            if (Vector3.Distance(obj.position, target) > maxDistance)
                obj.transform.position = target;
        }

    }
}
