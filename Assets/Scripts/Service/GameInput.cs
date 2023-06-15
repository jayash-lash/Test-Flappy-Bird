using System;
using IoC;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Service
{
    public class GameInput : MonoBehaviour, PlayerInputActions.IPlayerActions, IService
    {  
        public Action OnJumpPerformed;
        public Type ServiceType => typeof(GameInput);
    
        private PlayerInputActions _playerInputActions;
        private void OnEnable()
        {
            if (_playerInputActions != null)
                return;

            _playerInputActions = new PlayerInputActions();
            _playerInputActions.Player.SetCallbacks(this);
            _playerInputActions.Player.Enable();
        }
        public void OnDisable()
        {
            _playerInputActions.Player.Disable();
        }
        public void OnJump(InputAction.CallbackContext context)
        {
            if (!context.performed)
                return;
            OnJumpPerformed?.Invoke();
        }
    }
}