using System;
using UnityEngine;
using UnityEngine.InputSystem;
using R3;

namespace Kingdom.Input
{
    public class InputHandler : MonoBehaviour
    {
        [SerializeField] InputActionReference horAction;
        [SerializeField] InputActionReference downTapAction;
        [SerializeField] InputActionReference downHoldAction;

        public readonly Subject<float> HorPress = new();
        public readonly Subject<Unit> DownTap= new();
        public readonly Subject<bool> DownHold= new();
        private bool isDownHold = false;
        
        public bool IsDownHold => isDownHold;
        public float Hor => horAction.action.ReadValue<float>();
        
        void OnHorPress(InputAction.CallbackContext context)
        {
            HorPress.OnNext(context.ReadValue<float>());
        }
        
        void OnDownTap(InputAction.CallbackContext context)
        {
            DownTap.OnNext(Unit.Default);
        }
        
        void OnDownHoldPerformed(InputAction.CallbackContext context)
        {
            isDownHold = true;
            DownHold.OnNext(true);
        }
        
        void OnDownHoldCanceled(InputAction.CallbackContext context)
        {
            if (IsDownHold)
            {
                DownHold.OnNext(false);
            }

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