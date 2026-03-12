using System.Collections;
using UnityEngine;

namespace _FlappyBird._Scripts
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class Bird : MonoBehaviour
    {
        private Rigidbody2D _rigidbody;
        
        [Header("Bird Settings")]
        
        [Tooltip("Jumping force of the bird---Default = 10f")]
        [Range(0, 100)]
        [SerializeField] private float jumpForce = 45f;
        
        [Space(10)]
        
        [SerializeField] private float upperBoundForBird = 25f;
        [SerializeField] private float lowerBoundForBird = -25f;
        private bool IsBirdDead => (transform.position.y >=  upperBoundForBird || transform.position.y <= lowerBoundForBird);

        private void Awake()
        {
            _rigidbody = GetComponent<Rigidbody2D>();
        }
        private void Update()
        {
            if (IsBirdDead)
            {
                Logic.Instance.GameOver();
            }
            if (!Input.GetKeyDown(KeyCode.Space) || GameStateManager.Instance.currentState == GameState.Over)
            {
                return;
            }
            GameStateManager.Instance.currentState = GameState.Play;
            Jump();
        }

        
        private void OnCollisionEnter2D(Collision2D other)
        {
            if (other.gameObject.CompareTag("Obstacles"))
            {
                Logic.Instance.GameOver();
            }
        }
        
        private void Jump()
        {
            _rigidbody.linearVelocity = Vector2.up * jumpForce;
            
        }
    }
}
