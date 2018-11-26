using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Code.Classes;
using Code.Scripts.Entity;
using TMPro;
using UnityEngine;

namespace Code.Scripts.Scene
{
    public class TextController : MonoBehaviour
    {   
        public static int CutsceneStringCounter;
        public static List<string> CutsceneStrings;
        [SerializeField] private TMP_Text canvasText;
        [SerializeField] private SpriteRenderer pressAnyKeySprite;
        private Dictionary<Character, SpeechBubble> speechBubbleDict;
        [SerializeField] private List<SpeechBubble> allSpeechBubbles;
        [SerializeField] private TextAsset textAsset;
        [SerializeField] private GameObject textCanvas;
        public void ActivateCanvas(bool activate)
        {
            textCanvas.SetActive(activate);
        }

        public void SetCanvasTextToNextString()
        {
            canvasText.text = CutsceneStrings[CutsceneStringCounter++];
        }

        public IEnumerator ShowCharactersNextBubbleText(Character character, int times = 1)
        {
            yield return speechBubbleDict[character].ShowNextBubbleText(times);
        }

        public void ShowDiedText()
        {
            canvasText.color = Color.red;
            canvasText.text = "You Died";
            textCanvas.SetActive(true);
        }

        public void ShowPressAnyKey()
        {
            canvasText.text = string.Empty;
            textCanvas.SetActive(true);
            pressAnyKeySprite.enabled = true;
        }

        public void Start()
        {
            InitializeCutsceneStrings();
            InitializeBubbles();
        }

        private void AddToDictionary(SpeechBubble bubble)
        {
            switch (bubble.transform.parent.name)
            {
                case "Dani":
                    speechBubbleDict.Add(Character.Dani, bubble);
                    break;
                case "Muni":
                    speechBubbleDict.Add(Character.Muni, bubble);
                    break;
                case "Pollin":
                    speechBubbleDict.Add(Character.Pollin, bubble);
                    break;
                default:
                    break;
            }
        }

        private void InitializeBubbles()
        {
            speechBubbleDict = new Dictionary<Character, SpeechBubble>();
            allSpeechBubbles.ForEach(AddToDictionary);
        }

        private void InitializeCutsceneStrings()
        {
            string completeString = textAsset.text;
            CutsceneStrings = completeString.Split('\n').ToList();
            CutsceneStringCounter = 0;
        }
    }
}
