using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using TMPro;


namespace Code.Scripts
{
    public class HospitalScene : MonoBehaviour
    {
        public GameObject Chicken;
        public GameObject UiCanvas;
        public GameObject Background;
        public Vector3 BirthPosition;
        public AudioSource Beep;
        public TextAsset TextAsset;
        public TMP_Text Text;

        private GameObject chicken;
        private const float MoveForce = 250;
        private List<string> cutSceneStrings;
        private int cutSceneStringCounter = 0;

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

        private void SetNextCutSceneString()
        {
            Text.text = cutSceneStrings[cutSceneStringCounter++];
        }
        private void InitializeCutsceneStrings()
        {
            string completeString = TextAsset.text;
            cutSceneStrings = completeString.Split('\n').ToList();
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