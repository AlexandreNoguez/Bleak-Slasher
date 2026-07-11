using PixelFantasy.Data.ScriptableObjects;
using PixelFantasy.Infrastructure.Input;
using UnityEngine;

namespace PixelFantasy.Gameplay.Player
{
    [RequireComponent(typeof(Rigidbody2D))]
    public sealed class PlayerJumper : MonoBehaviour
    {
        [SerializeField] private Rigidbody2D body;
        [SerializeField] private PlayerInputReader inputReader;
        [SerializeField] private PlayerGroundSensor groundSensor;
        [SerializeField] private PlayerJumpConfig config;

        public bool IsGrounded => groundSensor != null && groundSensor.IsGrounded;
        public float VerticalVelocity => body == null ? 0f : body.linearVelocityY;

        private void Reset()
        {
            body = GetComponent<Rigidbody2D>();
            inputReader = GetComponent<PlayerInputReader>();
            groundSensor = GetComponent<PlayerGroundSensor>();
        }

        private void Awake()
        {
            if (body == null || inputReader == null || groundSensor == null || config == null)
            {
                Debug.LogError("PlayerJumper requires a Rigidbody2D, PlayerInputReader, PlayerGroundSensor, and jump config.", this);
                enabled = false;
            }
        }

        private void FixedUpdate()
        {
            body.linearVelocity = VerticalJump.CalculateVelocity(
                body.linearVelocity,
                inputReader.ConsumeJumpRequest(),
                groundSensor.IsGrounded,
                config.JumpForce);
        }
    }
}
