using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

namespace Code.Scripts.SceneController
{
    public class SceneController : MonoBehaviour
    {
        public TextAsset TextAsset;
        public TMP_Text Text;

        private List<string> cutsceneStrings;
        private int cutsceneStringCounter;

        protected void SetNextCutSceneString()
        {
            Text.text = cutsceneStrings[cutsceneStringCounter++];
        }

        protected void InitializeCutsceneStrings()
        {
            string completeString = TextAsset.text;
            cutsceneStrings = completeString.Split('\n').ToList();
        }
    }
}