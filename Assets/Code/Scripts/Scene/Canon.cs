﻿using UnityEngine;

namespace Code.Scripts.Scene
{
    public abstract class BaseCanon : MonoBehaviour
    {
        public Transform Source;
        public GameObject Projectile;

        protected virtual void ShootProjectile(Vector3 target)
        {
            GameObject projectile = Instantiate(Projectile, Source.position, new Quaternion());
            projectile.GetComponent<BaseProjectile>().Shoot(target);
        }
    }
}