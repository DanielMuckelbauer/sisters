﻿using UnityEngine;

namespace Code.Scripts.Hazards
{
    public class RespawnOneHazard : BaseHazard
    {
        private void OnCollisionStay2D(Collision2D other)
        {
            if (other.gameObject.tag.Contains("Enemy"))
                Destroy(other.gameObject);
            if (!other.gameObject.tag.Contains("Player"))
                return;
            if (EntityController != null)
                EntityController.RespawnOne(other.gameObject);
        }
    }
}