using UnityEngine;

namespace PixelFantasy.Gameplay.Player
{
    public static class HorizontalMovement
    {
        public static Vector2 CalculateVelocity(Vector2 currentVelocity, float input, float speed)
        {
            float horizontalVelocity = Mathf.Clamp(input, -1f, 1f) * Mathf.Max(0f, speed);
            return new Vector2(horizontalVelocity, currentVelocity.y);
        }
    }
}

