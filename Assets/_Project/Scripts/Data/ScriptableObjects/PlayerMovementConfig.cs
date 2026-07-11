using UnityEngine;

namespace PixelFantasy.Data.ScriptableObjects
{
    [CreateAssetMenu(
        fileName = "SO_PlayerMovementConfig",
        menuName = "Pixel Fantasy/Player/Movement Config")]
    public sealed class PlayerMovementConfig : ScriptableObject
    {
        [SerializeField, Min(0f)] private float movementSpeed = 5f;

        public float MovementSpeed => movementSpeed;

        private void OnValidate()
        {
            movementSpeed = Mathf.Max(0f, movementSpeed);
        }
    }
}

