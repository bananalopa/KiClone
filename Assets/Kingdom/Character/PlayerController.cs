using System;
using Kingdom.Input;
using UnityEngine;
using UnityEngine.InputSystem;
using Zenject;

namespace Kingdom.Core
{
    public class PlayerController : MonoBehaviour
    {
        [SerializeField] Transform playerTransform;
        [SerializeField] private CharacterController characterController;
        [SerializeField] private bool isFlipped = false;
        
        private PlayerInputHandler playerInputHandler;

        [Inject]
        void Construct(PlayerInputHandler playerInputHandler)
        {
	        this.playerInputHandler = playerInputHandler;
        }

        private void Update()
        {
            float dt = Time.deltaTime;
            float velocity = 0;
            velocity = playerInputHandler.Hor;
            UpdateDirection(velocity);
            characterController.Move(new Vector3(velocity, 0,0) * dt);
        }
        
        // default direction is always to the right
        private void UpdateDirection(float direction)
        {
            switch (direction)
            {
                // Use scale to flip character depending on direction
                case > 0 when isFlipped:
                    isFlipped = false;
                    playerTransform.localScale = Vector3.one;
                    break;
                case < 0 when !isFlipped:
                    isFlipped = true;
                    playerTransform.localScale = new Vector3(-1, 1, 1);
                    break;
            }
        }
    }
}