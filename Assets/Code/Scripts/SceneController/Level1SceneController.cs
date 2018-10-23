using System.Collections;
using Code.Scripts.Entity;
using UnityEngine;

namespace Code.Scripts.SceneController
{
    public class Level1SceneController : BaseSceneController
    {
        public BossEnemy Boss;
        private bool fightStarted;

        protected override void Start()
        {
            base.Start();
            StartCoroutine(PlayOpeningCutscene());
        }

        private IEnumerator PlayOpeningCutscene()
        {
            yield return ShowNextTextSection(2);
            yield return ShowNextTextSection(2);
            yield return ShowNextTextSection(2);
            UiCanvas.SetActive(false);
            GameElements.SetActive(true);
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (fightStarted || !other.gameObject.tag.Contains("Player"))
                return;
            Boss.StartBossFight();
            fightStarted = true;
        }
    }
}