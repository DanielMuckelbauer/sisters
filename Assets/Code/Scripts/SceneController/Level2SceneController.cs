using Code.Scripts.Entity;
using UnityEngine;

namespace Code.Scripts.SceneController
{
    public class Level2SceneController : BaseSceneController
    {
        public Transform EndbossSpawnPoint;

        protected override void Start()
        {
            base.Start();
            StartCoroutine(PlayOpeningCutscene(5, 2));
        }

        public override void SceneTriggerEntered()
        {


            foreach (Player player in Players.Values)
            {
                player.transform.position = EndbossSpawnPoint.position;
            }
        }
    }
}