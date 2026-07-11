using PixelFantasy.Data.ScriptableObjects;
using PixelFantasy.Infrastructure.Input;
using UnityEngine;

namespace PixelFantasy.Gameplay.Player
{
    [RequireComponent(typeof(Rigidbody2D))]
    public sealed class PlayerMotor : MonoBehaviour
    {
        [SerializeField] private Rigidbody2D body;
        [SerializeField] private PlayerInputReader inputReader;
        [SerializeField] private PlayerMovementConfig config;

        public float HorizontalVelocity => body == null ? 0f : body.linearVelocityX;

        private void Reset()
        {
            body = GetComponent<Rigidbody2D>();
            inputReader = GetComponent<PlayerInputReader>();
        }

        private void Awake()
        {
            if (body == null || inputReader == null || config == null)
            {
                Debug.LogError("PlayerMotor requires a Rigidbody2D, PlayerInputReader, and movement config.", this);
                enabled = false;
            }
        }

        private void FixedUpdate()
        {
            body.linearVelocity = HorizontalMovement.CalculateVelocity(
                body.linearVelocity,
                inputReader.HorizontalMovement,
                config.MovementSpeed);
        }
    }
}

