using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

namespace Code.Scripts.SceneController
{
    public class BaseSceneController : MonoBehaviour
    {
        public TextAsset TextAsset;
        public TMP_Text Text;
        public GameObject UiCanvas;
        public GameObject GameElements;
        public GameObject SpeechBubble;
        public GameObject MainCamera;

        protected List<string> CutsceneStrings;
        protected int CutsceneStringCounter;

        /// <summary>
        /// Initializes the cutscene strings from the provided textfile
        /// </summary>
        protected virtual void Start()
        {
            InitializeCutsceneStrings();
        }

        protected void SetNextCutSceneString()
        {
            Text.text = CutsceneStrings[CutsceneStringCounter++];
        }

        private void InitializeCutsceneStrings()
        {
            string completeString = TextAsset.text;
            CutsceneStrings = completeString.Split('\n').ToList();
        }

        protected IEnumerator ShowNextTextSection(int time, int times = 1)
        {
            for (int i = 0; i < times; i++)
            {
                SetNextCutSceneString();
                yield return new WaitForSeconds(time);
            }
        }

        protected IEnumerator MoveCamera(Vector3 targetPosition)
        {
            Transform cameraTransform = MainCamera.transform;
            const float smoothTime = 4;
            Vector3 velocity = Vector3.zero;
            while (cameraTransform.position != targetPosition)
            {
                cameraTransform.position = Vector3.SmoothDamp(cameraTransform.position, targetPosition,
                    ref velocity, smoothTime);
                yield return null;
            }
        }

        protected IEnumerator FillSpeechbubble()
        {
            Text.text = string.Empty;
            string bubbleText = CutsceneStrings[CutsceneStringCounter++];
            char[] charArray = bubbleText.ToCharArray();
            foreach (char c in charArray)
            {
                Text.text += c;
                yield return new WaitForSeconds(0.08f);
            }
        }

        protected void SetUpSpeechBubble()
        {
            if (SpeechBubble == null)
                return;
            SpeechBubble.GetComponent<SpriteRenderer>().enabled = true;
            Text = SpeechBubble.GetComponentInChildren<TMP_Text>();
        }

        protected IEnumerator ShowNextBubbleText(int times = 1)
        {
            for (int i = 0; i < times; i++)
            {
                yield return FillSpeechbubble();
                yield return new WaitForSeconds(2);
            }
        }
    }
}