using System.Collections.Generic;
using UnityEngine;

namespace Code.Scripts
{
    public class HorizintallyMovingPlatform : BaseMovingPlatform
    {
        protected override void CalculateDirection()
        {
            Direction = Direction == Vector3.right ? Vector3.left : Vector3.right;
        }
    }
}