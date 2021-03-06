﻿using Code.Scripts.Entity;
using UnityEngine;

namespace Code.Classes.CombatController
{
    public abstract class BaseCombatController : ICombatController
    {
        protected readonly GameObject GameObject;
        protected int MaxLife;

        protected BaseCombatController(GameObject go, int maxLife)
        {
            GameObject = go;
            CurrentLife = MaxLife = maxLife;
        }

        public int CurrentLife { get; set; }

        public virtual void ReceiveHit()
        {
            CurrentLife -= 1;
            if (CurrentLife <= 0)
                GameObject.GetComponent<BaseEntity>().Die();
        }
    }
}