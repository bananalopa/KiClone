namespace TooMooseGames.Parallax
{
    using UnityEngine;

    public class CameraFollow : MonoBehaviour
    {
        [SerializeField, Tooltip("Reference to the player's transform")]
        private Transform player;

        [SerializeField, Tooltip("Speed of the camera's movement smoothing")]
        private float smoothSpeed = 0.125f;

        [SerializeField, Tooltip("Offset between the camera and the player")]
        private Vector3 offset;

        [SerializeField, Tooltip("Follow on the horizontal")]
        private bool followHorizontal = true;

        [SerializeField, Tooltip("Follow on the vertical")]
        private bool followVertical = true;

        private float _cameraZ;

        private void Start()
        {
            if (Camera.main != null) _cameraZ = Camera.main.transform.position.z;
        }

        void Update()
        {
            // Desired position of the camera based on player's position and offset
            var desiredPosition = player.position + offset;

            // Smoothly interpolate the camera's position towards the desired position
            var smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);

            // Set the camera's position to the smoothed position
            transform.position = new Vector3(followHorizontal ? smoothedPosition.x : transform.position.x,
                followVertical ? smoothedPosition.y : transform.position.y,
                _cameraZ);
        }
    }
}