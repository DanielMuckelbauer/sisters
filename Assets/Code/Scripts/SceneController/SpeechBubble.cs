using System;
using System.Collections;
using System.Collections.Generic;
using Code.Classes;
using Code.Scripts.Entity;
using TMPro;
using UnityEngine;

namespace Code.Scripts.SceneController
{
    public class SpeechBubble : MonoBehaviour
    {
        public TMP_Text BubbleText;

        protected IEnumerator FillSpeechbubble()
        {
            BubbleText.text = string.Empty;
            string bubbleText = BaseSceneController.CutsceneStrings[BaseSceneController.CutsceneStringCounter++];
            char[] charArray = bubbleText.ToCharArray();
            foreach (char c in charArray)
            {
                BubbleText.text += c;
                yield return new WaitForSeconds(0.06f);
            }
        }

        public IEnumerator ShowNextBubbleText(int times = 1)
        {
            EnableBubbleIfNecessary();
            for (int i = 0; i < times; i++)
            {
                BubbleText.text = string.Empty;
                yield return FillSpeechbubble();
                yield return new WaitForSeconds(2);
            }

            yield return new WaitForSeconds(2);
            HideSpeechBubble();
        }

        private void EnableBubbleIfNecessary()
        {
            if (!GetComponent<SpriteRenderer>().enabled)
                GetComponent<SpriteRenderer>().enabled = true;
        }

        private void HideSpeechBubble()
        {
            GetComponent<SpriteRenderer>().enabled = false;
            BubbleText.text = string.Empty;
        }
    }
}