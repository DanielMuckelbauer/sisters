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
        protected List<string> CutsceneStrings;
        protected int CutsceneStringCounter;

        protected void SetNextCutSceneString()
        {
            Text.text = CutsceneStrings[CutsceneStringCounter++];
        }

        protected void InitializeCutsceneStrings()
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