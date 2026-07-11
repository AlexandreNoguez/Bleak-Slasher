using NUnit.Framework;
using PixelFantasy.Gameplay.Player;
using UnityEngine;

namespace PixelFantasy.Tests.EditMode
{
    public sealed class HorizontalMovementTests
    {
        [TestCase(-1f, -5f)]
        [TestCase(0f, 0f)]
        [TestCase(1f, 5f)]
        public void CalculateVelocity_UsesInputAndPreservesVerticalVelocity(
            float input,
            float expectedHorizontalVelocity)
        {
            Vector2 velocity = HorizontalMovement.CalculateVelocity(new Vector2(2f, 3f), input, 5f);

            Assert.That(velocity.x, Is.EqualTo(expectedHorizontalVelocity));
            Assert.That(velocity.y, Is.EqualTo(3f));
        }

        [Test]
        public void CalculateVelocity_ClampsInputToValidRange()
        {
            Vector2 velocity = HorizontalMovement.CalculateVelocity(Vector2.zero, 2f, 5f);

            Assert.That(velocity.x, Is.EqualTo(5f));
        }
    }
}
