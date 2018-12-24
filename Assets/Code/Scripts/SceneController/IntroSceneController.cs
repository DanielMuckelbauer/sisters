using Code.Classes;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
            TextController.ActivateCanvas(false);
            yield return new WaitForSecondsRealtime(1);
            GameElements.GetComponent<SpriteRenderer>().enabled = true;
        }

        private IEnumerator GiveBirth()
        {
            yield return ThrowObject(Fetus);
            yield return ThrowObject(Chicken);
            yield return new WaitForSecondsRealtime(5);
        }

        private IEnumerator HospitalCutscene()
        {
            yield return ActivateBackground();
            AudioPlayer.Play();
            yield return new WaitForSecondsRealtime(3);
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
            yield return ShowNextTextSection(4, 2);
            yield return HospitalCutscene();
            AudioPlayer.Stop();
            PlayIntroMusic();
            ReactivateText();
            yield return ShowNextTextSection(5);
            yield return SwordRoomCutscene();
            yield return TextController.ShowCharactersNextBubbleText(Character.Dani, 2);
            DaniAnimator.SetTrigger("GiveSword");
            yield return new WaitForSecondsRealtime(7);
            cameraAnimator.SetTrigger("MoveCameraUp");
            yield return new WaitForSecondsRealtime(6.3f);
            StartCoroutine(Fade(Title, 0, 1));
            moveSword = true;
            yield return new WaitForSecondsRealtime(6);
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
            TextController.ActivateCanvas(true);
        }

        private IEnumerator SwordRoomCutscene()
        {
            GameElements.GetComponent<SpriteRenderer>().sprite = SwordRoom;
            yield return ActivateBackground();
            Stars.enabled = true;
            Dani.SetActive(true);
            Girls.SetActive(true);
            yield return new WaitForSecondsRealtime(2.8f);
        }

        private IEnumerator ThrowObject(GameObject objectToThrow)
        {
            GameObject spawnedObject = Instantiate(objectToThrow, BirthPosition, new Quaternion());
            birthedObjects.Add(spawnedObject);
            Rigidbody2D rigidBody = spawnedObject.GetComponent<Rigidbody2D>();
            rigidBody.AddForce((Vector2.up + Vector2.left) * BirthForce);
            rigidBody.AddTorque(2, ForceMode2D.Impulse);
            yield return new WaitForSecondsRealtime(1.5f);
        }

        private void Update()
        {
            MoveSwordDown();
        }
    }
}