using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Code.Scripts.SceneController
{
    public class IntroSceneController : BaseSceneController
    {
        public GameObject Chicken;
        public GameObject Fetus;
        public GameObject Dani;
        public GameObject Girls;
        public GameObject SpeechBubble;
        public List<AudioClip> AudioClips;
        public AudioSource AudioPlayer;
        public Vector3 BirthPosition;
        public SpriteRenderer Stars;
        public Sprite SwordRoom;
        public SpriteRenderer Title;
        public Animator CameraAnimator;
        public Animator DaniAnimator;
        public Transform Sword;
        public Transform SwordTarget;

        private List<GameObject> birthedObjects;
        private const float BirthForce = 250;
        private bool moveSword;

        private void Update()
        {
            MoveSwordDown();
        }

        protected override void Start()
        {
            base.Start();
            StartCoroutine(PlayCutScene());
            birthedObjects = new List<GameObject>();
        }

        private IEnumerator ActivateBackground()
        {
            UiCanvas.SetActive(false);
            yield return new WaitForSeconds(1);
            GameElements.GetComponent<SpriteRenderer>().enabled = true;
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
            yield return ShowNextTextSection(1);
            yield return ShowNextTextSection(1);
            yield return HospitalCutscene();
            AudioPlayer.Stop();
            ReactivateText();
            yield return ShowNextTextSection(1);
            PlayIntroMusic();
            yield return SwordRoomCutscene();
            yield return BubbleSpeek();
            DaniAnimator.SetTrigger("GiveSword");
            yield return new WaitForSeconds(5);
            CameraAnimator.SetTrigger("MoveCameraUp");
            yield return new WaitForSeconds(5);
            StartCoroutine(MakeTitleAppear());
            moveSword = true;
        }

        private void MoveSwordDown()
        {
            if (!moveSword)
                return;
            const float speed = 4.5f;
            if (Sword.position.y <= SwordTarget.position.y)
                moveSword = false;
            float step = speed * Time.deltaTime;
            Sword.position = Vector3.MoveTowards(Sword.position, SwordTarget.position, step);
        }

        private void ReactivateText()
        {
            birthedObjects.ForEach(Destroy);
            GameElements.GetComponent<SpriteRenderer>().enabled = false;
            UiCanvas.SetActive(true);
        }

        private void PlayIntroMusic()
        {
            AudioPlayer.clip = AudioClips[0];
            AudioPlayer.Play();
        }

        private void SetUpSpeechBubble()
        {
            SpeechBubble.GetComponent<SpriteRenderer>().enabled = true;
            Text = SpeechBubble.GetComponentInChildren<TMP_Text>();
        }

        private IEnumerator SwordRoomCutscene()
        {
            GameElements.GetComponent<SpriteRenderer>().sprite = SwordRoom;
            yield return ActivateBackground();
            Stars.enabled = true;
            Dani.SetActive(true);
            Girls.SetActive(true);
            yield return new WaitForSeconds(3);
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

        private IEnumerator MakeTitleAppear()
        {
            const float duration = 5.0f;
            float t = 0;
            float startTime = Time.time;
            while (t < 1)
            {
                t = (Time.time - startTime) / duration;
                Title.color = new Color(1f, 1f, 1f, Mathf.SmoothStep(0, 1, t));
                yield return new WaitForSeconds(0.2f);
            }
            yield return new WaitForSeconds(3);
            SceneManager.LoadScene("Level1");
        }
    }
}