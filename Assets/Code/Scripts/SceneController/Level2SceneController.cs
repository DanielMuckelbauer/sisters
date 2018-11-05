using Code.Scripts.Entity;
using UnityEngine;

namespace Code.Scripts.SceneController
{
    public class Level2SceneController : BaseSceneController
    {
        public Transform EndbossSpawnPoint;

        public override void SceneTriggerEntered()
        {
            foreach (Player player in Players.Values)
            {
                player.transform.position = EndbossSpawnPoint.position;
            }
        }
    }
}