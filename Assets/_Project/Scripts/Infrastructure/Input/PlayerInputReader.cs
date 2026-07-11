using UnityEngine;
using UnityEngine.InputSystem;

namespace PixelFantasy.Infrastructure.Input
{
    public sealed class PlayerInputReader : MonoBehaviour
    {
        [SerializeField] private InputActionReference moveAction;
        [SerializeField] private InputActionReference jumpAction;

        private bool enabledMoveAction;
        private bool enabledJumpAction;
        private bool jumpRequested;

        public float HorizontalMovement { get; private set; }

        public bool ConsumeJumpRequest()
        {
            if (!jumpRequested)
            {
                return false;
            }

            jumpRequested = false;
            return true;
        }

        private void OnEnable()
        {
            if (moveAction == null || jumpAction == null)
            {
                Debug.LogError("Move and Jump input actions must be assigned.", this);
                enabled = false;
                return;
            }

            moveAction.action.performed += OnMovePerformed;
            moveAction.action.canceled += OnMoveCanceled;
            jumpAction.action.performed += OnJumpPerformed;

            if (!moveAction.action.enabled)
            {
                moveAction.action.Enable();
                enabledMoveAction = true;
            }

            if (!jumpAction.action.enabled)
            {
                jumpAction.action.Enable();
                enabledJumpAction = true;
            }
        }

        private void OnDisable()
        {
            if (moveAction != null)
            {
                moveAction.action.performed -= OnMovePerformed;
                moveAction.action.canceled -= OnMoveCanceled;
                if (enabledMoveAction)
                {
                    moveAction.action.Disable();
                    enabledMoveAction = false;
                }
            }

            if (jumpAction != null)
            {
                jumpAction.action.performed -= OnJumpPerformed;
                if (enabledJumpAction)
                {
                    jumpAction.action.Disable();
                    enabledJumpAction = false;
                }
            }

            HorizontalMovement = 0f;
            jumpRequested = false;
        }

        private void OnMovePerformed(InputAction.CallbackContext context)
        {
            HorizontalMovement = Mathf.Clamp(context.ReadValue<Vector2>().x, -1f, 1f);
        }

        private void OnMoveCanceled(InputAction.CallbackContext context)
        {
            HorizontalMovement = 0f;
        }

        private void OnJumpPerformed(InputAction.CallbackContext context)
        {
            jumpRequested = true;
        }
    }
}
