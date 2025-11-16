namespace TooMooseGames.Parallax
{
    using UnityEngine;

    public class PlayerController2D : MonoBehaviour
    {
        [SerializeField, Tooltip("Movement speed of the player")]
        private float moveSpeed = 5f;

        private Rigidbody2D _rb;

        void Start()
        {
            _rb = GetComponent<Rigidbody2D>();
        }

        void FixedUpdate()
        {
            // Get input for horizontal movement
            var moveInputHorizontal = Input.GetAxisRaw("Horizontal");
            var moveInputVertical = Input.GetAxisRaw("Vertical");
            
            // Move the player
            _rb.linearVelocity = new Vector2(moveInputHorizontal * moveSpeed,
                moveInputVertical != 0 ? moveInputVertical * moveSpeed : _rb.linearVelocity.y);
        }
    }
}
