using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

namespace Code.Scripts.SceneController
{
    public class HospitalSceneController : BaseSceneController
    {
        public GameObject Background;
        public GameObject Chicken;
        public GameObject Fetus;
        public GameObject Dani;
        public GameObject Girls;
        public GameObject SpeechBubble;
        public GameObject UiCanvas;
        public List<AudioClip> AudioClips;
        public AudioSource AudioPlayer;
        public Vector3 BirthPosition;
        public SpriteRenderer Stars;
        public Sprite SwordRoom;
        public Animator CameraAnimator;
        public Animator DaniAnimator;

        private List<GameObject> birthedObjects;
        private const float BirthForce = 250;

        private void Start()
        {
            StartCoroutine(PlayCutScene());
            birthedObjects = new List<GameObject>();
        }

        private IEnumerator ActivateBackground()
        {
            UiCanvas.SetActive(false);
            yield return new WaitForSeconds(1);
            Background.GetComponent<SpriteRenderer>().enabled = true;
        }

        private IEnumerator BubbleSpeek()
        {
            SetUpSpeechBubble();
            yield return FillSpeechbubble();
            yield return new WaitForSeconds(2);
            yield return FillSpeechbubble();
            yield return new WaitForSeconds(2);
        }

        private IEnumerator FillSpeechbubble()
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

        private IEnumerator GiveBirth()
        {
            yield return ThrowObject(Fetus);
            yield return ThrowObject(Chicken);
            yield return new WaitForSeconds(5);
        }

        private IEnumerator HospitalCutscene()
        {
            yield return ActivateBackground();
            AudioPlayer.Play();
            yield return new WaitForSeconds(3);
            yield return GiveBirth();
        }

        private IEnumerator PlayCutScene()
        {
            InitializeCutsceneStrings();
            yield return ShowNextTextSection(1);
            yield return ShowNextTextSection(1);
            yield return HospitalCutscene();
            ReactivateTextAndPlayIntroMusic();
            yield return ShowNextTextSection(1);
            yield return SwordRoomCutscene();
            yield return BubbleSpeek();
            DaniAnimator.SetTrigger("GiveSword");
            yield return new WaitForSeconds(5);
            CameraAnimator.SetTrigger("MoveCameraUp");
        }

        private void ReactivateTextAndPlayIntroMusic()
        {
            birthedObjects.ForEach(Destroy);
            AudioPlayer.Stop();
            AudioPlayer.clip = AudioClips[0];
            AudioPlayer.Play();
            Background.GetComponent<SpriteRenderer>().enabled = false;
            UiCanvas.SetActive(true);
        }

        private void SetUpSpeechBubble()
        {
            SpeechBubble.GetComponent<SpriteRenderer>().enabled = true;
            Text = SpeechBubble.GetComponentInChildren<TMP_Text>();
        }

        private IEnumerator SwordRoomCutscene()
        {
            Background.GetComponent<SpriteRenderer>().sprite = SwordRoom;
            yield return ActivateBackground();
            Stars.enabled = true;
            Dani.SetActive(true);
            Girls.SetActive(true);
            yield return new WaitForSeconds(2);
        }

        private IEnumerator ThrowObject(GameObject objectToThrow)
        {
            GameObject spawnedObject = Instantiate(objectToThrow, BirthPosition, new Quaternion());
            birthedObjects.Add(spawnedObject);
            Rigidbody2D rigidBody = spawnedObject.GetComponent<Rigidbody2D>();
            rigidBody.AddForce((Vector2.up + Vector2.left) * BirthForce);
            rigidBody.AddTorque(2, ForceMode2D.Impulse);
            yield return new WaitForSeconds(1.5f);
        }
    }
}