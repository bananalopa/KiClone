using System;
using Kingdom.Input;
using R3;
using UnityEngine;
using UnityEngine.InputSystem;
using Zenject;

namespace Kingdom.Monarch
{
    public class MonarchMoveController : MonoBehaviour
    {
        [SerializeField] Transform playerTransform;
        [SerializeField] private CharacterController characterController;
        [SerializeField] private SerializableReactiveProperty<bool> isFlipped;
        [SerializeField] Animator animator;
        
        private InputHandler inputHandler;

        const string IS_WALKING = "isWalking";

        public SerializableReactiveProperty<bool> IsFlipped => isFlipped;

        [Inject]
        void Construct(InputHandler inputHandler)
        {
	        this.inputHandler = inputHandler;
        }

        private void Update()
        {
            float dt = Time.deltaTime;
            float velocity = 0;
            velocity = inputHandler.Hor;
            UpdateDirection(velocity);
            characterController.Move(new Vector3(velocity, 0,0) * dt);

            animator.SetBool(IS_WALKING, Mathf.Abs(velocity) > 0);
        }
        
        // default direction is always to the right
        private void UpdateDirection(float direction)
        {
            switch (direction)
            {
                // Use scale to flip character depending on direction
                case > 0 when isFlipped.Value:
                    isFlipped.Value = false;
                    playerTransform.localScale = Vector3.one;
                    break;
                case < 0 when !isFlipped.Value:
                    isFlipped.Value = true;
                    playerTransform.localScale = new Vector3(-1, 1, 1);
                    break;
            }
        }
    }
}