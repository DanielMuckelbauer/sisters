using System.Collections;
using UnityEngine;

namespace Code.Scripts.SceneController
{
    public class HospitalSceneController : BaseSceneController
    {
        public GameObject Chicken;
        public GameObject UiCanvas;
        public GameObject Background;
        public Sprite SwordRoom;
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
            yield return ShowNextTextSection(5);
            yield return ActivateBackground();
            AudioPlayer.Play();
            yield return new WaitForSeconds(3);
            yield return GiveBirth();
            DeactivateBackground();
            yield return ShowNextTextSection(5);
            Background.GetComponent<SpriteRenderer>().sprite = SwordRoom;
            yield return ActivateBackground();
        }

        private IEnumerator ActivateBackground()
        {
            UiCanvas.SetActive(false);
            yield return new WaitForSeconds(1);
            Background.SetActive(true);
        }

        private IEnumerator GiveBirth()
        {
            chicken = Instantiate(Chicken, BirthPosition, new Quaternion());
            Rigidbody2D rigidBody = chicken.GetComponent<Rigidbody2D>();
            rigidBody.AddForce((Vector2.up + Vector2.left) * BirthForce);
            yield return new WaitForSeconds(5);
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