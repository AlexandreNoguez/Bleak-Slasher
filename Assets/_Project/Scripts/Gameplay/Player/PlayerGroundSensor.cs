using UnityEngine;

namespace PixelFantasy.Gameplay.Player
{
    public sealed class PlayerGroundSensor : MonoBehaviour
    {
        [SerializeField] private LayerMask groundMask;
        [SerializeField] private Vector2 checkOffset = new Vector2(0f, -0.55f);
        [SerializeField, Min(0.01f)] private float checkRadius = 0.12f;

        public bool IsGrounded =>
            Physics2D.OverlapCircle(GetCheckPosition(), checkRadius, groundMask) != null;

        private Vector2 GetCheckPosition()
        {
            return (Vector2)transform.position + checkOffset;
        }

        private void OnDrawGizmosSelected()
        {
            Gizmos.color = IsGrounded ? Color.green : Color.yellow;
            Gizmos.DrawWireSphere(GetCheckPosition(), checkRadius);
        }
    }
}
