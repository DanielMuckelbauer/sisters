using System.Collections;
using UnityEngine;

namespace Code.Scripts.SceneController
{
    public class HospitalBaseSceneController : BaseSceneController
    {
        public GameObject Chicken;
        public GameObject UiCanvas;
        public GameObject Background;
        public Vector3 BirthPosition;
        public AudioSource Beep;

        private GameObject chicken;
        private const float MoveForce = 250;

        private void Start()
        {
            StartCoroutine(PlayCutScene());
        }

        private IEnumerator PlayCutScene()
        {
            InitializeCutsceneStrings();
            yield return ActivateBackground();
            yield return GiveBirth();
            yield return AfterBirth();
        }

        private IEnumerator ActivateBackground()
        {
            SetNextCutSceneString();
            yield return new WaitForSeconds(5);
            UiCanvas.SetActive(false);
            yield return new WaitForSeconds(1);
            Background.SetActive(true);
            Beep.Play();
            yield return new WaitForSeconds(3);
        }

        private IEnumerator GiveBirth()
        {
            chicken = Instantiate(Chicken, BirthPosition, new Quaternion());
            Rigidbody2D rigidBody = chicken.GetComponent<Rigidbody2D>();
            rigidBody.AddForce((Vector2.up + Vector2.left) * MoveForce);
            yield return new WaitForSeconds(5);
        }

        private IEnumerator AfterBirth()
        {
            SetNextCutSceneString();
            Destroy(chicken);
            Beep.Stop();
            Background.SetActive(false);
            UiCanvas.SetActive(true);
            yield return new WaitForSeconds(5);
        }
    }
}