using Code.Scripts.Entity;
using UnityEngine;

namespace Code.Scripts.SceneController
{
    public class Level1SceneController : BaseSceneController
    {
        public BossEnemy Boss;
        private bool fightStarted;

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (fightStarted || !other.gameObject.tag.Contains("Player"))
                return;
            Boss.StartBossFight();
            fightStarted = true;
        }
    }
}