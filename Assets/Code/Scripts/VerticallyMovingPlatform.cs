﻿using UnityEngine;

namespace Code.Scripts
{
    public class VerticallyMovingPlatform : BaseMovingPlatform
    {
        protected override void CalculateDirection()
        {
            Direction = ChangeBetweenTwoDirections(Vector3.up, Vector3.down);
        }

        protected override void Start()
        {
            Direction = Vector3.up;
            base.Start();
        }
    }
}