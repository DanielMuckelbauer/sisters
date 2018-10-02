using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

namespace Code.Scripts.SceneController
{
    public class HospitalSceneController : BaseSceneController
    {
        public GameObject Chicken;
        public GameObject UiCanvas;
        public GameObject Background;
        public GameObject SpeechBubble;
        public SpriteRenderer Stars;
        public Sprite SwordRoom;
        public List<SpriteRenderer> SwordAndStars;
        public AudioSource AudioPlayer;
        public Vector3 BirthPosition;

        private GameObject chicken;
        private const float BirthForce = 250;

        private void Start()
        {
            StartCoroutine(PlayCutScene());
        }

        private IEnumerator PlayCutScene()
        {
            InitializeCutsceneStrings();
            yield return ShowNextTextSection(5);
            yield return ShowNextTextSection(6);
            yield return HospitalCutscene();
            DeactivateBackground();
            yield return ShowNextTextSection(5);
            yield return SwordRoomCutscene();
            SetUpSpeechBubble();
            yield return FillSpeechbubble();
            yield return new WaitForSeconds(2);
            yield return FillSpeechbubble();
            yield return new WaitForSeconds(2);
            SpawnSwords();
        }

        private void SpawnSwords()
        {
            SwordAndStars.ForEach(s => s.enabled = true);
        }

        private IEnumerator FillSpeechbubble()
        {
            Text.text = string.Empty;
            string bubbleText = CutsceneStrings[CutsceneStringCounter++];
            char[] charArray = bubbleText.ToCharArray();
            foreach (char c in charArray)
            {
                Text.text += c;
                yield return new WaitForSeconds(0.1f);
            }
        }

        private void SetUpSpeechBubble()
        {
            SpeechBubble.GetComponent<SpriteRenderer>().enabled = true;
            Text = SpeechBubble.GetComponentInChildren<TMP_Text>();
        }

        private IEnumerator HospitalCutscene()
        {
            yield return ActivateBackground();
            AudioPlayer.Play();
            yield return new WaitForSeconds(3);
            yield return GiveBirth();
        }

        private IEnumerator GiveBirth()
        {
            chicken = Instantiate(Chicken, BirthPosition, new Quaternion());
            Rigidbody2D rigidBody = chicken.GetComponent<Rigidbody2D>();
            rigidBody.AddForce((Vector2.up + Vector2.left) * BirthForce);
            yield return new WaitForSeconds(5);
        }

        private IEnumerator SwordRoomCutscene()
        {
            Background.GetComponent<SpriteRenderer>().sprite = SwordRoom;
            yield return ActivateBackground();
            Stars.enabled = true;
            yield return new WaitForSeconds(2);
        }

        private IEnumerator ActivateBackground()
        {
            UiCanvas.SetActive(false);
            yield return new WaitForSeconds(1);
            Background.SetActive(true);
        }

        private void DeactivateBackground()
        {
            Destroy(chicken);
            AudioPlayer.Stop();
            Background.SetActive(false);
            UiCanvas.SetActive(true);
        }
    }
}