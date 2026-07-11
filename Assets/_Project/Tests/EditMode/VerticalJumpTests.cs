using NUnit.Framework;
using PixelFantasy.Gameplay.Player;
using UnityEngine;

namespace PixelFantasy.Tests.EditMode
{
    public sealed class VerticalJumpTests
    {
        [Test]
        public void CalculateVelocity_WhenGroundedAndRequested_AppliesJumpForce()
        {
            Vector2 velocity = VerticalJump.CalculateVelocity(new Vector2(2f, -1f), true, true, 12f);

            Assert.That(velocity.x, Is.EqualTo(2f));
            Assert.That(velocity.y, Is.EqualTo(12f));
        }

        [Test]
        public void CalculateVelocity_WhenAirborne_DoesNotJump()
        {
            Vector2 velocity = VerticalJump.CalculateVelocity(new Vector2(2f, -1f), true, false, 12f);

            Assert.That(velocity, Is.EqualTo(new Vector2(2f, -1f)));
        }

        [Test]
        public void CalculateVelocity_WhenNotRequested_DoesNotJump()
        {
            Vector2 velocity = VerticalJump.CalculateVelocity(new Vector2(2f, -1f), false, true, 12f);

            Assert.That(velocity, Is.EqualTo(new Vector2(2f, -1f)));
        }

        [Test]
        public void CalculateVelocity_ClampsNegativeJumpForceToZero()
        {
            Vector2 velocity = VerticalJump.CalculateVelocity(Vector2.zero, true, true, -1f);

            Assert.That(velocity.y, Is.EqualTo(0f));
        }
    }
}
