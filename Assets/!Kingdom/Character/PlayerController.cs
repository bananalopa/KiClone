using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Kingdom.Core
{
    public class PlayerController : MonoBehaviour
    {
        [SerializeField] private CharacterController characterController;

        private void OnEnable()
        {
           // walkActionReference.action.Enable();
           // runActionReference.action.Enable();
        }

        private void Start()
        {
            //walkActionReference.action.started += (context) => { Debug.Log("started"); };
            //walkActionReference.action.performed += (context) =>
            //{
               // Debug.Log("performed"); 
                
           // };
        }

        private void Update()
        {
            float dt = Time.deltaTime;

            
            //if (walkActionReference.action.IsInProgress())
           //     Debug.Log(walkActionReference.action.ReadValue<float>());
            
            
   //         float velocity = 0;
//                velocity = (walkActionReference.action.ReadValue<float>());
			


      //      characterController.Move(new Vector3(velocity, 0,0) * dt);
        }
        
        private void OnDisable()
        {
           // walkActionReference.action.Disable();
          //  runActionReference.action.Disable();
        }
        
    }
    

}