using System.Collections;
using UnityEngine;

namespace _FlappyBird._Scripts
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class Bird : MonoBehaviour
    {
        private Rigidbody2D _rigidbody;

        [Header("References")]
        [SerializeField] private GameObject wingUp;
        [SerializeField] private GameObject wingDown;
        
        [Space(10)]
        
        [Header("Bird Settings")]

        [Tooltip("Time between wings flapping up and down (in seconds)---Default = 0.3f")]
        [Range(0,1)]
        [SerializeField] private float wingFlapInterval = 0.3f;
    
        [Tooltip("Jumping force of the bird---Default = 10f")]
        [Range(0, 45)]
        [SerializeField] private float jumpForce = 10f;
        
        #region Features to implement

        // [Tooltip("Rotation speed of the bird when ascending or descending.---Default = 5f")]
        // [Range(0, 45)]
        // [SerializeField] private float rotationSpeed = 5f;
        //
        // [Tooltip("The angle of the bird when we want to jump---Default = 20f")]
        // [Range(0, 45)] [SerializeField] private int ascentAngleLimit = 20;
        //
        // [Tooltip("The angle of the bird when in free fall---Default = -20f")]
        // [Range(0, -45)] [SerializeField] private int descentAngleLimit = -20;

        #endregion

        #region Core Functions
        private void Awake()
        {
            _rigidbody = GetComponent<Rigidbody2D>();
        }
        private void Start()
        {
            StartCoroutine(FlapWings());
        }
        private void Update()
        {
            if (!Input.GetKeyDown(KeyCode.Space) || GameStateManager.Instance.currentState == GameState.Over) return;
            GameStateManager.Instance.currentState = GameState.Play;
            _rigidbody.linearVelocity = Vector2.up * jumpForce;
        
        }
        private void OnCollisionEnter2D(Collision2D other)
        {
            if (other.gameObject.CompareTag("Obstacles"))
            {
                Logic.Instance.GameOver();
            }
        }
        #endregion
       
        #region Coroutines
        private IEnumerator FlapWings()
        {
            yield return new WaitUntil(() => GameStateManager.Instance.currentState == GameState.Play);
            while (true)
            {
                if (GameStateManager.Instance.currentState == GameState.Pause || GameStateManager.Instance.currentState == GameState.Over)
                {
                    yield break;
                }

                yield return new WaitForSeconds(wingFlapInterval);
                Flap();
            }
        }
        #endregion
        
        #region Utility Functions
        private void Flap()
        {
            if (wingUp.activeSelf)
            {
                wingUp.SetActive(false);
                wingDown.SetActive(true);
            }
            else
            {
                wingUp.SetActive(true);
                wingDown.SetActive(false);
            }
        }
        #endregion
    }
}
