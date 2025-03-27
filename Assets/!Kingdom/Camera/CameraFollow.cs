
using Kingdom.Monarch;
using R3;
using Unity.Cinemachine;
using UnityEngine;
using Zenject;

namespace _Kingdom.Camera
{

    /// <summary>
    /// This class inherits CinemachineCameraManagerBase, which is a convenient base class for
    /// making complex cameras by transitioning between a number of worker cameras, depending
    /// on some arbitrary game state.
    /// 
    /// In this case, we monitor the player's facing direction and motion, and select a camera
    /// with the appropriate settings.  CinemachineCameraManagerBase takes care of handling the blends.
    /// </summary>
    [ExecuteAlways]
    public class CameraFollow : CinemachineCameraManagerBase
    {
        enum PlayerState
        {
            Right,
            Left
        }
        
        // The cameras in these fields must be GameObject children of the manager camera.
        [Header("State Cameras")]
        [ChildCameraProperty] public CinemachineVirtualCameraBase RightCamera;
        [ChildCameraProperty] public CinemachineVirtualCameraBase LeftCamera;

        
        [SerializeField] MonarchMoveController monarchMoveController;

        [Inject]
        void Construct(MonarchMoveController monarchMoveController)
        {
            this.monarchMoveController = monarchMoveController;
        }


        protected override void OnEnable()
        {
            base.OnEnable();
            var target = DefaultTarget.Enabled ? DefaultTarget.Target.TrackingTarget : null;
            
        }
   
        PlayerState GetPlayerState()
        {
            return monarchMoveController.IsFlipped.Value ? PlayerState.Left : PlayerState.Right;
        }

        /// <summary>
        /// Choose the appropriate child camera depending on player state.
        /// </summary>
        protected override CinemachineVirtualCameraBase ChooseCurrentCamera(Vector3 worldUp, float deltaTime)
        {
            return GetPlayerState() switch
            {
                PlayerState.Left => LeftCamera,
                _ => RightCamera,
            };
        }
    }


}