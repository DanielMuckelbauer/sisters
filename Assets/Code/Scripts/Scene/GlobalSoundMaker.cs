using Code.Scripts.Entity;
using UnityEngine;

namespace Code.Scripts.Scene
{
    public class GlobalSoundMaker : MonoBehaviour
    {
        [SerializeField] private AudioSource source;
        [SerializeField] private AudioClip getHit;

        private void Start()
        {
            BasePatrollingEnemy.PlayHitSound += PlayGetHitSound;
        }

        private void PlayGetHitSound()
        {
            source.clip = getHit;
            source.Play();
        }
    }
}