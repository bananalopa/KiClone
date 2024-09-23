using System;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Interactions;

namespace Kingdom.Input
{
    public class PlayerInputHandler : MonoBehaviour
    {
        [SerializeField] InputActionReference horAction;
        [SerializeField] InputActionReference downTapAction;
        [SerializeField] InputActionReference downHoldAction;

        private Action<float> HorPress;
        private Action DownTap;
        private Action<bool> DownHold;
        
        bool isDownHold = false;
        
        public bool IsDownHold => isDownHold;
        public float Hor => horAction.action.ReadValue<float>();
        
        void OnHorPress(InputAction.CallbackContext context)
        {
            HorPress?.Invoke(context.ReadValue<float>());
        }
        
        void OnDownTap(InputAction.CallbackContext context)
        {
            DownTap?.Invoke();
        }
        
        void OnDownHoldPerformed(InputAction.CallbackContext context)
        {
            isDownHold = true;
            DownHold?.Invoke(true);
        }
        
        void OnDownHoldCanceled(InputAction.CallbackContext context)
        {
            if (IsDownHold)
                DownHold?.Invoke(false);
            isDownHold = false;
        }
        
        private void OnEnable()
        {
            horAction.action.performed += OnHorPress;
            downTapAction.action.performed += OnDownTap;
            downHoldAction.action.performed += OnDownHoldPerformed;
            downHoldAction.action.canceled += OnDownHoldCanceled;
        }
        
        private void OnDisable()
        {
            horAction.action.performed -= OnHorPress;
            downTapAction.action.performed -= OnDownTap;
            downHoldAction.action.performed -= OnDownHoldPerformed;
            downHoldAction.action.canceled -= OnDownHoldCanceled;
        }
    }
}