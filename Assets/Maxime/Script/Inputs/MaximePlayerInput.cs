using System;
using Maxime.Script.EventArgs;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Maxime.Script.Inputs
{
    public class MaximePlayerInput : MonoBehaviour
    {
        private PlayerInput _playerInput;
        public event EventHandler<JumpEventArgs> OnJump;

        private void OnEnable()
        {
            _playerInput = gameObject.GetComponent<PlayerInput>();
            _playerInput.actions["Jump"].performed += PlayerInput_onJump;
        }
        
        private void OnDisable()
        {
            _playerInput.actions["Jump"].performed -= PlayerInput_onJump;
        }

        private void PlayerInput_onJump(InputAction.CallbackContext ctx)
        {
            if (ctx.action.phase == InputActionPhase.Performed)
                OnJump?.Invoke(this, new JumpEventArgs(true));
            else if (ctx.action.phase == InputActionPhase.Canceled)
                OnJump?.Invoke(this, new JumpEventArgs(false));
        }
    }
}