using UnityEngine;

namespace PixelFantasy.Data.ScriptableObjects
{
    [CreateAssetMenu(
        fileName = "SO_PlayerJumpConfig",
        menuName = "Pixel Fantasy/Player/Jump Config")]
    public sealed class PlayerJumpConfig : ScriptableObject
    {
        [SerializeField, Min(0f)] private float jumpForce = 12f;

        public float JumpForce => jumpForce;

        private void OnValidate()
        {
            jumpForce = Mathf.Max(0f, jumpForce);
        }
    }
}
