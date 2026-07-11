using UnityEngine;

namespace PixelFantasy.Gameplay.Player
{
    public static class VerticalJump
    {
        public static Vector2 CalculateVelocity(
            Vector2 currentVelocity,
            bool jumpRequested,
            bool isGrounded,
            float jumpForce)
        {
            if (!jumpRequested || !isGrounded)
            {
                return currentVelocity;
            }

            return new Vector2(currentVelocity.x, Mathf.Max(0f, jumpForce));
        }
    }
}
