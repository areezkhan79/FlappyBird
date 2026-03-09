using System;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class BirdScript : MonoBehaviour
{
    private Rigidbody2D _rigidbody;
    
    [Range(0, 100)]
    [SerializeField]
    private float jumpForce = 10f;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
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
           LogicScript.Instance.GameOver();
        }
    }
    
}
