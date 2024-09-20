using System;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Interactions;

namespace Kingdom.Input
{
    public class PlayerInputHandler : MonoBehaviour
    {
        [SerializeField] private TapAndHoldInputInteractionSim tapAndHoldInputInteractionSim;


        private void OnEnable()
        {
            //tapAndHoldInputInteractionSim.OnCancelled+= () => {Debug.Log("Oncancelled"); };
        }
    }
}