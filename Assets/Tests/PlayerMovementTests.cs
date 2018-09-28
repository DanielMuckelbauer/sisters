using Code.Classes;
using Code.Classes.MovementController;
using NUnit.Framework;
using UnityEngine;

namespace Tests
{
    public class PlayerMovementTests
    {
        [Test]
        public void PlayerFlipsLeft()
        {
            GameObject player = CreatePlayer();
            IMovementController movementController = new PlayerMovementController(player, new RectTransform());
            movementController.Move(-5);
            Assert.IsFalse(movementController.LookingRight);
        }

        [Test]
        public void PlayerFlipsRight()
        {
            GameObject player = CreatePlayer();
            IMovementController movementController =
                new PlayerMovementController(player, new RectTransform()) {LookingRight = true};
            movementController.Move(5);
            Assert.IsTrue(movementController.LookingRight);
        }

        private static GameObject CreatePlayer()
        {
            GameObject player = new GameObject();
            player.AddComponent<Rigidbody2D>();
            player.AddComponent<SpriteRenderer>();
            return player;
        }
    }
}