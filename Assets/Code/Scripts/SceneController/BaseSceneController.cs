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

        protected List<string> CutsceneStrings;
        protected int CutsceneStringCounter;

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

        protected IEnumerator ShowNextTextSection(int time)
        {
            SetNextCutSceneString();
            yield return new WaitForSeconds(time);
        }
    }
}