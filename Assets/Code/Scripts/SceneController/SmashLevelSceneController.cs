using System;
using Code.Classes;
using Code.Scripts.Entity;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Code.Scripts.SceneController
{
    public class SmashLevelSceneController : BaseSceneController
    {
        private Coroutine addSpawning;
        [SerializeField] private List<Transform> addSpawnPositions;
        [SerializeField] private AudioSource audioSource;
        [SerializeField] private DaniBoss dani;
        private Vector3 daniCameraPosition;
        [SerializeField] private List<Transform> flyAwayTargets;
        [SerializeField] private Transform flyTarget;
        [SerializeField] private AudioClip hook;
        private Stack<Action> phaseChangeMethods;
        [SerializeField] private GameObject portal;
        private List<GameObject> portals;
        private List<GameObject> spawnedSpiders;
        [SerializeField] private GameObject spider;

        protected override void Start()
        {
            base.Start();
            spawnedSpiders = new List<GameObject>();
            PlayOpeningCutScene();
            dani.OnNextPhase += ChangeFightPhase;
        }

        private void AfterFirstPhase()
        {
            StartCoroutine(FirstPhaseCutScene());
        }

        private void AfterSecondPhase()
        {
            DestroyAllSpidersAndPortals();
            StopCoroutine(addSpawning);
            StartCoroutine(SecondPhaseCutScene());
        }

        private void AfterThirdPhase()
        {
            StartCoroutine(ThirdPhaseCutscene());
        }

        private void ChangeFightPhase()
        {
            Action nextPhaseMethod = phaseChangeMethods.Pop();
            nextPhaseMethod.Invoke();
        }

        private void DestroyAllSpidersAndPortals()
        {
            spawnedSpiders.ForEach(s =>
            {
                if (s != null)
                    Destroy(s);
            });
            portals?.ForEach(p =>
            {
                if (p != null)
                    Destroy(p);
            });
        }

        private IEnumerator DisablePlayersAndMoveCameraToBoss()
        {
            DisableCameraAndMovement();
            Vector3 cameraTarget =
                new Vector3(flyTarget.position.x, flyTarget.position.y + 2, MainCamera.transform.position.z);
            StartCoroutine(MoveCameraSmoothly(cameraTarget));
            yield return new WaitForSeconds(3);
        }

        private IEnumerator FirstPhaseCutScene()
        {
            yield return DisablePlayersAndMoveCameraToBoss();
            yield return TextController.ShowCharactersNextBubbleText(Character.Dani, 3);
            yield return new WaitForSeconds(1);
            EnableCameraAndMovement();
            StartSecondPhase();
        }

        private IEnumerator FlyAway()
        {
            yield return FlyToFlyAwayTargets();
        }

        private IEnumerator FlyToFlyAwayTargets()
        {
            for (var i = 0; i < flyAwayTargets.Count; i++)
            {
                if (i == 1)
                    StartCoroutine(TextController.ShowCharactersNextBubbleText(Character.Dani));
                Transform target = flyAwayTargets[i];
                yield return RotateTowardsTarget(target);
                yield return EntityController.MoveTo(dani.transform, target, 1f);
            }

            yield return null;
        }

        private void InitializePhaseChangeMethods()
        {
            phaseChangeMethods = new Stack<Action>();
            phaseChangeMethods.Push(AfterThirdPhase);
            phaseChangeMethods.Push(AfterSecondPhase);
            phaseChangeMethods.Push(AfterFirstPhase);
        }

        private void MoveCameraToDani()
        {
            const int offset = 2;
            daniCameraPosition = new Vector3(dani.transform.position.x, dani.transform.position.y + offset,
                MainCamera.transform.position.z);
            MainCamera.transform.position = daniCameraPosition;
        }

        private void PlayHookMusic()
        {
            audioSource.Stop();
            audioSource.clip = hook;
            audioSource.Play();
        }

        private void PlayOpeningCutScene()
        {
            DisableCameraAndMovement();
            MoveCameraToDani();
            InitializePhaseChangeMethods();
            StartCoroutine(PlayOpeningCutscene(1, 2));
            StartCoroutine(PlayOpeningDialog());
        }

        private IEnumerator PlayOpeningDialog()
        {
            yield return new WaitForSeconds(1);
            yield return TextController.ShowCharactersNextBubbleText(Character.Dani, 2);
            dani.StartFight();
            yield return EntityController.MoveTo(dani.transform, flyTarget, 1f);
            yield return new WaitForSeconds(1);
            EnableCameraAndMovement();
        }

        private IEnumerator RotateTowardsTarget(Transform target)
        {
            Vector3 direction = target.position - dani.transform.position;
            dani.transform.rotation = Quaternion.LookRotation(
                Vector3.forward,
                direction
            );
            yield return null;
        }

        private IEnumerator SecondPhaseCutScene()
        {
            yield return DisablePlayersAndMoveCameraToBoss();
            yield return TextController.ShowCharactersNextBubbleText(Character.Dani, 3);
            yield return new WaitForSeconds(1);
            EnableCameraAndMovement();
            StartThirdPhase();
        }

        private IEnumerator SpawnAddsPeriodically()
        {
            while (true)
            {
                yield return new WaitForSeconds(2);
                Vector3 offset = new Vector3(Random.value * 5, 0, 0);
                portals = new List<GameObject>();
                addSpawnPositions.ForEach(
                    tr => portals.Add(Instantiate(portal, tr.position + offset, new Quaternion())));
                yield return new WaitForSeconds(2);
                addSpawnPositions.ForEach(tr =>
                    spawnedSpiders.Add(Instantiate(spider, tr.position + offset, new Quaternion())));
                yield return new WaitForSeconds(2);
                portals.ForEach(Destroy);
                yield return new WaitForSeconds(5);
            }
        }

        private void StartSecondPhase()
        {
            addSpawning = StartCoroutine(SpawnAddsPeriodically());
            dani.StartShooting();
        }

        private void StartThirdPhase()
        {
            dani.StartShooting();
            dani.StartShootingLaser();
            addSpawning = StartCoroutine(SpawnAddsPeriodically());
        }

        private IEnumerator ThirdPhaseCutscene()
        {
            if (addSpawning != null)
                StopCoroutine(addSpawning);
            DestroyAllSpidersAndPortals();
            dani.StopPatrolling();
            yield return DisablePlayersAndMoveCameraToBoss();
            yield return TextController.ShowCharactersNextBubbleText(Character.Dani, 6);
            PlayHookMusic();
            yield return new WaitForSeconds(5);
            dani.Animator.SetTrigger("FlyAway");
            yield return new WaitForSeconds(1);
            StartCoroutine(FlyAway());
        }
    }
}