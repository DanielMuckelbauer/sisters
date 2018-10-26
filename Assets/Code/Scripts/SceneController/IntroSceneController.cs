using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Code.Scripts.SceneController
{
    public class IntroSceneController : BaseSceneController
    {
        public List<AudioClip> AudioClips;
        public AudioSource AudioPlayer;
        public Vector3 BirthPosition;
        public GameObject Chicken;
        public GameObject Dani;
        public Animator DaniAnimator;
        public GameObject Fetus;
        public GameObject Girls;
        public SpriteRenderer Stars;
        public Transform Sword;
        public Sprite SwordRoom;
        public Transform SwordTarget;
        public SpriteRenderer Title;
        private const float BirthForce = 250;
        private List<GameObject> birthedObjects;
        private Animator cameraAnimator;
        private bool moveSword;

        protected override void Start()
        {
            base.Start();
            cameraAnimator = MainCamera.GetComponent<Animator>();
            StartCoroutine(PlayCutScene());
            birthedObjects = new List<GameObject>();
        }

        private IEnumerator ActivateBackground()
        {
            UiCanvas.SetActive(false);
            yield return new WaitForSeconds(1);
            GameElements.GetComponent<SpriteRenderer>().enabled = true;
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

        private IEnumerator PlayCutScene()
        {
            yield return ShowNextTextSection(5, 2);
            yield return HospitalCutscene();
            AudioPlayer.Stop();
            ReactivateText();
            yield return ShowNextTextSection(5);
            PlayIntroMusic();
            yield return SwordRoomCutscene();
            SetUpSpeechBubble();
            yield return ShowNextBubbleText(2);
            DaniAnimator.SetTrigger("GiveSword");
            yield return new WaitForSeconds(5);
            cameraAnimator.SetTrigger("MoveCameraUp");
            yield return new WaitForSeconds(5);
            StartCoroutine(Fade(Title, 0, 1));
            moveSword = true;
            yield return new WaitForSeconds(8);
            EnableNextScene();
        }

        private void PlayIntroMusic()
        {
            AudioPlayer.clip = AudioClips[0];
            AudioPlayer.Play();
        }

        private void ReactivateText()
        {
            birthedObjects.ForEach(Destroy);
            GameElements.GetComponent<SpriteRenderer>().enabled = false;
            UiCanvas.SetActive(true);
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

        private void Update()
        {
            MoveSwordDown();
        }
    }
}