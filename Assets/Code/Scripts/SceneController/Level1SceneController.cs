using System.Collections;
using UnityEngine;

namespace Code.Scripts.SceneController
{
    public class Level1SceneController : BaseSceneController
    {
        protected override void Start()
        {
            base.Start();
            StartCoroutine(PlayOpeningCutscene());
        }

        private IEnumerator PlayOpeningCutscene()
        {
            yield return ShowNextTextSection(1);
            yield return ShowNextTextSection(1);
            yield return ShowNextTextSection(1);
            yield return ShowNextTextSection(1);
            UiCanvas.SetActive(false);
            GameElements.SetActive(true);
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            
            StartCoroutine(BubbleSpeak());
        }

        private IEnumerator BubbleSpeak()
        {
            SetUpSpeechBubble();
            yield return ShowNextBubbleText(2);
        }
    }
}