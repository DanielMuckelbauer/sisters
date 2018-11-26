using System.Collections;
using Code.Scripts.SceneController;
using TMPro;
using UnityEngine;

namespace Code.Scripts.Scene
{
    public class SpeechBubble : MonoBehaviour
    {
        [SerializeField] private TMP_Text bubbleText;

        public IEnumerator ShowNextBubbleText(int times = 1)
        {
            EnableBubbleIfNecessary();
            for (int i = 0; i < times; i++)
            {
                bubbleText.text = string.Empty;
                yield return FillSpeechbubble();
                yield return new WaitForSeconds(2);
            }

            HideSpeechBubble();
        }

        private IEnumerator FillSpeechbubble()
        {
            bubbleText.text = string.Empty;
            string nextBubbleText = TextController.CutsceneStrings[TextController.CutsceneStringCounter++];
            char[] charArray = nextBubbleText.ToCharArray();
            foreach (char c in charArray)
            {
                bubbleText.text += c;
                //TODO Change in prod
                //yield return new WaitForSeconds(0.06f);
                yield return new WaitForSeconds(0.01f);
            }
        }

        private void EnableBubbleIfNecessary()
        {
            if (!GetComponent<SpriteRenderer>().enabled)
                GetComponent<SpriteRenderer>().enabled = true;
        }

        private void HideSpeechBubble()
        {
            GetComponent<SpriteRenderer>().enabled = false;
            bubbleText.text = string.Empty;
        }
    }
}