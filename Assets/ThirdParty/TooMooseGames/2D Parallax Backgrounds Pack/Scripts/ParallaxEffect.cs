using UnityEngine;

namespace TooMooseGames._2D_Parallax_Backgrounds_Pack.Scripts
{
    public class ParallaxEffect : MonoBehaviour
    {
        [SerializeField, Tooltip("Update mode")]
        private UpdateMode updateMode = UpdateMode.LateUpdate;

        [SerializeField, Tooltip("Speed of the parallax effect")]
        private Vector2 parallaxSpeed = new(0.5f, 0f);

        [SerializeField, Tooltip("Enable X scrolling")]
        private bool scrollX = true;

        [SerializeField, Tooltip("Enable Y scrolling")]
        private bool scrollY = true;

        private Transform _cameraTransform;
        private Vector2 _lastCameraPosition;
        private Vector2 _textureUnitSize;

        void Start()
        {
            // Get the main camera's transform
            if (Camera.main != null) _cameraTransform = Camera.main.transform;

            // Store the initial camera position
            _lastCameraPosition = _cameraTransform.position;

            // If we are a child object don't run the parallax effect
            if (transform.parent?.GetComponent<ParallaxEffect>() != null) return;
            
            var spriteRenderer = GetComponent<SpriteRenderer>();
            if (spriteRenderer != null)
            {
                // Calculate the size of the texture based on the sprite renderer's bounds
                _textureUnitSize = spriteRenderer.bounds.size;
                
                // Add additional sprites for unlimited scrolling
                if (scrollX)
                {
                    CreateTile("Right", _textureUnitSize.x, 0f);
                    CreateTile("Left", -_textureUnitSize.x, 0f);
                }

                if (scrollY)
                {
                    CreateTile("Up", 0f, _textureUnitSize.y);
                    CreateTile("Down", 0f, -_textureUnitSize.y);
                }

                if (scrollX && scrollY)
                {
                    CreateTile("TopRight", _textureUnitSize.x, _textureUnitSize.y);
                    CreateTile("TopLeft", -_textureUnitSize.x, _textureUnitSize.y);
                    CreateTile("BottomRight", _textureUnitSize.x, -_textureUnitSize.y);
                    CreateTile("BottomLeft", -_textureUnitSize.x, -_textureUnitSize.y);
                }
            }
            else
            {
                Debug.LogError("ParallaxEffect script must be attached to a GameObject with a SpriteRenderer.");
            }
        }

        /// <summary>
        /// Create a new image as a child object
        /// </summary>
        /// <param name="namePostfix">The text to append to the name of the game object e.g. left,right,up,down</param>
        /// <param name="xOffset">The x offset from the original image</param>
        /// <param name="yOffset">The y offset from the original image</param>
        private void CreateTile(string namePostfix, float xOffset, float yOffset)
        {
            var tile = Instantiate(gameObject, transform, false);
            // Remove any added child objects
            foreach (Transform child in tile.transform)
            {
                Destroy(child.gameObject);
            }

            var parallaxEffect = tile.GetComponent<ParallaxEffect>();
            if (parallaxEffect != null)
            {
                // Remove the cloned parallax effect as we only want the original parent one
                Destroy(parallaxEffect);
            }

            tile.name = $"{gameObject.name} - {namePostfix}";
            tile.transform.localPosition = new Vector3(xOffset, yOffset, 0);
        }

        #region Update

        void Update()
        {
            if (updateMode != UpdateMode.Update) return;
            ApplyParallaxEffect();
        }

        void FixedUpdate()
        {
            if (updateMode != UpdateMode.FixedUpdate) return;
            ApplyParallaxEffect();
        }

        void LateUpdate()
        {
            if (updateMode != UpdateMode.LateUpdate) return;
            ApplyParallaxEffect();
        }

        #endregion

        private void ApplyParallaxEffect()
        {
            // Calculate the difference in the camera's position since the last frame
            var cameraDelta = (Vector2)_cameraTransform.position - _lastCameraPosition;
            _lastCameraPosition = _cameraTransform.position;

            // Calculate the parallax movement based on the camera's movement and parallax speed
            var parallaxX = scrollX ? cameraDelta.x * parallaxSpeed.x : 0f;
            var parallaxY = scrollY ? cameraDelta.y * parallaxSpeed.y : 0f;

            // Move the background with parallax effect
            transform.position += new Vector3(parallaxX, parallaxY, 0f);

            // Check if the background needs to be tiled horizontally
            if (scrollX && Mathf.Abs(_cameraTransform.position.x - transform.position.x) >= _textureUnitSize.x)
            {
                var offsetX = (_cameraTransform.position.x - transform.position.x) % _textureUnitSize.x;
                transform.position = new Vector3(_cameraTransform.position.x + offsetX, transform.position.y,
                    transform.position.z);
            }

            // Check if the background needs to be tiled vertically
            if (scrollY && Mathf.Abs(_cameraTransform.position.y - transform.position.y) >= _textureUnitSize.y)
            {
                var offsetY = (_cameraTransform.position.y - transform.position.y) % _textureUnitSize.y;
                transform.position = new Vector3(transform.position.x, _cameraTransform.position.y + offsetY,
                    transform.position.z);
            }
        }

        private enum UpdateMode
        {
            Update,
            LateUpdate,
            FixedUpdate
        }
    }
}
