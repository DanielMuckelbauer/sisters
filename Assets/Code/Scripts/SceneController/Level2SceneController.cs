using System.Collections;
using Code.Classes;
using Code.Scripts.Entity;
using UnityEngine;

namespace Code.Scripts.SceneController
{
    public class Level2SceneController : BaseSceneController
    {
        public Transform EndbossSpawnPoint;

        public override void SceneTriggerEntered()
        {
            DisablePlayerMovement();
            StartCoroutine(TalkingCutScene());
        }

        protected override void Start()
        {
            base.Start();
            StartCoroutine(PlayOpeningCutscene(5, 2));
        }
        private IEnumerator TalkingCutScene()
        {
            SpeechBubble munisBubble = SpeechBubbles[Character.Muni];
            SpeechBubble pollinsBubble = SpeechBubbles[Character.Pollin];
            munisBubble.ShowSpeechBubble();
            yield return munisBubble.ShowNextBubbleText();
            pollinsBubble.ShowSpeechBubble();
            yield return pollinsBubble.ShowNextBubbleText();
            munisBubble.HideSpeechBubble();
            pollinsBubble.HideSpeechBubble();
            foreach (Player player in Characters.Values)
            {
                player.transform.position = EndbossSpawnPoint.position;
            }

            EnablePlayerMovement();
        }
    }
}