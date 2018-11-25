using System.Collections.Generic;
using System.Linq;
using Code.Classes;
using Code.Scripts.Entity;
using UnityEngine;

namespace Code.Scripts.Scene
{
    public class RespawnController : MonoBehaviour
    {
        [SerializeField] protected Dictionary<Character, Player> Characters;
        protected List<Transform> RespawnPoints;
        [SerializeField] private List<Player> playerList;
        [SerializeField] private GameObject respawnPointParent;

        public void RespawnBoth()
        {
            Vector3 closest = FindClosestLeftSpawnPoint();
            BeamPlayersTo(closest);
        }

        public void RespawnOne(GameObject player)
        {
            Vector3 closest = FindClosestRespawnPoint(RespawnPoints);
            BeamPlayerTo(closest, player.transform);
        }

        public void Start()
        {
            RespawnPoints = InitializeRespawnPoints();
        }

        public void BeamPlayersTo(Vector3 target)
        {
            BeamPlayerTo(target, playerList[0].transform, -0.5f);
            BeamPlayerTo(target, playerList[1].transform, 0.5f);
        }

        //TODO Change for two players
        protected Vector3 FindClosestLeftSpawnPoint()
        {
            List<Transform> leftRespawnPoints = RespawnPoints
                .FindAll(rp => rp.position.x <= Characters[Character.Pollin].transform.position.x);
            Vector3 closest = FindClosestRespawnPoint(leftRespawnPoints);
            return closest;
        }

        protected Vector3 FindClosestRespawnPoint(List<Transform> transforms)
        {
            //Vector3 middlePoint =
            //    (Players[Character.Pollin].transform.position + Players[Character.Muni].transform.position) / 2;
            //float minDistance = RespawnPoints.Min(rp => Vector3.Distance(middlePoint, rp.position));
            //Vector3 closest = RespawnPoints.First(rp => Vector3.Distance(middlePoint, rp.position) == minDistance)
            //    .position;
            //return closest;
            float minDistance = transforms.Min(rp =>
                Vector3.Distance(Characters[Character.Pollin].transform.position, rp.position));
            Vector3 closest = transforms.First(rp =>
                    Vector3.Distance(Characters[Character.Pollin].transform.position, rp.position) == minDistance)
                .position;
            return closest;
        }

        private void BeamPlayerTo(Vector3 target, Transform obj, float xOffset = 0.0f)
        {
            Vector3 offset = new Vector3(xOffset, 0, 0);
            obj.transform.position = target + offset;
        }

        private List<Transform> InitializeRespawnPoints()
        {
            return respawnPointParent != null ? respawnPointParent.GetComponentsInChildren<Transform>().ToList() : null;
        }
    }
}
