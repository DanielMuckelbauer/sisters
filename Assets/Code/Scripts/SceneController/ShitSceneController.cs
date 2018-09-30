using Code.Scripts.Entity;
using UnityEngine;

namespace Code.Scripts.SceneController
{
    public class ShitSceneController : BaseSceneController
    {
        public BossEnemy Boss;
        private bool fightStarted = false;

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (fightStarted || !other.gameObject.tag.Contains("Player"))
                return;
            Boss.StartBossFight();
            fightStarted = true;
        }
    }
}